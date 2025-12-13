using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerAmmo ammo = GameObject.Find("Santa").GetComponent<PlayerAmmo>();
            if (ammo != null)
            {
                ammo.AddAmmo(ammoAmount);
            }

            Destroy(gameObject);
        }
    }
}
