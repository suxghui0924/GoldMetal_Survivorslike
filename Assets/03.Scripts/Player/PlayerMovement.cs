using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2.5f;
    public Vector2 moveDir;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Animator anim;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + (moveDir.normalized * walkSpeed * Time.fixedDeltaTime));
    }

    private void LateUpdate()
    {
        anim.SetFloat("speed", moveDir.magnitude);
        if (moveDir.x < -0.1)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
    }
}
