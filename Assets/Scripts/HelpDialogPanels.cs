using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpDialogPanels : MonoBehaviour
{
    [Header("Navigation Buttons")]
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    [Header("Panels (each is a prebuilt GameObject to show/hide)")]
    [SerializeField] private List<GameObject> pages = new List<GameObject>();

    [Header("State")]
    [SerializeField] private int startPageIndex = 0;

    public int CurrentPageIndex => currentPageIndex;

    private int currentPageIndex = 0;

    private void Awake()
    {
        if (leftButton == null) { Debug.LogError("HelpDialogPanels: Left button is missing."); enabled = false; return; }
        if (rightButton == null) { Debug.LogError("HelpDialogPanels: Right button is missing."); enabled = false; return; }

        leftButton.onClick.AddListener(GoLeft);
        rightButton.onClick.AddListener(GoRight);
    }

    private void Start()
    {
        if (pages == null || pages.Count == 0)
        {
            Debug.LogError("HelpDialogPanels: Pages list is empty.");
            UpdateButtons();
            return;
        }

        currentPageIndex = Mathf.Clamp(startPageIndex, 0, pages.Count - 1);
        ShowPage(currentPageIndex);
    }

    public void GoLeft()
    {
        GoToPage(currentPageIndex - 1);
    }

    public void GoRight()
    {
        GoToPage(currentPageIndex + 1);
    }

    public void GoToPage(int index)
    {
        if (pages == null || pages.Count == 0) return;

        index = Mathf.Clamp(index, 0, pages.Count - 1);
        if (index == currentPageIndex) return;

        currentPageIndex = index;
        ShowPage(currentPageIndex);
    }

    private void ShowPage(int index)
    {
        // Show only the selected page, hide all others.
        for (int i = 0; i < pages.Count; i++)
        {
            GameObject page = pages[i];
            if (page != null)
                page.SetActive(i == index); // SetActive enables/disables a GameObject. [web:46]
        }

        UpdateButtons();
    }

    private void UpdateButtons()
    {
        bool hasPages = pages != null && pages.Count > 0;

        if (!hasPages)
        {
            if (leftButton != null) leftButton.gameObject.SetActive(false);
            if (rightButton != null) rightButton.gameObject.SetActive(false);
            return;
        }

        bool isFirst = currentPageIndex <= 0;
        bool isLast = currentPageIndex >= pages.Count - 1;

        leftButton.gameObject.SetActive(!isFirst);
        rightButton.gameObject.SetActive(!isLast);
    }
}
