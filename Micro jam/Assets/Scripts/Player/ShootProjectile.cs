using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootingPoint;
    public float bulletSpeed = 12f;
    public float fireRate = 0.2f;

    float nextFire;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        Vector3 dir = (mouse - transform.position).normalized;

        GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        rb.linearVelocity = dir * bulletSpeed;
    }
}
