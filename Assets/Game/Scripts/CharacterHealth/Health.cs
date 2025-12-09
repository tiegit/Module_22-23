using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    [SerializeField] private float _maxHealth = 100f;

    private float _currentHealth;
    private DamagableManager _damagableManager;

    public Vector3 Position => transform.position;
    public float CurrentHealthPercent => _currentHealth / _maxHealth;

    public void Initialize(DamagableManager manager)
    {
        _damagableManager = manager;
        _damagableManager.RegisterDamagable(this);

        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float value) => ChangeHealth(-value);

    private void ChangeHealth(float value)
    {
        _currentHealth += value;

        if (_currentHealth <= 0)
            _maxHealth = 0;

        if (_currentHealth >= _maxHealth)
            _currentHealth = _maxHealth;
    }
}
