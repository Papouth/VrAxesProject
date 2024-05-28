using UnityEngine;

public class VFXOptimiser : MonoBehaviour
{
    [SerializeField] private float timer = 5f;

    void Start()
    {
        Invoke("DeathEffect", timer);
    }

    private void DeathEffect()
    {
        Destroy(gameObject);
    }
}