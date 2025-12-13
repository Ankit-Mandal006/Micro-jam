using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    
    public float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
