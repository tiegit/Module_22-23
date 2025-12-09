using UnityEngine;

public interface IDirectionalMovable : ITransformPosition
{
    float MaxSpeed { get; }
    float CurrentSpeed { get; }
    Vector3 CurrentHorizontalVelocity { get; }

    void SetMoveDirection(Vector3 inputDirection);
}
