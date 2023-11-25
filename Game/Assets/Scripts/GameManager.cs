using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Tipo de corrupcao do cenario
    public int corruption;

    void Start()
    {
        corruption = Random.Range(0, 2);
    }
}
