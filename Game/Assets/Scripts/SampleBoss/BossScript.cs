using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float velocidadeMovimento = 3f; // Velocidade de movimento vertical do vil�o
    public float amplitudeMovimento = 3f; // Amplitude de movimento vertical do vil�o
    public GameObject poderPrefab; // Prefab do poder que ser� lan�ado
    public Transform pontoLancamento; // Ponto de onde o poder ser� lan�ado
    public float velocidadePoder = 5f; // Velocidade do poder lan�ado

    private bool movimento = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Movimento vertical do vil�o em um ciclo de cima para baixo
        float movimentoVertical = movimento ? 1 : -1;
        transform.Translate(Vector3.up * movimentoVertical * velocidadeMovimento * Time.deltaTime);

        // Inverte a dire��o quando atinge a amplitude m�xima
        if (Mathf.Abs(transform.position.y) >= amplitudeMovimento)
        {
            movimento = !movimento;
        }

        // Lan�amento de poder em intervalos
        if (Random.Range(0f, 1f) < 0.02f) // Ajuste a probabilidade conforme necess�rio
        {
            LancaPoder();
        }
    }

    void LancaPoder()
    {
        // Cria��o do poder
        GameObject poder = Instantiate(poderPrefab, pontoLancamento.position, Quaternion.identity);

        // Define a dire��o do poder em dire��o ao personagem
        Vector3 direcao = (GameObject.FindWithTag("Player").transform.position - pontoLancamento.position).normalized;

        // Aplica velocidade ao poder
        poder.GetComponent<Rigidbody2D>().velocity = direcao * velocidadePoder;
    }
}