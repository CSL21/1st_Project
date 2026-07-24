using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    [SerializeField] List<GameObject> objList = new List<GameObject>();

    Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();

    int poolSize;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        poolSize = 5;

        foreach (GameObject obj in objList)
        {
            pools[obj.name] = new Queue<GameObject>();

            GameObject parentPool = new GameObject($"{obj.name}_Pool");
            parentPool.transform.SetParent(this.transform);

            for (int i = 0; i < poolSize; i++)
            {
                GameObject go = Instantiate(obj); //

                go.name = obj.name;

                go.transform.SetParent(parentPool.transform);
                go.transform.rotation = obj.transform.rotation; //
                go.SetActive(false);
                pools[obj.name].Enqueue(go);
            }
        }
    }

    public GameObject GetObject(string name)
    {
        if (!pools.ContainsKey(name))
        {
            return null;
        }

        if (pools[name].Count > 0)
        {
            GameObject go = pools[name].Dequeue();
            go.SetActive(true);
            return go;
        }
        else
        {
            GameObject go = Instantiate(objList.Find(obj => obj.name == name));
            go.transform.rotation = objList.Find(obj => obj.name == name).transform.rotation; //
            return go;
        }
    }

    public void ReturnObject(string name, GameObject go)
    {
        if (!pools.ContainsKey(name))
        {
            Destroy(go);
            return;
        }
        go.SetActive(false);
        pools[name].Enqueue(go);
    }

}


// 1. 싱글톤
// 2. 미리 생성
// 3. 필요할 때 가져다 쓰기
// 4. 사용 후 반납
// 