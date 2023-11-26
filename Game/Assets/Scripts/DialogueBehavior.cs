using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public GameObject gameManager;

    public GameObject initialDialogue; 
    private DialogueObject dialogoAtual;

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager");
    }

    void Update()
    {
        // Exemplo: Verifica se o jogador pressionou uma tecla para escolher uma op��o de resposta
        //if (Input.GetKeyDown(KeyCode.Alpha1) && dialogoAtual.opcoesRespostaJogador.Length >= 1)
        //{
        //    EscolherResposta(0);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2) && dialogoAtual.opcoesRespostaJogador.Length >= 2)
        //{
        //    EscolherResposta(1);
        //}
        // Adicione mais condi��es conforme o n�mero de op��es desejado

        // Exemplo: Adicione aqui qualquer l�gica adicional do seu jogo
    }

    public void Speech(string actorName, bool canAsk)
    {
        dialogueObj.SetActive(true);
        actorNameText.text = actorName;
        _canAsk = canAsk;
        _player.canMove = false;

        sentences = new string[1] { baseTexts.ElementAt(baseTextIndex) };
        StartCoroutine(TypingSentence());
    }

    public void IniciarDialogo()
    {
        // Exemplo: Voc� pode chamar esta fun��o quando quiser iniciar um novo di�logo
        // e passar um novo objeto Dialogo para carregar
        DialogueObject novoDialogo = new DialogueObject(); // Substitua isso pelo objeto real do di�logo
        CarregarDialogo(novoDialogo);
    }

    void CarregarDialogo(DialogueObject dialogo)
    {
        dialogoAtual = dialogo;
        // Limpa o ponto de spawn antes de instanciar o novo di�logo
        LimparPontoSpawn();

        // Instancia o prefab do di�logo
        GameObject dialogoPrefab = Instantiate(prefabDialogo, pontoSpawnDialogo.position, Quaternion.identity);
        // Configura o di�logo no prefab
        //DialogoUI dialogoUI = dialogoPrefab.GetComponent<DialogoUI>();
        //if (dialogoUI != null)
        //{
        //    dialogoUI.ConfigurarDialogo(dialogo, this);
        //}
    }

    void LimparPontoSpawn()
    {
        // Limpa o ponto de spawn removendo qualquer objeto filho
        foreach (Transform child in pontoSpawnDialogo)
        {
            Destroy(child.gameObject);
        }
    }

    void EscolherResposta(int indiceResposta)
    {
        //// L�gica para escolher uma resposta e atualizar o status da narrativa
        //if (dialogoAtual.opcoesRespostaJogador.Length > indiceResposta)
        //{
        //    int scoreOpcao = indiceResposta + 1; // Pode ajustar conforme necess�rio
        //    //gameManager.AtualizarStatusNarrativa(scoreOpcao);
        //}

        // Exemplo: Ap�s escolher uma resposta, voc� pode carregar o pr�ximo di�logo
        // ou fazer qualquer outra coisa que fa�a sentido para o seu jogo
        IniciarDialogo();
    }
}
