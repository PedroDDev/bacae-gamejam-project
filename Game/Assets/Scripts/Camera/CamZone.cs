using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CamZone : MonoBehaviour
{
    #region Fields

    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;

    #endregion

    void Start()
    {
        virtualCamera.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            virtualCamera.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            virtualCamera.enabled = false;
        }
    }

    void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
