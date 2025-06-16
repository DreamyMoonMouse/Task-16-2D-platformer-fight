using UnityEngine;

[RequireComponent(typeof(CharacterDetector))]
public class VampirismAbility : MonoBehaviour
{
    [SerializeField] private float _activationDuration = 6f;
    [SerializeField] private float _cooldownDuration = 4f;
    [SerializeField] private float _damagePerSecond = 10f;
    [SerializeField] private SpriteRenderer _radiusVisualizer;

    private Health _playerHealth;
    private CharacterDetector _detector;
    private float _remainingTime;
    private float _cooldownRemaining;
    private bool _isActivated;
    private bool _isOnCooldown;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
        _detector = GetComponent<CharacterDetector>();
    }

    private void Update()
    {
        if (_isActivated)
        {
            _remainingTime -= Time.deltaTime;
            VampirismUI.Instance.UpdateTimer(_remainingTime / _activationDuration);

            if (_remainingTime <= 0f)
            {
                Deactivate();
            }
            else
            {
                ApplyVampirism();
            }
        }
        else if (_isOnCooldown)
        {
            _cooldownRemaining -= Time.deltaTime;
            VampirismUI.Instance.UpdateCooldown(_cooldownRemaining / _cooldownDuration);

            if (_cooldownRemaining <= 0f)
            {
                _isOnCooldown = false;
            }
        }
    }

    public void Activate()
    {
        if (!_isActivated && !_isOnCooldown)
        {
            _isActivated = true;
            _remainingTime = _activationDuration;
            _radiusVisualizer.enabled = true;
            VampirismUI.Instance.Show();
        }
    }

    private void ApplyVampirism()
    {
        ITargetable target = _detector.DetectNearestTarget();
        if (target == null) return;

        int damage = Mathf.CeilToInt(_damagePerSecond * Time.deltaTime);
        Health targetHealth = target.GetTransform().GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.ApplyDamage(damage);
            _playerHealth.ApplyHeal(damage);
        }
    }

    private void Deactivate()
    {
        _isActivated = false;
        _radiusVisualizer.enabled = false;
        _isOnCooldown = true;
        _cooldownRemaining = _cooldownDuration;
        VampirismUI.Instance.Hide();
    }

    public bool IsReady() => !_isActivated && !_isOnCooldown;
}
