using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour, IDamageAnimator
{
    private readonly int WalkingVelocity = Animator.StringToHash("Velocity");
    private readonly int IsExploded = Animator.StringToHash("IsExploded");
    private readonly int IsDying = Animator.StringToHash("IsDying");

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

    public void TakeDamageAnimationRun() => _animator.SetTrigger(IsExploded);

    public void DyingAnimationRun() => _animator.SetBool(IsDying, true);

    public void ResumeMove() => _character.ResumeMove();

    private void StopRunning() => _animator.SetFloat(WalkingVelocity, 0);

    private void StartRunning(float speedRatio) => _animator.SetFloat(WalkingVelocity, speedRatio);
}