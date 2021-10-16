using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSupport : MonoBehaviour
{
    private Camera gameCamera;

    private Bounds wrldBound;

    void Start()
    {
        gameCamera = gameObject.GetComponent<Camera>();
        wrldBound = new Bounds();

        Vector3 cam = gameCamera.transform.position;
        cam.z = 0.0f;
        wrldBound.center = cam;

        float maxY = gameCamera.orthographicSize;
        float maxX = gameCamera.orthographicSize * gameCamera.aspect;
        float sizeX = 2 * maxX;
        float sizeY = 2 * maxY;
        wrldBound.size = new Vector3(sizeX, sizeY, 1f);

        Debug.Log("World Bound: " + wrldBound);
    }


    void Update()
    {
        
    }
    public bool isInside(Bounds firstBound)
    {
        return isInsideBounds(firstBound, wrldBound);
    }

    public bool isInsideBounds(Bounds firstBound, Bounds secondBound)
    {
        return (firstBound.min.x < secondBound.max.x) && (firstBound.max.x > secondBound.min.x) && (firstBound.min.y < secondBound.max.y) && (firstBound.max.y > secondBound.min.y);
    }
    public Bounds GetWorldBound() { 
        return wrldBound; 
    }

}
