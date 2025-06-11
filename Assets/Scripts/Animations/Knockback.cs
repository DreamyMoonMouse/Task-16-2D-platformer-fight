using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Knockback : MonoBehaviour
{
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _force = 5f; 

    private Rigidbody2D _rigidbody;
    
    public bool IsKnockback { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Apply(Vector2 direction, float forceMultiplier, bool resetVelocityAfter)
    {
        if (IsKnockback) 
            return; 

        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.AddForce(direction * _force * forceMultiplier, ForceMode2D.Impulse);
        StartCoroutine(Cooldown(resetVelocityAfter));
    }

    private IEnumerator Cooldown(bool resetVelocityAfter)
    {
        IsKnockback = true;
        yield return new WaitForSeconds(_duration);
        IsKnockback = false;
        
        if (resetVelocityAfter)
            _rigidbody.linearVelocity = Vector2.zero;
    }
}
