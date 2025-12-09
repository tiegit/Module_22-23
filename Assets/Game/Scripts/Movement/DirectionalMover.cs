using UnityEngine;

public class DirectionalMover
{
    private CharacterController _characterController;
    private ObstacleChecker _groundChecker;
    private float _gravityForce;
    private float _moveSpeed;

    private Vector3 _currentDirection;
    private Vector3 _gravityVelocity = Vector3.zero;
    private bool _isStopped;

    public DirectionalMover(CharacterController characterController,
                            ObstacleChecker groundChecker,
                            float gravityForce,
                            float moveSpeed)
    {
        _characterController = characterController;
        _groundChecker = groundChecker;
        _gravityForce = gravityForce;

        SetMoveSpeed(moveSpeed);
    }

    public Vector3 CurrentHorizontalVelocity { get; private set; }

    public void SetInputDirection(Vector3 inputDirection) => _currentDirection = inputDirection;

    public void SetMoveSpeed(float speed) => _moveSpeed = speed;

    public void Stop() => _isStopped = true;

    public void Resume() => _isStopped = false;

    public void Update(float deltaTime, float yPosition)
    {
        if (_groundChecker.IsTouches())
            _gravityVelocity.y = 0;
        else
            _gravityVelocity.y -= _gravityForce * deltaTime;

        if (_isStopped)
        {
            CurrentHorizontalVelocity = Vector3.zero;
        }
        else
        {
            _currentDirection = new Vector3(_currentDirection.x, 0, _currentDirection.z);
            CurrentHorizontalVelocity = _currentDirection.normalized * _moveSpeed;
        }

        Vector3 velocity = CurrentHorizontalVelocity + _gravityVelocity;

        _characterController.Move(velocity * deltaTime);
    }
}