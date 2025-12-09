using UnityEngine;
using UnityEngine.AI;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private CharacterView _characterView;
    [SerializeField] private Health _characterHealth;
    [SerializeField] private HealthBarView _healthBarView;

    [SerializeField, Space(15)] Pointer _pointerPrefab;

    [SerializeField, Space(15)] private Character _enemyCharacter;
    [SerializeField] private AgentCharacter _agentEnemyCharacter;

    [SerializeField, Space(15)] private MineManager _mineManager;

    private Controller _characterController;
    private Controller _enemyCharacterController;
    private Controller _agentEnemyCharacterController;

    private NavMeshPath _path;

    private void Awake()
    {
        PlayerInput playerInput = new PlayerInput();

        _path = new NavMeshPath();

        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = NavMesh.AllAreas;

        ClickToMoveController playerMoveController = new(playerInput, _character, queryFilter);

        _characterController = new CompositeController(playerMoveController,
                                                       /*new PlayerDirectionalMovableController(playerInput, _character),*/
                                                       new AlongMovableVelocityRotatableController(_character, _character));
        _characterController.Enable();

        _characterView.Initialize(_character);

        DamagableManager damagableManager = new DamagableManager();

        _healthBarView.Initialize(_characterHealth);

        Pointer pointer = Instantiate(_pointerPrefab);
        pointer.Initialize(playerInput, playerMoveController);

        _enemyCharacterController = new CompositeController(new DirectionalMovableAgroController(_enemyCharacter, _character.transform, 10f, 2f, queryFilter, 1),
                                                            new AlongMovableVelocityRotatableController(_enemyCharacter, _enemyCharacter));
        _enemyCharacterController.Enable();

        _agentEnemyCharacterController = new AgentCharacterAgroController(_agentEnemyCharacter, _character.transform, 20, 2, 1);

        _agentEnemyCharacterController.Enable();

        _characterHealth.Initialize(damagableManager);
        _mineManager.Initialize(damagableManager);
    }

    private void Start() => _enemyCharacter.gameObject.SetActive(false);

    private void Update()
    {
        _characterController.Update(Time.deltaTime);
        _enemyCharacterController.Update(Time.deltaTime);
        _agentEnemyCharacterController.Update(Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F))
            _characterHealth.TakeDamage(10);
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
            return;

        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = NavMesh.AllAreas;

        NavMesh.CalculatePath(_enemyCharacter.transform.position, _character.transform.position, queryFilter, _path);

        Gizmos.color = Color.red;

        if (_path.status != NavMeshPathStatus.PathInvalid)
            foreach (Vector3 corner in _path.corners)
                Gizmos.DrawSphere(corner, 0.3f);
    }
}