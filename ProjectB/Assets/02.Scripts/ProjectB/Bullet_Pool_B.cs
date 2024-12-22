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
        // �ʱ⤵���� 
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    // ������Ʈ ������ 
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
            // ���� ����
            GameObject obj = Instantiate(prefab);
            return obj;
        }
    }

    // ������Ʈ ��ȯ
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
