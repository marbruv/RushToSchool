using UnityEngine;

public class HelmetBooster : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ActivateHelmet();
                Destroy(gameObject);
            }
        }
    }
}