using UnityEngine;

public class Character : MonoBehaviour, IDirectionalMovable, IDirectionalRotatable, ICharacter
{
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _injuredMoveSpeed;
    [SerializeField] private float _rotationSpeed;

    private DirectionalMover _mover;
    private DirectionalRotator _rotator;

    [SerializeField] private ObstacleChecker _groundChecker;
    [SerializeField] private float _gravityForce;

    private float _currentSpeed;
    private bool _isDead;

    public float MaxSpeed => _maxMoveSpeed;
    public float InjuredMoveSpeed => _injuredMoveSpeed;

    public Vector3 CurrentHorizontalVelocity => _mover.CurrentHorizontalVelocity;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;
    public Vector3 Position => transform.position;

    private void Awake()
    {
        _mover = new DirectionalMover(GetComponent<CharacterController>(), _groundChecker, _gravityForce, _maxMoveSpeed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);
    }

    private void Update()
    {
        if (_isDead)
            return;

        _mover.Update(Time.deltaTime, transform.position.y);
        _rotator.Update(Time.deltaTime);
    }

    public void SetMoveSpeed(float speed)
    {
        _currentSpeed = speed;
        _mover.SetMoveSpeed(speed);
    }

    public void SetDeathState(bool isDead) => _isDead = isDead;

    public void StopMove() => _mover.Stop();

    public void ResumeMove() => _mover.Resume();

    public void SetMoveDirection(Vector3 inputDirection) => _mover.SetInputDirection(inputDirection);

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);
}
