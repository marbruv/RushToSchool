using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoostUI : MonoBehaviour
{
    public TextMeshProUGUI boostText;
    public Image boostFillImage;
    public Image boostIconBackground;

    public float pulseSpeed = 2f;
    public float minScale = 0.8f;
    public float maxScale = 1.2f;

    private bool isActive = false;

    void Start()
    {
        if (boostFillImage != null)
            boostFillImage.fillAmount = 0f;
        if (boostIconBackground != null)
            boostIconBackground.color = new Color(1f, 1f, 1f, 0.2f);
        if (boostText != null)
            boostText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Text that pulsates
        if (isActive && boostText != null)
        {
            float scale = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
            boostText.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }

    public void Show()
    {
        isActive = true;

        if (boostText != null)
            boostText.gameObject.SetActive(true);
    }

    public void Hide()
    {
        isActive = false;
        if (boostText != null)
            boostText.gameObject.SetActive(false);
        if (boostFillImage != null)
            boostFillImage.fillAmount = 0f; // Remove fill-icon when finished
    }

    public void SetBoostProgress(float progress)
    {
        if (boostFillImage != null)
           boostFillImage.fillAmount = Mathf.Clamp01(progress);

        Debug.Log("FillAmount: " + boostFillImage.fillAmount);
    }
}