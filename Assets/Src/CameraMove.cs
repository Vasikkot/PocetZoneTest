using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 0.125f; 

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position;
        Vector3 smoothedPosition = Vector3.Lerp(new Vector3( transform.position.x, transform.position.y, -10), desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        
    }
}