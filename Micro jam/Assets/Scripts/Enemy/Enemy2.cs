using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Contact Damage")]
    public int touchDamage = 1;

    [Header("Shooting")]
    public GameObject enemyBulletPrefab;
    public float shootInterval = 2f;      // seconds between shots
    public float bulletSpeed = 8f;

    [Header("Kid Type / Score")]
    public KidType kidType;
    public PlayerScore playerScore;
    public GameObject particle;

    Transform player;
    EnemyHealth hp;
    SpriteRenderer sr;
    float shootTimer;

    void Start()
    {
        // refs
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerScore = playerObj.GetComponent<PlayerScore>();
        }

        hp = GetComponent<EnemyHealth>();
        sr = GetComponent<SpriteRenderer>();

        // random kid type
        kidType = (Random.value < 0.5f) ? KidType.Nice : KidType.Naughty;

        // color by type
        if (sr != null)
        {
            if (kidType == KidType.Nice)
            {
                particle.SetActive(false);
            }
            else 
            {
                sr.color = new Color(0.05f, 0.15f, 0.05f, 1f);
                particle.SetActive(true);        
            }
        }

        shootTimer = shootInterval;
    }

    void Update()
    {
        if (player == null) return;

        // move toward player
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;

        // look at player (optional)
        if (dir.sqrMagnitude > 0.0001f)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg+90;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        
        // shooting timer
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            ShootAtPlayer();
            shootTimer = shootInterval;
        }
    }

    void ShootAtPlayer()
    {
        if (enemyBulletPrefab == null || player == null) return;

        Vector3 dir = (player.position - transform.position).normalized;

        GameObject b = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);

        // if you use the same Bullet script, you can set its type here
        Bullet bullet = b.GetComponent<Bullet>();
        if (bullet != null)
        {
            // for example, enemy always shoots Coal bullets
            bullet.bulletType = BulletType.Coal;
        }

        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = dir * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        // touch player → damage
        if (other.CompareTag("Player"))
        {
            AudioSource audioSource=GameObject.Find("EnemyHit").GetComponent<AudioSource>();
            audioSource.Play();
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
                ph.TakeDamage(touchDamage);

            Destroy(gameObject);
        }

        // hit by player's bullet → apply same kid-type scoring rules
        if (other.CompareTag("Bullet"))
        {
            AudioSource audioSource=GameObject.Find("EnemyHit").GetComponent<AudioSource>();
            audioSource.Play();
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                bool shouldTakeDamage = false;

                if (kidType == KidType.Nice && bullet.bulletType == BulletType.Gift)
                {
                    shouldTakeDamage = true;
                    playerScore?.UpdateScore(4);
                }
                else if (kidType == KidType.Naughty && bullet.bulletType == BulletType.Coal)
                {
                    shouldTakeDamage = true;
                    playerScore?.UpdateScore(4);
                }
                else if (kidType == KidType.Nice && bullet.bulletType == BulletType.Coal)
                {
                    shouldTakeDamage = true;
                    playerScore?.UpdateScore(-1);
                }
                else if (kidType == KidType.Naughty && bullet.bulletType == BulletType.Gift)
                {
                    shouldTakeDamage = true;
                    playerScore?.UpdateScore(-1);
                }

                if (shouldTakeDamage && hp != null)
                    hp.TakeDamage(1);
            }

            Destroy(other.gameObject); // remove player bullet
        }
    }
}
