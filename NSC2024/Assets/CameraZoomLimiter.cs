using UnityEngine;
using Cinemachine;

public class CameraZoomLimiter : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform player1;
    public Transform player2;
    public float maxZoomOutDistance = 10f;
    public float minOrthographicSize = 5f;
    public float maxOrthographicSize = 15f;

    void Update()
    {
        if (virtualCamera != null && player1 != null && player2 != null)
        {
            float distance = Vector3.Distance(player1.position, player2.position);
            Debug.Log("Distance between players: " + distance);

            // Ensure distance is clamped to avoid excessive zoom
            distance = Mathf.Clamp(distance, 0, maxZoomOutDistance);
            Debug.Log("Clamped distance: " + distance);

            // Calculate orthographic size based on distance
            float t = Mathf.InverseLerp(0, maxZoomOutDistance, distance);
            float orthographicSize = Mathf.Lerp(minOrthographicSize, maxOrthographicSize, t);
            Debug.Log("Calculated orthographic size: " + orthographicSize);

            virtualCamera.m_Lens.OrthographicSize = orthographicSize;
        }
    }
}