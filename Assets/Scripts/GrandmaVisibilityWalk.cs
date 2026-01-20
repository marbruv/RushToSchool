using UnityEngine;

public class GrandmaVisibilityWalk : MonoBehaviour
{
    public Camera MainCamera;
    public float speed = 2f;

    void Update()
    {
        if (IsVisibleToCamera(MainCamera))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    bool IsVisibleToCamera(Camera cam)
    {
        Vector3 viewportPos = cam.WorldToViewportPoint(transform.position);

        return viewportPos.x >= 0 && viewportPos.x <= 1 &&
               viewportPos.y >= 0 && viewportPos.y <= 1 &&
               viewportPos.z > 0;
    }
}