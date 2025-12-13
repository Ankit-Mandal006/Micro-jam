using UnityEngine;

public enum KidType
{
    Nice,
    Naughty
}

public class Enemy1 : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int damage = 1;

    public KidType kidType;   

    Transform player;
    EnemyHealth hp;
    SpriteRenderer sr;

    void Start()
    {
       
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        hp = GetComponent<EnemyHealth>();
        sr = GetComponent<SpriteRenderer>();

        
        kidType = (Random.value < 0.5f) ? KidType.Nice : KidType.Naughty;

        
        if (sr != null)
        {
            if (kidType == KidType.Nice)
            {
                sr.color = Color.green;                           
            }
            else 
            {
                sr.color = new Color(0f, 0.3f, 0f, 1f);         
            }
        }
    

        
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
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
                ph.TakeDamage(damage);

            Destroy(gameObject);
        }

        
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                bool shouldTakeDamage = false;

                
                if (kidType == KidType.Nice && bullet.bulletType == BulletType.Gift)
                    shouldTakeDamage = true;
                
                else if (kidType == KidType.Naughty && bullet.bulletType == BulletType.Coal)
                    shouldTakeDamage = true;

                if (shouldTakeDamage && hp != null)
                {
                    hp.TakeDamage(1);
                }
            }

            Destroy(other.gameObject); 
        }
    }
}
