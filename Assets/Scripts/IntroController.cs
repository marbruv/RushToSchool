using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public GameObject[] panels;        // Panels in order
    public float[] panelDurations;     // How long each panel stays before switching automatically

    private int index = 0;
    private float timer = 0f;

    void Start()
    {
        // Make sure only the first panel is visible
        for (int i = 0; i < panels.Length; i++)
            panels[i].SetActive(i == 0);

        timer = panelDurations[0];
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // If time runs out switch to next panel
        if (timer <= 0f)
        {
            Next();
        }
    }

    public void Next()
    {
        index++;

        if (index >= panels.Length)
        {
            SceneManager.LoadScene("MainMenu");
            return;
        }

        panels[index - 1].SetActive(false);
        panels[index].SetActive(true);
        timer = panelDurations[index];
    }
}