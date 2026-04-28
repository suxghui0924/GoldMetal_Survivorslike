using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData; // 인스펙터에서 설정할 소환 데이터 배열

    int level;
    float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        // 10초마다 레벨업 (0레벨부터 시작)
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);

        timer += Time.deltaTime;

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        // 풀 매니저에서 에너미 소환 (기본 에너미 프리팹 인덱스 0 가정)
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length -1)].position;

        // 소환된 에너미 초기화
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

// 직렬화를 통해 인스펙터에서 보이게 설정
[System.Serializable]
public class SpawnData
{
    public int spriteType;   // 애니메이터 인덱스
    public float spawnTime;  // 소환 간격
    public int health;       // 체력
    public float speed;      // 속도
}