using UnityEngine;
using TMPro;

public class ShootProjectile : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 12f;
    public float fireRate = 0.2f;

    float nextFire;
    PlayerAmmo ammo;

    public BulletType currentBulletType = BulletType.Gift;

    public TMP_Text bulletTypeText;   // assign in Inspector

    void Awake()
    {
        ammo = GetComponentInParent<PlayerAmmo>();
        UpdateBulletTypeUI();
    }

    void Update()
    {
        HandleScrollSwitch();

        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            if (ammo == null || !ammo.TryShoot())
                return;

            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void HandleScrollSwitch()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f || scroll < 0f)
        {
            currentBulletType = (currentBulletType == BulletType.Gift)
                ? BulletType.Coal
                : BulletType.Gift;

            UpdateBulletTypeUI();
        }
    }

    void UpdateBulletTypeUI()
    {
        if (bulletTypeText == null) return;

        switch (currentBulletType)
        {
            case BulletType.Gift:
                bulletTypeText.text = "Gift";
                break;
            case BulletType.Coal:
                bulletTypeText.text = "Coal";
                break;
        }
    }

    void Shoot()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        Vector3 dir = (mouse - transform.position).normalized;

        GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Bullet bullet = b.GetComponent<Bullet>();
        if (bullet != null)
            bullet.bulletType = currentBulletType;

        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        rb.linearVelocity = dir * bulletSpeed;
    }
}
