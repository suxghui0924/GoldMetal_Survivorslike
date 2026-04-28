using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] GameObject[] prefabZombies;

    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabZombies.Length];
        for(int i=0;i<pools.Length;i++)
        {
            pools[i] = new List<GameObject>();
        }

    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        if (!select) //.. data 잇으면 true 없으면 false
        {
            select = Instantiate(prefabZombies[index], transform);
            pools[index].Add(select);
        }
        return select;
    }
}
