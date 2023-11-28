using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public float velocidadeMovimento = 3f;
    public GameObject poderPrefab;
    public Transform pontoLancamento;
    public float velocidadePoder = 5f;
    public float intervaloLancamento = 2f;
    public float delayMovimentoVertical = 5f;
    private Transform jogador;
    private float tempoDesdeUltimoLancamento;
    private float limiteInferior;
    private float limiteSuperior;
    private float BossHealth = 100.0f;
    private Rigidbody2D rb;

    void Start()
    {
        AudioManager.instance.Play("Fight");
        jogador = GameObject.FindWithTag("Player").transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        float alturaTela = Camera.main.orthographicSize;
        limiteInferior = -alturaTela;
        limiteSuperior = alturaTela;
        InvokeRepeating("MovimentaVerticalComDelay", delayMovimentoVertical, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        MovimentaBoss();
        LancaPoderPeriodicamente();
    }

    void MovimentaBoss()
    {
        float delayPos = jogador.position.x + 4f;
        float novaPosX = Mathf.Clamp(delayPos, float.MinValue, float.MaxValue);
        delayPos = jogador.position.y;
        float novaPosY = Mathf.Lerp(transform.position.y, delayPos, Time.deltaTime * velocidadeMovimento);

        rb.MovePosition(new Vector2(novaPosX, novaPosY));
    }

    void MovimentaVerticalComDelay()
    {
        float delayPos = jogador.position.y;
        float novaPosY = Mathf.Lerp(transform.position.y, delayPos, Time.deltaTime * velocidadeMovimento);
        rb.MovePosition(new Vector2(transform.position.x, novaPosY));
    }

    void LancaPoderPeriodicamente()
    {
        tempoDesdeUltimoLancamento += Time.deltaTime;

        if (tempoDesdeUltimoLancamento >= intervaloLancamento)
        {
            LancaPoder();
            tempoDesdeUltimoLancamento = 0.0f;
        }
    }
    void LancaPoder()
    {
        AudioManager.instance.Play("EnemyShoot");
       
        GameObject poder = Instantiate(poderPrefab, pontoLancamento.position, Quaternion.identity);

        
        Vector3 direcao = (jogador.transform.position - pontoLancamento.position).normalized;
        
        direcao.y = 0;
        direcao.Normalize();

     
        poder.GetComponent<Rigidbody2D>().velocity = direcao * velocidadePoder;
    }

    public void RecebeDano(float dano)
    {
       if(dano > BossHealth)
        {
            BossHealth = 0.0f;
            Destroy(gameObject);
            SceneManager.LoadScene("EndGameScene");

        }

        BossHealth -= dano;
    }

    public void RecebeDanoEspecial(float dano)
    {
        if (dano > BossHealth)
        {
            BossHealth = 0.0f;
            Destroy(gameObject);
        }
    }
}
