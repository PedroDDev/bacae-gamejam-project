using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum MovementState { IDLE, WALK, JUMP }

    private Rigidbody2D _rb;
    private BoxCollider2D _coll;
    private SpriteRenderer _spr;
    private Animator _anim;

    private float dirX = 0f;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();

        _spr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(dirX * moveSpeed, _rb.velocity.y);
        if (Input.GetButtonDown("Jump") && IsGrounded()) _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);

        UpdateAnimationStateMachine();
    }

    /// <summary>
    /// Verifica se o personagem está no chão
    /// </summary>
    /// <returns></returns>
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(_coll.bounds.center, _coll.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }

    /// <summary>
    /// Cria máquina de estados para gerenciamento das animações de movimentacao do personagem
    /// </summary>
    private void UpdateAnimationStateMachine()
    {
        MovementState currentState = MovementState.IDLE;
        
        //Verifica se o personagem está se movendo
        if (dirX != 0)
        {
            if (dirX > 0) _spr.flipX = false;
            else if (dirX < 0) _spr.flipX = true;

            currentState = MovementState.WALK;
        }
        else
        {
            currentState = MovementState.IDLE;
        }

        //Verifica se o personagem está no ar
        if(!IsGrounded()) currentState = MovementState.JUMP;

        _anim.SetInteger("state", (int)currentState);
    }
}
