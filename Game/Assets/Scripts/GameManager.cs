using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Tipo de corrupcao do cenario
    public int corruption;
    public GameObject consumivelPrefab;
    public bool isABossBattle = false;

    void Start()
    {
        corruption = Random.Range(0, 2);
        InvokeRepeating("SpawnConsumivel", 2f, 5f);
    }

    void SpawnConsumivel()
    {
        if (!isABossBattle)
        {
            return;
        }

        // Instantiate(consumivelPrefab, new Vector3(Random.Range(-5f, 5f), 
        //     Random.Range(plataforma.position.y + 1f, plataforma.position.y + 4f), 0f), Quaternion.identity);
        
    }
}
