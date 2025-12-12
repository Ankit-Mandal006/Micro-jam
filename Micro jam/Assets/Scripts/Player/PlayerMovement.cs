using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float dashSpeed = 15f;
    public float dashTime = 0.15f;

    public float rotationSpeed = 720f; // degrees per second

    bool isDashing;
    float dashTimer;

    void Update()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        Vector3 dir = (mouse - transform.position).normalized;

        // movement
        if (!isDashing)
            transform.position += dir * moveSpeed * Time.deltaTime;

        if (Input.GetMouseButtonDown(1))
        {
            isDashing = true;
            dashTimer = dashTime;
        }

        if (isDashing)
        {
            transform.position += dir * dashSpeed * Time.deltaTime;
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
                isDashing = false;
        }

        // rotation towards mouse
        if (dir.sqrMagnitude > 0.0001f)
        {
            float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;   // angle for 2D topdown[web:33][web:186]
            Quaternion targetRot = Quaternion.Euler(0f, 0f, targetAngle);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRot,
                rotationSpeed * Time.deltaTime
            );                                                                // smooth rotate[web:27][web:193]
        }
    }
}
