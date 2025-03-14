using UnityEngine;

public class HealthPack : MonoBehaviour
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

    public void ResetHealthPack()
    {
        transform.position = _initialPosition;
        gameObject.SetActive(true);
    }
}
