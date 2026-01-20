using UnityEngine;

public class CarDrive : MonoBehaviour
{
    public Camera MainCamera;
    public TrafficLight trafficLight;   
    public float speed = 3f;

    // How early should the car start driving (0.1 = 10% out of the screen)
    public float earlyStartMargin = 0.2f;

    void Update()
    {
        if (trafficLight.isRed && IsAlmostVisibleToCamera(MainCamera))
        {
            // Drives left
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    bool IsAlmostVisibleToCamera(Camera cam)
    {
        Vector3 viewportPos = cam.WorldToViewportPoint(transform.position);

        
        return viewportPos.x >= -earlyStartMargin && viewportPos.x <= 1 + earlyStartMargin &&
               viewportPos.y >= -earlyStartMargin && viewportPos.y <= 1 + earlyStartMargin &&
               viewportPos.z > 0;
    }
}