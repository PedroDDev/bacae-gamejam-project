using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private string npcName;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float radius;
    private DialogueController _dc;

    private bool canInitDialogue = true;

    public bool canAsk;

    private bool onRadius;

    void Start()
    {
        _dc = FindObjectOfType<DialogueController>();
    }

    void FixedUpdate()
    {
        Interact();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onRadius && canInitDialogue)
        {
            _dc.Speech(npcName, canAsk);
            canInitDialogue = false;
        }
    }

    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if (hit != null)
        {
            onRadius = true;
        }
        else
        {
            onRadius = false;
            canInitDialogue = true;
        }
    }
}
