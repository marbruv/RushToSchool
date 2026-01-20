using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class BikeLaneChecker : MonoBehaviour
{
    public Tilemap bikeLaneTilemap;
    public Tilemap grassOutline;

    [Range(0f, 1f)] public float offLaneSpeedMultiplier = 0.25f; // e.g., half speed off-lane
    [HideInInspector] public bool onLane = true;  // true if on the bike lane
    [HideInInspector] public bool onGrass = true;  // true if on the grass field

    private PlayerController controller;

    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        // Get the cell under the player
        Vector3Int cellPos = bikeLaneTilemap.WorldToCell(transform.position);
        TileBase tile = bikeLaneTilemap.GetTile(cellPos);

        // Player is on lane if tile exists
        onLane = tile != null;

        // Get the cell under the player
        Vector3Int cellPos1 = grassOutline.WorldToCell(transform.position);
        TileBase tile1 = grassOutline.GetTile(cellPos1);

        // Player is on grass if tile exists
        onGrass = tile1 != null;
    }
}