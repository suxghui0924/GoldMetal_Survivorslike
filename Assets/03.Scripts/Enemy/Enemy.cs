using UnityEngine;

public class Enemy : MonoBehaviour
{
    float speed = 1.5f;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!isLive)
            return;
        Vector2 dirVec = (target.position - rigid.position).normalized;
        Vector2 nextDir = dirVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextDir);
        rigid.linearVelocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }
}
