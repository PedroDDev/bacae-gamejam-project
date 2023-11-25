using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float velocidadeMovimento = 3f; // Velocidade de movimento vertical do vilão
    public float amplitudeMovimento = 3f; // Amplitude de movimento vertical do vilão
    public GameObject poderPrefab; // Prefab do poder que será lançado
    public Transform pontoLancamento; // Ponto de onde o poder será lançado
    public float velocidadePoder = 5f; // Velocidade do poder lançado

    private bool movimento = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Movimento vertical do vilão em um ciclo de cima para baixo
        float movimentoVertical = movimento ? 1 : -1;
        transform.Translate(Vector3.up * movimentoVertical * velocidadeMovimento * Time.deltaTime);

        // Inverte a direção quando atinge a amplitude máxima
        if (Mathf.Abs(transform.position.y) >= amplitudeMovimento)
        {
            movimento = !movimento;
        }

        // Lançamento de poder em intervalos
        if (Random.Range(0f, 1f) < 0.02f) // Ajuste a probabilidade conforme necessário
        {
            LancaPoder();
        }
    }

    void LancaPoder()
    {
        // Criação do poder
        GameObject poder = Instantiate(poderPrefab, pontoLancamento.position, Quaternion.identity);

        // Define a direção do poder em direção ao personagem
        Vector3 direcao = (GameObject.FindWithTag("Player").transform.position - pontoLancamento.position).normalized;

        // Aplica velocidade ao poder
        poder.GetComponent<Rigidbody2D>().velocity = direcao * velocidadePoder;
    }
}