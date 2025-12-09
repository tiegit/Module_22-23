using UnityEngine;

public class DirectionalRotator
{
    private Transform _transform;
    private float _rotationSpeed;

    private Vector3 _currentDirection;
    public DirectionalRotator(Transform transform, float rotationSpeed)
    {
        _transform = transform;
        _rotationSpeed = rotationSpeed;
    }

    public Quaternion CurrentRotation => _transform.rotation;

    public void SetInputDirection(Vector3 inputDirection) => _currentDirection = inputDirection;

    public void Update(float deltaTime)
    {
        if (_currentDirection.magnitude < 0.05f)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(_currentDirection.normalized);

        float speed = _rotationSpeed * deltaTime;

        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, speed);
    }
}