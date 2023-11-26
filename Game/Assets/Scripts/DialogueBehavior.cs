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
        // Exemplo: Você pode chamar esta função quando quiser iniciar um novo diálogo
        // e passar um novo objeto Dialogo para carregar
        DialogueObject novoDialogo = new DialogueObject(); // Substitua isso pelo objeto real do diálogo
        CarregarDialogo(novoDialogo);
    }

    void CarregarDialogo(DialogueObject dialogo)
    {
        dialogoAtual = dialogo;
        // Limpa o ponto de spawn antes de instanciar o novo diálogo
        LimparPontoSpawn();

        // Instancia o prefab do diálogo
        GameObject dialogoPrefab = Instantiate(prefabDialogo, pontoSpawnDialogo.position, Quaternion.identity);
        // Configura o diálogo no prefab
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
        //// Lógica para escolher uma resposta e atualizar o status da narrativa
        //if (dialogoAtual.opcoesRespostaJogador.Length > indiceResposta)
        //{
        //    int scoreOpcao = indiceResposta + 1; // Pode ajustar conforme necessário
        //    //gameManager.AtualizarStatusNarrativa(scoreOpcao);
        //}

        // Exemplo: Após escolher uma resposta, você pode carregar o próximo diálogo
        // ou fazer qualquer outra coisa que faça sentido para o seu jogo
        IniciarDialogo();
    }
}
