using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float dashSpeed = 15f;
    public float dashTime = 0.15f;

    bool isDashing;
    float dashTimer;

    void Update()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        Vector3 dir = (mouse - transform.position).normalized;

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
    }
}
