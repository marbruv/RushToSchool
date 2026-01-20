using UnityEngine;
using UnityEngine.UI;

public class UIKeyboardHints : MonoBehaviour
{
    [Header("UI Images")]
    public Image up;
    public Image down;
    public Image left;
    public Image right;
    public Image space;

    [Header("Alpha")]
    [Range(0f, 1f)] public float alphaOff = 100f / 255f; // ~0.392
    [Range(0f, 1f)] public float alphaOn = 1f;

    void Awake()
    {
        // Initial state: all of
        SetAlpha(up, alphaOff);
        SetAlpha(down, alphaOff);
        SetAlpha(left, alphaOff);
        SetAlpha(right, alphaOff);
        SetAlpha(space, alphaOff);
    }

    void Update()
    {
   
        SetAlpha(up, IsUpPressed() ? alphaOn : alphaOff);
        SetAlpha(down, IsDownPressed() ? alphaOn : alphaOff);
        SetAlpha(left, IsLeftPressed() ? alphaOn : alphaOff);
        SetAlpha(right, IsRightPressed() ? alphaOn : alphaOff);

     
        SetAlpha(space, Input.GetKey(KeyCode.Space) ? alphaOn : alphaOff);
    }

    bool IsUpPressed()
        => Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);

    bool IsDownPressed()
        => Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

    bool IsLeftPressed()
        => Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);

    bool IsRightPressed()
        => Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

    static void SetAlpha(Image img, float a)
    {
        if (!img) return;

        var c = img.color;
        c.a = a;
        img.color = c;
    }
}
