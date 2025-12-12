using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int damage = 1;

    Transform player;
    EnemyHealth hp;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hp = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (player == null) return;

        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (other.CompareTag("Bullet"))
        {
            hp.TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}
