using UnityEngine;

public class EnergyDrinkBooster : MonoBehaviour
{
    public float superSpeedAmount = 8f;
    public float duration = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ActivateEnergyDrink(superSpeedAmount, duration); // pass in the values
            Destroy(gameObject);
        }
    }
}