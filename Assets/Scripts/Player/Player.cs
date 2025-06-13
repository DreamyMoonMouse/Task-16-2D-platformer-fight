using UnityEngine;

public class Player : MonoBehaviour, ITargetable 
{
    public Transform GetTransform() => transform;
}
