using UnityEngine;

public class DirectionalMover
{
    private CharacterController _characterController;
    private float _moveSpeed;
    private ObstacleChecker _groundChecker;
    private float _gravityForce;

    private Vector3 _currentDirection;
    private Vector3 _gravityVelocity = Vector3.zero;

    public DirectionalMover(CharacterController characterController,
                            float moveSpeed,
                            ObstacleChecker groundChecker,
                            float gravityForce)
    {
        _characterController = characterController;
        _moveSpeed = moveSpeed;
        _groundChecker = groundChecker;
        _gravityForce = gravityForce;
    }

    public Vector3 CurrentHorizontalVelocity { get; private set; }

    public void SetInputDirection(Vector3 inputDirection) => _currentDirection = inputDirection;

    public void Update(float deltaTime, float yPosition)
    {
        _currentDirection = new Vector3(_currentDirection.x, 0, _currentDirection.z);
        CurrentHorizontalVelocity = _currentDirection.normalized * _moveSpeed;

        if (_groundChecker.IsTouches())
            _gravityVelocity.y = 0;
        else
            _gravityVelocity.y -= _gravityForce * deltaTime;

        Vector3 velocity = CurrentHorizontalVelocity + _gravityVelocity;

        _characterController.Move(velocity * deltaTime);
    }
}