using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerScript : MonoBehaviour
{
    public bool isAPlayer = false;

    public void InicializaTrajetoria(Vector3 trajetoria, float Velocidade, Rigidbody2D collider)
    {
        collider.velocity = trajetoria * Velocidade;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAPlayer)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("player acertado");
                collision.gameObject.GetComponent<PlayerScript>().RecebeDano(15.0f);
                Destroy(gameObject);
            }

        }
        else if (isAPlayer)
        {
            if (collision.gameObject.CompareTag("Boss"))
            {
                Debug.Log("boss acertado");
                collision.gameObject.GetComponent<BossScript>().RecebeDano(15.0f);
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }
}
