using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    [SerializeField] private Text messageText;

    public GameObject exitButton;

    private readonly string[] sentences = new string[5]
    {
        "Às vezes, nós precisamos entender nossos monstros antes de poder enfrentá-los...",
        "E às vezes, a comunicação é a única saída, para todos nós...",
        "Você conseguiu derrotar o monstro...",
        "Mas e os seus monstros? ...",
        "Obrigado por jogar..."
    };
    private int index = -1;
    public float typingSpeed = 0.2f;

    public float timeToChangeSentence = 8;
    private float currentTimeToChangeSentence = 0;

    // Start is called before the first frame update
    void Start()
    {
        exitButton.SetActive(false);
        currentTimeToChangeSentence = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeToChangeSentence += Time.deltaTime;

        if (currentTimeToChangeSentence >= timeToChangeSentence)
        {
            currentTimeToChangeSentence = 0;
            ChangeSentence();
        }
    }

    void ChangeSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            messageText.text = string.Empty;
            StartCoroutine(TypingSentence());
        }
        else
        {
            exitButton.SetActive(true);
        }
    }

    IEnumerator TypingSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
