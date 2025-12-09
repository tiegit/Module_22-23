using UnityEngine;

public class AgentCharacterView : MonoBehaviour
{
    private readonly int WalkingVelocity = Animator.StringToHash("Velocity");

    [SerializeField] private Animator _animator;
    [SerializeField] private AgentCharacter _character;

    private void Update()
    {
        if (_character.CurrentVelocity.magnitude > 0.05f)
            StartRunning(_character.CurrentVelocity.magnitude / _character.MaxSpeed);
        else
            StopRunning();
    }

    private void StopRunning() => _animator.SetFloat(WalkingVelocity, 0);

    private void StartRunning(float speedRatio) => _animator.SetFloat(WalkingVelocity, speedRatio);
}