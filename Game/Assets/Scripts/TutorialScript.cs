using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    public void StartGame()
    {
        AudioManager.instance.Play("SelectSound");
        SceneManager.LoadScene("LevelDesignScene");
    }
}
