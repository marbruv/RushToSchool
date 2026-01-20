using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public GameController gameController;

    [Header("Obstacle Sprites")]
    public Sprite treeSprite;
    public Sprite pedestrianSprite;
    public Sprite riderSprite;
    public Sprite carSprite;
    public Sprite lightSprite;

    [Header("Obstacle SFX")]
    public AudioClip treeSfx;
    public AudioClip pedestrianSfx;
    public AudioClip riderSfx;
    public AudioClip carSfx;

    private PlayerController playerController;
    private CameraFollow cameraFollow;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Obstacle obstacle = collision.collider.GetComponentInParent<Obstacle>();

        if (obstacle == null)
            return;

        if (playerController != null && playerController.TryUseHelmet())
        {
            cameraFollow.StartShake(0.2f, 0.2f);
            return;
        }

        Sprite sprite = null;
        AudioClip sfx = null;

        switch (obstacle.type)
        {
            case Obstacle.ObstacleType.Tree:
                sprite = treeSprite;
                sfx = treeSfx;
                break;

            case Obstacle.ObstacleType.Pedestrian:
                sprite = pedestrianSprite;
                sfx = pedestrianSfx;
                break;

            case Obstacle.ObstacleType.Rider:
                sprite = riderSprite;
                sfx = riderSfx;
                break;

            case Obstacle.ObstacleType.Car:
                sprite = carSprite;
                sfx = carSfx;
                break;

            case Obstacle.ObstacleType.TrafficLight:
                sprite = lightSprite;
                sfx = riderSfx;
                break;
        }

        gameController.PlayerHitObstacle(sprite, sfx);
    }
}
