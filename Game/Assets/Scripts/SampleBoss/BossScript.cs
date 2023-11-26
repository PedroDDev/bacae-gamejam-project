using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float velocidadeMovimento = 3f; // Velocidade de movimento vertical do vil�o
    public GameObject poderPrefab; // Prefab do poder que ser� lan�ado
    public Transform pontoLancamento; // Ponto de onde o poder ser� lan�ado
    public float velocidadePoder = 5f; // Velocidade do poder lan�ado
    public float intervaloLancamento = 5f;
    private Transform jogador;
    private float tempoDesdeUltimoLancamento;

    private float BossHealth = 100.0f;

    void Start()
    {
        jogador = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        MovimentaBoss();
        LancaPoderPeriodicamente();
    }

    void MovimentaBoss()
    {
        // Movimento vertical alinhado � posi��o do jogador com um delay
        float delay = 5.5f;
        float novaPosY = Mathf.Lerp(transform.position.y, jogador.position.y, Time.deltaTime * velocidadeMovimento);
        transform.position = new Vector3(transform.position.x, novaPosY, transform.position.z);
        transform.Translate(Vector3.up * velocidadeMovimento * Time.deltaTime);

        // Atualiza a posi��o para a posi��o do jogador com delay
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, jogador.position.y, Time.deltaTime * delay), transform.position.z);
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
        // Cria��o do poder
        GameObject poder = Instantiate(poderPrefab, pontoLancamento.position, Quaternion.identity);

        // Define a dire��o do poder em dire��o ao jogador
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
