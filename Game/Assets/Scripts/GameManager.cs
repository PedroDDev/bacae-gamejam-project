using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int corruption;

    void Start()
    {
        corruption = Random.Range(0, 2);
    }
}
