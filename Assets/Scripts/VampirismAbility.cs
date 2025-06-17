using UnityEngine;

[RequireComponent(typeof(CharacterDetector))]
public class VampirismAbility : MonoBehaviour, ISkill
{
    [SerializeField] private float _damagePerSecond = 10f;
    [SerializeField] private float _maxDamage = 60f;  
    [SerializeField] private SpriteRenderer _radiusVisualizer;

    private Health _playerHealth;
    private CharacterDetector _detector;
    private float _accumulatedDamage = 0f; 
    private float _totalDamageDealt = 0f;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
        _detector = GetComponent<CharacterDetector>();
    }

    public void Activate()
    {
        _radiusVisualizer.enabled = true;
        _accumulatedDamage = 0f;
        _totalDamageDealt = 0f;
    }

    public void Deactivate()
    {
        _radiusVisualizer.enabled = false;
    }

    public void Apply()
    {
        ITargetable target = _detector.DetectNearestTarget();

        if (target == null || target.GetTransform() == transform)
        {
            Debug.Log("Нет подходящей цели для вампиризма");
            return;
        }

        Debug.Log($"Применяем вампиризм к {target.GetTransform().name}");
        
        if (_totalDamageDealt >= _maxDamage)
        {
            Debug.Log("Достигнут лимит высасываемого здоровья");
            return;
        }
        
        float damageThisFrame = _damagePerSecond * Time.deltaTime;
        _accumulatedDamage += damageThisFrame;
        
        if (_accumulatedDamage >= 1f)
        {
            int damageToApply = Mathf.FloorToInt(_accumulatedDamage);
            _accumulatedDamage -= damageToApply;
            
            if (_totalDamageDealt + damageToApply > _maxDamage)
            {
                damageToApply = Mathf.FloorToInt(_maxDamage - _totalDamageDealt);
            }

            Health targetHealth = target.GetTransform().GetComponent<Health>();
            
            if (targetHealth != null)
            {
                targetHealth.ApplyDamage(damageToApply);
                _playerHealth.ApplyHeal(damageToApply);
                _totalDamageDealt += damageToApply; 
                Debug.Log($"Нанесено {damageToApply} урона и вылечено игроку, всего нанесено {_totalDamageDealt}");
            }
        }
    }
}
