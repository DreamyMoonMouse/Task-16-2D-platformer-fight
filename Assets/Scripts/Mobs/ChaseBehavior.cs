using UnityEngine;

public class ChaseBehavior : MonoBehaviour, IMobBehavior
{
    [SerializeField] private Transform _player;
    [SerializeField] private Mover _mover;
    [SerializeField] private float _stopDistance = 0.8f;
    
    public void Execute()
    {
        Chase();
    }

    private void Chase()
    {
        if (_player != null)
        {
            float distance = Vector2.Distance(transform.position, _player.position);
            
            if (distance > _stopDistance)
            {
                _mover.MoveTo(_player.position);
            }
            else
            {
                _mover.Stop();
            }
        }
    }
}
