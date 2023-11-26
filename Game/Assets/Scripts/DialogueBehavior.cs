using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DialogueBehavior : MonoBehaviour
{
    public GameObject [] prefabDialogo;
    [SerializeField] private GameObject dialogueObj;
    [SerializeField] private GameObject dialogueQuestionsObj;
    [SerializeField] private Text messageText;
    [SerializeField] private Text actorNameText;

    [SerializeField] private Text firstQuestion;
    [SerializeField] private Text secondQuestion;
    [SerializeField] private Text thirdQuestion;
    private GameObject gameManager;
    private GameObject npcToDestroy;

    private PlayerMovement _player;

    private string[] sentences;
    private int index;

    private int baseTextIndex = 0;
    private int questionIndex = 0;
    public float typingSpeed;

    private bool _canAsk;

    private DialogueObject dialogoAtual;

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager");
        _player = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        // Exemplo: Verifica se o jogador pressionou uma tecla para escolher uma opção de resposta
        //if (Input.GetKeyDown(KeyCode.Alpha1) && dialogoAtual.opcoesRespostaJogador.Length >= 1)
        //{
        //    EscolherResposta(0);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2) && dialogoAtual.opcoesRespostaJogador.Length >= 2)
        //{
        //    EscolherResposta(1);
        //}
        // Adicione mais condições conforme o número de opções desejado

        // Exemplo: Adicione aqui qualquer lógica adicional do seu jogo
    }

    public void Speech(string actorName, bool canAsk, GameObject currentNPC)
    {
        Debug.Log("chamado");
        npcToDestroy = currentNPC;
        dialogueObj.SetActive(true);
        actorNameText.text = actorName;
        _canAsk = canAsk;
        _player.canMove = false;

        sentences = new string[1] { prefabDialogo.ElementAt(baseTextIndex).GetComponent<DialogueObject>().falaNPCPrimaria };
        StartCoroutine(TypingSentence());
    }

    public void ReturnQuestions()
    {
        dialogueQuestionsObj.SetActive(true);

        firstQuestion.text = prefabDialogo.ElementAt(baseTextIndex).GetComponent<DialogueObject>().opcoesPerguntaJogador[0];
        secondQuestion.text = prefabDialogo.ElementAt(baseTextIndex).GetComponent<DialogueObject>().opcoesPerguntaJogador[1];
        thirdQuestion.text = prefabDialogo.ElementAt(baseTextIndex).GetComponent<DialogueObject>().opcoesPerguntaJogador[2];

    }


    public void ReturnResponse(int buttonId)
    {
        dialogueQuestionsObj.SetActive(false);
        _canAsk = false;

        messageText.text = prefabDialogo.ElementAt(baseTextIndex).GetComponent<DialogueObject>().respostaNPC[buttonId];
        baseTextIndex++;
    }

    public void NextDialogue()
    {
        if (_canAsk) ReturnQuestions();
        else
        {
            if (messageText.text == sentences[index])
            {
                if (index < sentences.Length - 1)
                {
                    index++;
                    messageText.text = string.Empty;
                    StartCoroutine(TypingSentence());
                }
                else
                {
                    index = 0;
                    messageText.text = string.Empty;
                    dialogueObj.SetActive(false);
                    _player.canMove = true;
                    Destroy(npcToDestroy);
                }
            }
            else
            {
                index = 0;
                messageText.text = string.Empty;
                dialogueObj.SetActive(false);
                _player.canMove = true;
                Destroy(npcToDestroy);
            }
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
    
}
