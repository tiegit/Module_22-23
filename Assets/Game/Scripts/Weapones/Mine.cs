using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private float _damage = 30f;
    [SerializeField] private float _explosionRadius = 3f;
    [SerializeField] private float _checkInterval = 0.5f;
    [SerializeField] private float _explosionDelay = 2f;
    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private float _blinkSpeed = 5f; // Увеличена скорость мигания
    [SerializeField] private Renderer _mineRenderer;

    private float _timer;
    private float _explosionTimer;
    private bool _isPlayerInRadius;
    private bool _isActivated; // Флаг активации мины
    private DamagableManager _damagableManager;
    private Color _originalColor;
    private bool _isBlinking;

    public void Initialize(DamagableManager damagableManager) => _damagableManager = damagableManager;

    private void Start()
    {
        if (_mineRenderer != null)
        {
            _originalColor = _mineRenderer.material.color;
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _checkInterval && _damagableManager != null)
        {
            CheckForDamageableObjects();
            _timer = 0f;
        }

        // Если мина активирована, продолжаем отсчет независимо от положения игрока
        if (_isActivated)
        {
            _explosionTimer += Time.deltaTime;

            // Мигание во время активации
            if (_mineRenderer != null)
            {
                float blinkValue = Mathf.PingPong(Time.time * _blinkSpeed, 1f);
                Color blinkColor = Color.Lerp(_originalColor, Color.white, blinkValue);
                _mineRenderer.material.color = blinkColor;
                _isBlinking = true;
            }

            if (_explosionTimer >= _explosionDelay)
            {
                Explode();
            }
        }
    }

    private void CheckForDamageableObjects()
    {
        List<IDamagable> damagables = _damagableManager.GetAllDamagables();

        foreach (var damagable in damagables)
        {
            float distance = Vector3.Distance(transform.position, damagable.Position);

            if (distance <= _explosionRadius)
            {
                // Активируем мину при первом обнаружении игрока
                if (!_isActivated)
                {
                    _isActivated = true;
                    _isPlayerInRadius = true;
                    _explosionTimer = 0f;
                }
                return;
            }
        }

        // Игрок вышел из радиуса, но мина продолжает отсчет
        _isPlayerInRadius = false;
    }

    private void Explode()
    {
        if (_explosionEffect != null)
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);

        List<IDamagable> damagables = _damagableManager.GetAllDamagables();

        foreach (var damagable in damagables)
        {
            float distance = Vector3.Distance(transform.position, damagable.Position);

            if (distance <= _explosionRadius)
            {
                damagable.TakeDamage(_damage);

                if (damagable.CurrentHealthPercent <= 0)
                    _damagableManager.UnregisterDamagable(damagable);
            }
        }

        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);

        if (Application.isPlaying && _isActivated)
        {
            float progress = Mathf.Clamp01(_explosionTimer / _explosionDelay);
            Gizmos.color = Color.Lerp(Color.yellow, Color.red, progress);
            Gizmos.DrawSphere(transform.position, 0.3f * progress);
        }
    }
}
