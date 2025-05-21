using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

[RequireComponent(typeof(Health))]
public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _smoothHealthBar;
    [SerializeField] private float _smoothSpeed = 50f;
    [SerializeField] private Button _damageButton;
    [SerializeField] private Button _healButton;
    [SerializeField] private int _damageAmount = 10;
    [SerializeField] private int _healAmount = 10;

    private Health _health;
    private Coroutine _smoothCoroutine;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _damageButton.onClick.AddListener(() => OnDamageButton(_damageAmount));
        _healButton.onClick.AddListener(() => OnHealButton(_healAmount));
    }

    private void Start()
    {
        UpdateInstantUI();
        _smoothHealthBar.maxValue = _health.MaxHealth;
        _smoothHealthBar.value = _health.CurrentHealth;
    }
    
    private void UpdateInstantUI()
    {
        int currentHealth = _health.CurrentHealth;
        int maxHealth = _health.MaxHealth;
        _healthText.text = $"{currentHealth}/{maxHealth}";
        _healthBar.maxValue = maxHealth;
        _healthBar.value = currentHealth;
    }
    
    private void AnimateSmoothBar(int targetValue)
    {
        if (_smoothCoroutine != null)
            StopCoroutine(_smoothCoroutine);

        _smoothCoroutine = StartCoroutine(SmoothBarRoutine(targetValue));
    }

    private IEnumerator SmoothBarRoutine(int targetValue)
    {
        while (Mathf.Approximately(_smoothHealthBar.value, targetValue) == false)
        {
            _smoothHealthBar.value = Mathf.MoveTowards(_smoothHealthBar.value,
                targetValue, _smoothSpeed * Time.deltaTime);
            
            yield return null;
        }
    }
    
    private void OnDamageButton(int damageAmount)
    {
        _health.TakeDamage(damageAmount);
        UpdateInstantUI();
        AnimateSmoothBar(_health.CurrentHealth);
    }

    private void OnHealButton(int healAmount)
    {
        _health.Heal(healAmount);
        UpdateInstantUI();
        AnimateSmoothBar(_health.CurrentHealth);
    }
}
