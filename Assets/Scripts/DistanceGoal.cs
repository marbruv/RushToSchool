using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceToGoalProgress : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject goal;
    public Slider progressBar;      // Progress bar from 0 to 1
    public RectTransform icon;     
    public TMP_Text distanceText;   

    private Collider2D goalCollider;
    private float initialDistance;

    void Start()
    {
        goalCollider = goal.GetComponent<Collider2D>();
        if (goalCollider == null)
        {
            Debug.LogError("The goal object needs a Collider2D to calculate the bottom line.");
            return;
        }

        // Calculate the initial distance at the start of the game
        initialDistance = CalculateDistance();
        UpdateProgress(); // Update the progress bar from the start
    }

    void Update()
    {
        UpdateProgress();
    }

    float CalculateDistance()
    {
        // Bottom line of the goal
        Bounds bounds = goalCollider.bounds;
        Vector3 bottomLeft = new Vector3(bounds.min.x, bounds.min.y, 0);
        Vector3 bottomRight = new Vector3(bounds.max.x, bounds.min.y, 0);

        Vector3 p = player.position;

        // Project the player position onto the bottom line
        Vector3 ap = p - bottomLeft;
        Vector3 ab = bottomRight - bottomLeft;

        float t = Mathf.Clamp01(Vector3.Dot(ap, ab) / ab.sqrMagnitude);
        Vector3 closestPoint = bottomLeft + ab * t;

        // Distance to the closest point
        float distance = Vector3.Distance(p, closestPoint);

        // Display distance in the UI
        if (distanceText != null)
            distanceText.text = Mathf.RoundToInt(distance) + "m";

        return distance;
    }

    void UpdateProgress()
    {
        float currentDistance = CalculateDistance();

        // Normalize between 0 (goal reached) and 1 (start)
        float normalized = Mathf.Clamp01(1 - currentDistance / initialDistance);

        // Update progress bar
        if (progressBar != null)
            progressBar.value = normalized;

        // Update icon position if it exists
        if (icon != null && progressBar != null)
        {
            RectTransform fillArea = progressBar.fillRect.parent.GetComponent<RectTransform>();
            float width = fillArea.rect.width;

            Vector3 pos = icon.localPosition;
            pos.x = normalized * width;
            icon.localPosition = pos;
        }
    }
}
