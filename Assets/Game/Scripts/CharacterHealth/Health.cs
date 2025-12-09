using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    [SerializeField] private float _maxHealth = 100f;

    private IStopable _stopable;
    private DamagableManager _damagableManager;
    private IDamageAnimator _damageAnimator;

    private float _currentHealth;
    public Vector3 Position => transform.position;
    public float CurrentHealthPercent => _currentHealth / _maxHealth;

    public void Initialize(IStopable stopable, DamagableManager manager, IDamageAnimator damageAnimator)
    {
        _stopable = stopable;
        _damagableManager = manager;
        _damagableManager.RegisterDamagable(this);

        _damageAnimator = damageAnimator;

        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float value)
    {
        _damageAnimator.TakeDamageAnimationRun();
        
        ChangeHealth(-value);

        _stopable.StopMove();
    }

    private void ChangeHealth(float value)
    {
        _currentHealth += value;

        if (_currentHealth <= 0)
        {
            _maxHealth = 0;

            _stopable.StopMove();

            _damageAnimator.DyingAnimationRun();
        }

        if (_currentHealth >= _maxHealth)
            _currentHealth = _maxHealth;
    }
}
