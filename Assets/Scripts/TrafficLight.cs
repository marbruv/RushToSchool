using UnityEngine;
using System.Collections;

public class TrafficLight : MonoBehaviour
{
    public Sprite redLight;
    public Sprite yellowLight;
    public Sprite greenLight;

    public float redTime = 3f;
    public float yellowTime = 1f;
    public float greenTime = 3f;

    public bool isRed = false; 

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(TrafficCycle());
    }

    IEnumerator TrafficCycle()
    {
        while (true)
        {
            // RED
            isRed = true;
            sr.sprite = redLight;
            yield return new WaitForSeconds(redTime);

            // GREEN
            isRed = false;
            sr.sprite = greenLight;
            yield return new WaitForSeconds(greenTime);

            // YELLOW
            isRed = false;
            sr.sprite = yellowLight;
            yield return new WaitForSeconds(yellowTime);
        }
    }
}
