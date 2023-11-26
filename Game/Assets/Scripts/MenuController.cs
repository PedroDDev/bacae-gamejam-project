using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManager; 

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("Echoes");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayGame()
    {
        
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
