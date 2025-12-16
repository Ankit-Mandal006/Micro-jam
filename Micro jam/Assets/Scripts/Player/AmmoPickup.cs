using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 10;

    public AudioSource audioSource;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource=GameObject.Find("AmmoSound").GetComponent<AudioSource>();
            audioSource.Play();
            PlayerAmmo ammo = GameObject.Find("Santa").GetComponent<PlayerAmmo>();
            if (ammo != null)
            {
                ammo.AddAmmo(ammoAmount);
            }

            Destroy(gameObject);
        }
    }
}
