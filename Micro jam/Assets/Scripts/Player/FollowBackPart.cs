using UnityEngine;
using System.Collections.Generic;

public class FollowBackPart : MonoBehaviour
{
    public Transform backPart;          
    public float followDistance = 0.5f; 
    public float followSpeed = 20f;     
    public float recordStep = 0.05f; 
    public float minGap = 1f;    

    List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        positions.Add(transform.position);
        if (backPart != null && positions.Count > 1)
        {
            
            int index = Mathf.FloorToInt(followDistance / recordStep);
            if (index >= positions.Count)
                index = positions.Count - 1;

            Vector3 targetPos = positions[index];

            backPart.position = Vector3.Lerp(
                backPart.position,
                targetPos,
                Time.deltaTime * followSpeed
            );

            Vector3 dir = targetPos - backPart.position;
            if (dir.sqrMagnitude > 0.0001f)
            {
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                backPart.rotation = Quaternion.Lerp(
                    backPart.rotation,
                    Quaternion.Euler(0f, 0f, angle),
                    Time.deltaTime * followSpeed * 0.5f
                );
            }
        }

        
        if (positions.Count > 3000)
            positions.RemoveRange(3000, positions.Count - 3000);
        if (backPart != null)
    {
        Vector3 toBack = backPart.position - transform.position;
        float dist = toBack.magnitude;

        if (dist < minGap && dist > 0.0001f)
        {
            // push backPart away so distance becomes exactly minGap
            Vector3 dir = toBack / dist;              // normalized
            backPart.position = transform.position + dir * minGap;
        }
    }

    if (positions.Count > 3000)
        positions.RemoveRange(3000, positions.Count - 3000);
        
    }

    void Update()
    {
        
        positions.Insert(0, transform.position);

        
        if (backPart != null && positions.Count > 1)
        {
            
            int index = Mathf.FloorToInt(followDistance / recordStep);
            if (index >= positions.Count)
                index = positions.Count - 1;

            Vector3 targetPos = positions[index];

            backPart.position = Vector3.Lerp(
                backPart.position,
                targetPos,
                Time.deltaTime * followSpeed
            );

            Vector3 dir = targetPos - backPart.position;
            if (dir.sqrMagnitude > 0.0001f)
            {
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                backPart.rotation = Quaternion.Lerp(
                    backPart.rotation,
                    Quaternion.Euler(0f, 0f, angle),
                    Time.deltaTime * followSpeed * 0.5f
                );
            }
        }

        
        if (positions.Count > 3000)
            positions.RemoveRange(3000, positions.Count - 3000);
        if (backPart != null)
    {
        Vector3 toBack = backPart.position - transform.position;
        float dist = toBack.magnitude;

        if (dist < minGap && dist > 0.0001f)
        {
            // push backPart away so distance becomes exactly minGap
            Vector3 dir = toBack / dist;              // normalized
            backPart.position = transform.position + dir * minGap;
        }
    }

    if (positions.Count > 3000)
        positions.RemoveRange(3000, positions.Count - 3000);
    }
}
