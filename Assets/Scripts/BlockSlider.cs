using UnityEngine;
using UnityEngine.UI;

public class ProgressBarWithIcon : MonoBehaviour
{
    public Slider slider;        
    public RectTransform icon;   

    void UpdateIconPosition()
    {
        if (icon == null || slider == null) return;

        // Get the total width of the slider's fill area
        RectTransform fillArea = slider.fillRect.parent.GetComponent<RectTransform>();
        float width = fillArea.rect.width;

        // Move the icon based on the slider's current value
        Vector3 pos = icon.localPosition;
        pos.x = slider.value * width;
        icon.localPosition = pos;
    }

    // Update the slider value and the icon position
    public void SetProgress(float value)
    {
        slider.value = value;
        UpdateIconPosition();
    }
}
