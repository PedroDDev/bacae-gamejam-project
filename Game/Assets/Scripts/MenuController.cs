using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("Echoes");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("LevelDesignScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
