using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightItemScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o poder atingiu o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScript>().ConsumirItem(); // Ajuste o valor de dano conforme necessário
            Destroy(gameObject); // Destroi o poder ao atingir o jogador
        }
    }
}
