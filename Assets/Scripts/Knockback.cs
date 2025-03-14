using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Knockback : MonoBehaviour
{
    [SerializeField] private float _knockbackDuration = 0.5f;
    [SerializeField] private float _knockbackForce = 5f; 

    private Rigidbody2D _rigidbody;
    private bool _isKnockback = false;
    
    public bool IsKnockback => _isKnockback; 

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Apply(Vector2 direction, float forceMultiplier, bool resetVelocityAfter)
    {
        if (_isKnockback) return; 

        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.AddForce(direction * _knockbackForce * forceMultiplier, ForceMode2D.Impulse);
        StartCoroutine(Cooldown(resetVelocityAfter));
    }
    
    public void Apply(Vector2 direction, bool resetVelocityAfter)
    {
        Apply(direction, 1f, resetVelocityAfter);
    }

    private IEnumerator Cooldown(bool resetVelocityAfter)
    {
        _isKnockback = true;
        yield return new WaitForSeconds(_knockbackDuration);
        _isKnockback = false;
        
        if (resetVelocityAfter)
        {
            _rigidbody.linearVelocity = Vector2.zero;
        }
    }
}
