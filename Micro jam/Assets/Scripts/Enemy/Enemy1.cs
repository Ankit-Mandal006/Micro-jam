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
    public PlayerScore playerScore;
    public GameObject particle;

    Transform player;
    EnemyHealth hp;
    SpriteRenderer sr;

    void Start()
    {
       playerScore= GameObject.Find("Player").GetComponent<PlayerScore>();
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
                particle.SetActive(false);
            }
            else 
            {
                sr.color = new Color(0.05f, 0.15f, 0.05f, 1f);
                particle.SetActive(true);        
            }
        }
    

        
    }

    void Update()
{
    if (player == null) return;

    // move toward player
    Vector3 dir = (player.position - transform.position).normalized;
    transform.position += dir * moveSpeed * Time.deltaTime;

    // rotate to face player
    if (dir.sqrMagnitude > 0.0001f)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg+90;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}


    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            AudioSource audioSource=GameObject.Find("EnemyHit").GetComponent<AudioSource>();
            audioSource.Play();
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
                ph.TakeDamage(damage);

            Destroy(gameObject);
        }

        
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
                        playerScore.UpdateScore(4);
                }
                
                else if (kidType == KidType.Naughty && bullet.bulletType == BulletType.Coal)
                {
                        shouldTakeDamage = true;
                        playerScore.UpdateScore(4);
                }
                else if(kidType == KidType.Nice && bullet.bulletType == BulletType.Coal)
                {
                        shouldTakeDamage = true;
                        playerScore.UpdateScore(-1);
                }
                else if(kidType == KidType.Naughty && bullet.bulletType == BulletType.Gift)
                {
                        shouldTakeDamage = true;
                        playerScore.UpdateScore(-1);
                }

                if (shouldTakeDamage && hp != null)
                {
                    hp.TakeDamage(1);
                }
            }

            Destroy(other.gameObject); 
        }
    }
}
