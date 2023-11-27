using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        AudioManager.instance.Play("Fight");
        jogador = GameObject.FindWithTag("Player").transform;

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
        float novaPosX = Mathf.Clamp(jogador.position.x + 5f, float.MinValue, float.MaxValue);
        float novaPosY = Mathf.Lerp(transform.position.y, jogador.position.y, Time.deltaTime * velocidadeMovimento);

        transform.position = new Vector3(novaPosX, novaPosY, transform.position.z);
    }

    void MovimentaVerticalComDelay()
    {
        float novaPosY = Mathf.Lerp(transform.position.y, jogador.position.y, Time.deltaTime * velocidadeMovimento);
        transform.position = new Vector3(transform.position.x, novaPosY, transform.position.z);
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
