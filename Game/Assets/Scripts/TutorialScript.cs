using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LevelDesignScene");
    }
}
