using UnityEngine;

public class FollowRoad : MonoBehaviour
{
    public Transform[] points;
    public float speed = 2f;
    int index = 0;

    void Update()
    {
        if (points.Length == 0) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            points[index].position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, points[index].position) < 0.1f)
        {
            index++;

            if (index >= points.Length)
            {
                Destroy(gameObject);   // remove the character
                return;
            }
        }
    }
}