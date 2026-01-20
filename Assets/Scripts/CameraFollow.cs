using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;               // Player transform reference
    private Rigidbody2D rb;               // Player Rigidbody reference

    [Header("Follow Settings")]
    public float smoothTime = 0.25f;      // Camera smoothing time
    private Vector3 velocity = Vector3.zero;

    [Header("Zoom Settings")]
    public float baseZoom = 5f;           // Normal zoom level
    public float maxZoomOut = 8f;         // Furthest zoom when player is fast
    public float zoomSpeed = 5f;          // Zoom smoothness
    public float speedToMaxZoom = 10f;    // Player speed needed to hit max zoom

    [Header("Camera Offset")]
    public Vector3 offset = new Vector3(0f, 2f, 0f);  // Adjust Y to move camera up/down

    [Header("Shake Settings")]
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.2f;

    private float shakeTimer = 0f;
    private Vector3 originalPos;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        rb = target.GetComponent<Rigidbody2D>();
    }

    public void StartShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        shakeTimer = duration;
    }

    void LateUpdate()
    {
        FollowPlayer();
        HandleZoom();
        HandleCameraShake();
    }

    void FollowPlayer()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z) + offset;
        
        // Smooth camera follow
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    void HandleZoom()
    {
        float playerSpeed = rb.linearVelocity.magnitude;

        // Map speed to zoom level
        float targetZoom = Mathf.Lerp(baseZoom, maxZoomOut, playerSpeed / speedToMaxZoom);

        // Smooth zoom
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
    }

    void HandleCameraShake()
    {
        if (shakeTimer > 0)
        {
            if (shakeTimer == shakeDuration)
                originalPos = transform.position;

            transform.position = originalPos + (Vector3)Random.insideUnitCircle * shakeMagnitude;

            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0)
            {
                transform.position = originalPos; // Reset position
            }
        }
    }
}