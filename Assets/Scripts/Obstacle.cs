using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public enum ObstacleType
    {
        Tree,
        Pedestrian,
        Rider,
        Car,
        TrafficLight
    }

    [Header("Obstacle type")]
    public ObstacleType type;
}
