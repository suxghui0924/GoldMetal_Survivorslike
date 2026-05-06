using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Mono.Cecil;
using UnityEngine;

namespace _03.Scripts.Player
{
    public class Weapone : MonoBehaviour
    {
        public int id;
        public int prefabId;
        public float damage;
        public int count;
        public float speed;
        private PlayerMovement player;
        private float timer;

        private void Awake()
        {
            player = GetComponentInParent<PlayerMovement>();
        }

        private void Start()
        {
            Init();
        }

        void Update()
        { 
            switch (id)
            {
                case 0:
                    transform.Rotate(Vector3.back * speed * Time.deltaTime);
                    break;
                default:
                    timer += Time.deltaTime;
                    if (timer >= speed)
                    {
                        timer = 0f;
                        Fire();
                    } 
                    break;
            }
            if(Input.GetButtonDown("Jump"))
                LevelUp(20, 2);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void Fire()
        {
            if (!player.scanner.nearestTarget) return;
            Vector3 targetPos = player.scanner.nearestTarget.position;
            Vector3 dir = targetPos - transform.position;
            dir.Normalize();
            Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.GetComponent<Bullet>().Init(damage, count, dir);
        }

        public void LevelUp(float damage, int count)
        {
            this.damage = damage;
            this.count += count;
            
            if (id == 0)
                Batch();
        }

        public void Init()
        {
            switch (id)
            {
                case 0:
                    speed = 150;
                    Batch();
                    break;
                default:
                    speed = 0.3f;
                    break;
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        void Batch()
        {
            for (int index = 0; index < count; index++)
            {
                Transform bullet;
                if (index < transform.childCount)
                    bullet = transform.GetChild(index);
                else
                    bullet = GameManager.instance.pool.Get(prefabId).transform; bullet.parent = transform;
                bullet.localPosition = Vector3.zero;
                bullet.localRotation = Quaternion.identity;
                Vector3 rotVec = Vector3.forward * 360 * index / count;
                bullet.Rotate(rotVec);
                bullet.Translate(bullet.up * 1.5f, Space.World);
                bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);
            }
        }
    }
}