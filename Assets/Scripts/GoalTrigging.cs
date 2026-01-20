using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public GameController gameController;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameController.PlayerReachesGoal();
        }
    }
}
