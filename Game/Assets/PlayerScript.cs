using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public Slider healthBar;
    public float vidaMaxima = 100.0f;
    private float vidaAtual;
    public Transform pontoLancamento;

    public float velocidadePoder = 5f;

    public int poderEspecialItensConsumidos = 0;
    public GameObject poderPrefab;
    private GameObject boss;

    void Start()
    {
        boss = GameObject.FindWithTag("Boss");
        vidaAtual = vidaMaxima;
        healthBar.maxValue = vidaMaxima;
        healthBar.value = vidaAtual;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LancaPoder();
        }

        if (Input.GetKeyDown(KeyCode.J) && poderEspecialItensConsumidos >= 5)
        {
            AtivaPoderEspecial();
        }
    }

    void LancaPoder()
    {
        // Criação do poder
        GameObject poder = Instantiate(poderPrefab, pontoLancamento.position, Quaternion.identity);

        // Define a direção do poder em direção ao jogador
        Vector3 direcao = (boss.transform.position - pontoLancamento.position).normalized;
        //Vector3 direcao = pontoLancamento.position.normalized;
        direcao.y = 0;
        direcao.Normalize();

        // Aplica velocidade ao poder
        poder.GetComponent<Rigidbody2D>().velocity = direcao * velocidadePoder;
    }

    void AtivaPoderEspecial()
    {
        if (boss != null || poderEspecialItensConsumidos < 5)
        {
            boss.GetComponent<BossScript>().RecebeDanoEspecial(50.0f);
        }

        poderEspecialItensConsumidos = 0; // Reinicia o contador
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Consumivel"))
        {
            // Consome item ao colidir
            ConsumirItem();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("PoderBoss"))
        {
            // Perde vida ao colidir com o poder do boss
            RecebeDano(5);
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public void ConsumirItem()
    {
        vidaAtual += 10; // Exemplo: recupera 10 de vida ao consumir um item
        vidaAtual = Mathf.Min(vidaAtual, vidaMaxima); // Garante que a vida não ultrapasse o máximo
        AtualizaHealthBar();
        poderEspecialItensConsumidos++;

        if (poderEspecialItensConsumidos >= 5)
        {
            // Ative algum indicador visual de que o jogador pode usar o poder especial
        }
    }

    public void RecebeDano(float dano)
    {
        vidaAtual -= dano;
        vidaAtual = Mathf.Max(0, vidaAtual); // Garante que a vida não seja negativa
        AtualizaHealthBar();

        if (vidaAtual <= 0)
        {
            // Implemente lógica de morte do jogador
            // Exemplo: reiniciar o jogo
            Debug.Log("Jogador morreu!");
        }
    }

    void AtualizaHealthBar()
    {
        healthBar.value = vidaAtual;
    }
}
