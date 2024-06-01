using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{

    public float minX, maxX, minZ, maxZ;

    void Update()
    {
        // Clamp player position to the defined boundaries
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minZ, maxZ);
        transform.position = clampedPosition;
    }


}
