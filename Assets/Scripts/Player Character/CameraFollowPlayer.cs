using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                // The player’s transform
    public float smoothSpeed = 0.125f;      // Camera follow smoothness
    public Vector3 offset;                  // Offset from the player

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;

            // Lock the camera's Z-axis (since it's 2D)
            desiredPosition.z = transform.position.z;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}

