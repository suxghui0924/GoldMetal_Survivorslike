using System;
using UnityEngine;

namespace _03.Scripts._Core
{
    public class Follow : MonoBehaviour
    {
        RectTransform myRectTransform;
        private Vector3 moveDir;

        private void Awake()
        {
            myRectTransform = GetComponent<RectTransform>();
        }

        private void FixedUpdate()
        {
            moveDir = Camera.main.WorldToScreenPoint(GameManager.instance.plr.transform.position);
            myRectTransform.position = new Vector3(moveDir.x, moveDir.y - 100, myRectTransform.position.z);
        }
    }
}