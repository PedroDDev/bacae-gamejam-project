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
        AudioManager.instance.Play("SelectSound");
        SceneManager.LoadScene("LevelDesignScene");
    }

    public void ExitGame()
    {
        AudioManager.instance.Play("SelectSound");
        Application.Quit();
    }
}
