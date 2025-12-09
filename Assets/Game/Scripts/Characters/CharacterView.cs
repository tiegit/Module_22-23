using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    private readonly int WalkingVelocity = Animator.StringToHash("Velocity");

    private Animator _animator;
    private Character _character;

    public void Initialize(Character character)
    {
        _character = character;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_character.CurrentHorizontalVelocity.magnitude > 0.05f)
            StartRunning(_character.CurrentHorizontalVelocity.magnitude / _character.MaxSpeed);
        else
            StopRunning();
    }

    private void StopRunning() => _animator.SetFloat(WalkingVelocity, 0);

    private void StartRunning(float speedRatio) => _animator.SetFloat(WalkingVelocity, speedRatio);
}