using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        gameObject.SetActive(false);
    }
}