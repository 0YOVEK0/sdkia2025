using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera mainCamera;

    private void Update()
    {
        if (mainCamera != null)
        {
            // Hace que el objeto siempre mire hacia la cámara
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                             mainCamera.transform.rotation * Vector3.up);
        }
    }
}
