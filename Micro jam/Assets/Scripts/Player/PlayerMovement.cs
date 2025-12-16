using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float dashSpeed = 15f;
    public float dashTime = 0.15f;
    public float turnLerp = 10f;

    // ✅ ADDED
    public Vector2 minBounds;
    public Vector2 maxBounds;

    bool isDashing;
    float dashTimer;

    Vector3 moveDir = Vector3.up;

    void Update()
    {
        float x = 0f, y = 0f;
        if (Input.GetKey(KeyCode.A)) x -= 1f;
        if (Input.GetKey(KeyCode.D)) x += 1f;
        if (Input.GetKey(KeyCode.S)) y -= 1f;
        if (Input.GetKey(KeyCode.W)) y += 1f;

        Vector3 inputDir = new Vector3(x, y, 0f);

        if (inputDir.sqrMagnitude > 0.001f)
        {
            inputDir.Normalize();
            moveDir = Vector3.Lerp(moveDir, inputDir, Time.deltaTime * turnLerp);
            moveDir.Normalize();
        }

        float speed = isDashing ? dashSpeed : moveSpeed;
        transform.position += moveDir * speed * Time.deltaTime;

        if (Input.GetMouseButtonDown(1))
        {
            isDashing = true;
            dashTimer = dashTime;
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f) isDashing = false;
        }

        if (moveDir.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0f, 0f, angle),
                Time.deltaTime * turnLerp
            );
        }

        // ✅ CLAMP — MUST BE LAST
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
        transform.position = pos;
    }

    public void StartSpeedBoost(float amount, float duration)
    {
        moveSpeed = 12f;
        CancelInvoke();
        Invoke(nameof(ResetSpeed), duration);
    }

    void ResetSpeed()
    {
        moveSpeed = 6f;
    }
}
