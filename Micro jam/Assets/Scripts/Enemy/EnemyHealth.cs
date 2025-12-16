using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    public int health = 1;

    public void TakeDamage(int amount)
    {
        //audioSource=this.GetComponent<AudioSource>();
        health -= amount;
        
        if (health <= 0)
            Destroy(gameObject);
    }
}
