using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.plr.transform.position;
        Vector3 myPos = this.transform.position;

        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        float dirX = (GameManager.instance.plr.moveDir.x < 0) ? -1 : 1;
        float dirY = (GameManager.instance.plr.moveDir.y < 0) ? -1 : 1;

        switch(transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    transform.Translate(GameManager.instance.plr.moveDir * 20 + new Vector2(UnityEngine.Random.Range(-3f,3f), UnityEngine.Random.Range(-3f,3f)));
                }
                break;
        }
    }
}
