using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;            // El jugador
    public Vector3 offset = new Vector3(0, 3, -5);  // Posición detrás y arriba del jugador
    public float smoothSpeed = 0.125f;  // Suavidad del seguimiento

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + target.rotation * offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
        transform.LookAt(target);  // Mira al jugador
    }
}
