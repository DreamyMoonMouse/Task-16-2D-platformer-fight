using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public void Collect()
    {
        gameObject.SetActive(false);
    }
}
