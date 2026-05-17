using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D _collider;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate waitForFixedUpdate;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        waitForFixedUpdate = new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        Vector2 dirVec = (target.position - rigid.position).normalized;
        Vector2 nextDir = dirVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextDir);
        rigid.linearVelocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive) return;
        spriter.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable()
    {
        target = GameManager.instance.plr.GetComponent<Rigidbody2D>();
        isLive = true;
        _collider.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead",false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];

        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && !other.CompareTag("Bullet")) return;
        health -= other.GetComponent<Bullet>().damage;
        StartCoroutine(Knockback());
        if (health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            _collider.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead",true);
        }
    }

    IEnumerator Knockback()
    {
        yield return waitForFixedUpdate;
        Vector3 playerPos = GameManager.instance.plr.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }
        
    private void Dead()
    {
        gameObject.SetActive(false);
    }
}