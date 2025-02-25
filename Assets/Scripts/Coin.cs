using UnityEngine;

public class Coin : MonoBehaviour
{
    private Vector3 _initialPosition;

    private void Awake()
    {
        _initialPosition = transform.position;
    }
    
    public void Collect()
    {
        gameObject.SetActive(false);
    }
    
    public void ResetCoin()
    {
        transform.position = _initialPosition;
        gameObject.SetActive(true);
    }
}