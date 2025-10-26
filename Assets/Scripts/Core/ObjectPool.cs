using System.Collections.Generic;
using UnityEngine;

namespace ArcherOfGod.Core
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int initialSize = 10;

        private Queue<GameObject> pool = new Queue<GameObject>();

        private void Awake()
        {

            for (int i = 0; i < initialSize; i++)
            {
                CreatePooledObject();
            }
        }

        private GameObject CreatePooledObject()
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
            return obj;
        }

        public GameObject Get()
        {
            GameObject obj;

            if (pool.Count > 0)
            {
                obj = pool.Dequeue();
            }
            else
            {
                obj = CreatePooledObject();
            }

            obj.SetActive(true);
            return obj;
        }

        public void Return(GameObject obj)
        {
            if (obj == null) return;

            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}
