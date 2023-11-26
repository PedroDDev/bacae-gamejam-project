using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float velocidadeMovimento = 3f; // Velocidade de movimento vertical do vilão
    public GameObject poderPrefab; // Prefab do poder que será lançado
    public Transform pontoLancamento; // Ponto de onde o poder será lançado
    public float velocidadePoder = 5f; // Velocidade do poder lançado
    public float intervaloLancamento = 5f;
    private Transform jogador;
    private float tempoDesdeUltimoLancamento;
    private float limiteInferior;
    private float limiteSuperior;
    private float BossHealth = 100.0f;

    void Start()
    {
        jogador = GameObject.FindWithTag("Player").transform;

        float alturaTela = Camera.main.orthographicSize;
        limiteInferior = -alturaTela;
        limiteSuperior = alturaTela;
    }

    // Update is called once per frame
    void Update()
    {
        MovimentaBoss();
        LancaPoderPeriodicamente();
    }

    void MovimentaBoss()
    {
        float novaPosY = Mathf.Lerp(transform.position.y, GetNovaPosicaoVertical(), Time.deltaTime * velocidadeMovimento);
        transform.position = new Vector3(transform.position.x, novaPosY, transform.position.z);
    }

    float GetNovaPosicaoVertical()
    {
        float novaPosicaoY = Mathf.Clamp(transform.position.y + Random.Range(-1f, 1f) * velocidadeMovimento, limiteInferior, limiteSuperior);
        return novaPosicaoY;
    }

    void LancaPoderPeriodicamente()
    {
        tempoDesdeUltimoLancamento += Time.deltaTime;
        Debug.Log(tempoDesdeUltimoLancamento);

        if (tempoDesdeUltimoLancamento >= intervaloLancamento)
        {
            LancaPoder();
            tempoDesdeUltimoLancamento = 0.0f;
        }
    }
    void LancaPoder()
    {
        // Criação do poder
        GameObject poder = Instantiate(poderPrefab, pontoLancamento.position, Quaternion.identity);

        // Define a direção do poder em direção ao jogador
        Vector3 direcao = (jogador.transform.position - pontoLancamento.position).normalized;
        //Vector3 direcao = pontoLancamento.position.normalized;
        direcao.y = 0;
        direcao.Normalize();

        // Aplica velocidade ao poder
        poder.GetComponent<Rigidbody2D>().velocity = direcao * velocidadePoder;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        RecebeDano(10.0f);
    }

    public void RecebeDano(float dano)
    {
       if(dano > BossHealth)
        {
            BossHealth = 0.0f;
            Destroy(gameObject);
        }
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
