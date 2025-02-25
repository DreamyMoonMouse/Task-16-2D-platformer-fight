using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage = 1;

    public int Damage => _damage;
}
