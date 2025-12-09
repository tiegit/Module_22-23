using UnityEngine;

public class Tower : MonoBehaviour, IDirectionalRotatable
{
    [SerializeField] private float _rotationSpeed;

    private DirectionalRotator _rotator;

    public Quaternion CurrentRotation => _rotator.CurrentRotation;
    public Vector3 Position => transform.position;

    private void Awake()
    {
        _rotator = new DirectionalRotator(transform, _rotationSpeed);
    }

    private void Update()
    {
        _rotator.Update(Time.deltaTime);
    }

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);
}