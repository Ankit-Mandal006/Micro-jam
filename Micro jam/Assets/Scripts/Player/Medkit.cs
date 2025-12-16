using UnityEngine;

public class Medkit : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only restore when PLAYER hits it
        if (other.CompareTag("Player"))
        {
            audioSource=GameObject.Find("Heal").GetComponent<AudioSource>();
            audioSource.Play();
            PlayerHealth.Instance.RestoreFullHealth();
            Destroy(gameObject); // remove medkit after use
        }
    }
}
