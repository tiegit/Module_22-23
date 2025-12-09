using UnityEngine;

public class Character : MonoBehaviour, IDirectionalMovable, IDirectionalRotatable
{
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _rotationSpeed;

    private DirectionalMover _mover;
    private DirectionalRotator _rotator;

    [SerializeField] private ObstacleChecker _groundChecker;
    [SerializeField] private float _gravityForce;
 
    public float MaxSpeed { get; private set; }
    public Vector3 CurrentHorizontalVelocity => _mover.CurrentHorizontalVelocity;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;
    public Vector3 Position => transform.position;

    private void Awake()
    {
        _mover = new DirectionalMover(GetComponent<CharacterController>(), _maxMoveSpeed, _groundChecker, _gravityForce);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);

        MaxSpeed = _maxMoveSpeed;
    }

    private void Update()
    {
        _mover.Update(Time.deltaTime, transform.position.y);
        _rotator.Update(Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 inputDirection) => _mover.SetInputDirection(inputDirection);

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);
}
