using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerScript : MonoBehaviour
{
    public bool isAPlayer = false;

    public void Start()
    {
       
    }
    public void InicializaTrajetoria(Vector3 trajetoria, float Velocidade, Rigidbody2D collider)
    {
        collider.velocity = trajetoria * Velocidade;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAPlayer)
        {
            return;
        }
        // Verifica se o poder atingiu o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player acertado");
            collision.gameObject.GetComponent<PlayerScript>().RecebeDano(20.0f); // Ajuste o valor de dano conforme necessário
            Destroy(gameObject); // Destroi o poder ao atingir o jogador
        }
    }
}
