using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float speedBoost = 50f; // max speed
    public float boostDuration = 10f; // seconds

    public AudioSource audioSource;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource=GameObject.Find("PickupPowerUp").GetComponent<AudioSource>();
            audioSource.Play();
            PlayerMovement player = GameObject.Find("PlayerController").GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.StartSpeedBoost(speedBoost, boostDuration);
            }

            Destroy(gameObject); // remove the bell
        }
    }
}
