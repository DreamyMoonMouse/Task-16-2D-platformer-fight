using UnityEngine;

public class HealthPack : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        gameObject.SetActive(false);
    }
}
