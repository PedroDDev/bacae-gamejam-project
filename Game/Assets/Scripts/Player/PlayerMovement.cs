using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _coll;
    private SpriteRenderer _spr;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<BoxCollider2D>();

        _spr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(dirX * moveSpeed, _rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded()) _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);

        if (dirX > 0) _spr.flipX = false;
        else if (dirX < 0) _spr.flipX = true;
    }

    /// <summary>
    /// Verifica se o personagem está no chão
    /// </summary>
    /// <returns></returns>
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(_coll.bounds.center, _coll.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }
}
