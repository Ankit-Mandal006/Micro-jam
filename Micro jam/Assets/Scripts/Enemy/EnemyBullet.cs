using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float lifeTime = 5f;   
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = GameObject.Find("Player").GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
            }

            Destroy(gameObject);
        }

        
        /*if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }*/
    }
}
