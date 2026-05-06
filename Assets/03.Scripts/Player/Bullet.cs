using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;
        if (per > -1)
            rb.linearVelocity = dir * 15f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null && !other.CompareTag("Enemy") || per == -1) return;
        per--;
        if (per == -1)
        {
            rb.linearVelocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
