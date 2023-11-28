using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameScript : MonoBehaviour
{
    public GameObject restartPanel;

    // Start is called before the first frame update
    void Start()
    {
        restartPanel.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("BossBattle");
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
