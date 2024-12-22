using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Pool_B : MonoBehaviour
{
    public GameObject prefab;
    public int initialSize = 10; 

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        // 초기ㅅ생성 
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    // 오브젝트 가져옴 
    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // 새로 생성
            GameObject obj = Instantiate(prefab);
            return obj;
        }
    }

    // 오브젝트 반환
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
