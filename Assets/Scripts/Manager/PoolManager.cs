using System;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class PoolObject:MonoBehaviour
    {
        public string poolName;
        public bool isPooled;
        
    }
    public class PoolManager : MonoBehaviour
    {
        public class Pool
        {
            private Stack<PoolObject> availblePoolObj = new Stack<PoolObject>();
            private bool fixedSize;
            private GameObject poolPrefabs;
            private int poolSize;
            private string poolName;

            public Pool(string name, GameObject prefab, int count, bool fixedSize)
            {
                this.poolName = name;
                this.poolPrefabs = prefab;
                this.fixedSize = fixedSize;
                this.poolSize = count;
                for (int i = 0; i < count; i++)
                {
                    AddObjectToPool(NewObjectInstance());
                }
            }

            private void AddObjectToPool(PoolObject poolObject)
            {
                poolObject.gameObject.SetActive(false);
                availblePoolObj.Push(poolObject);
                poolObject.isPooled = true;
            }

            private PoolObject NewObjectInstance()
            {
                GameObject obj = GameObject.Instantiate(poolPrefabs);
                PoolObject poolObject = obj.GetComponent<PoolObject>();
                if (poolObject == null)
                {
                    poolObject=obj.AddComponent<PoolObject>();
                }

                poolObject.poolName = this.poolName;
                return poolObject;
            }

            public PoolObject NextAvailableObject(Vector3 postion, Quaternion rotation)
            {
                PoolObject poolObject = null;
                if (availblePoolObj.Count > 0)
                {
                    poolObject = availblePoolObj.Pop();
                }
                else
                {
                    if (fixedSize==false)
                    {
                        poolSize++;
                        poolObject = NewObjectInstance();
                    }
                    else
                    {
                        Debug.Log(poolName+" khong the them pool");
                    }
                }

                GameObject obj = null;
                if (poolObject != null)
                {
                    poolObject.isPooled = false;
                    obj = poolObject.gameObject;
                    obj.SetActive(true);
                    obj.transform.position = postion;
                    obj.transform.rotation = rotation;
                }
                return poolObject;
            }
            public void ReturnObjToPool(PoolObject poolObject)
            {
                if (poolName.Equals(poolObject.name))
                {
                    if (poolObject.isPooled)
                    {
                        Debug.Log(poolObject.gameObject.name+"is already in pool");
                    }
                    else
                    {
                        AddObjectToPool(poolObject);
                    }
                }
                else
                {
                    Debug.Log(string.Format("thu lai {0} vs {1}",poolObject.name,poolName));
                }
            }
        
        }
        [Serializable]
        public class Info
        {
            public string name;
            public GameObject prefab;
            public int size;
            public bool fixedsize;
        }

        private static PoolManager instance;

        public static PoolManager Instance
        {
            get => instance == null ? new PoolManager() : instance;
        }
    
        private Dictionary<string, Pool> poolMap=new Dictionary<string, Pool>();

        public Dictionary<string, Pool> PoolMap
        {
            get => poolMap;
            set => poolMap = value;
        }

        public Info[] info;

        private PoolManager()
        {
            instance = this;
        }

        public void Init()
        {
            CheckDuplicatePoolName();
            CreatePools();
        }

        private void CheckDuplicatePoolName()
        {
            for (int i = 0; i < info.Length; i++)
            {
                string poolname = info[i].name;
                if (poolname.Length == 0)
                {
                    Debug.Log(string.Format("pool {0} cha co ten kia",i));
                }
                else
                {
                    for (int j = i+1; j < info.Length; j++)
                    {
                        if (poolname.Equals(info[j].name))
                        {
                            Debug.Log(string.Format("trung ten giua {0} va {1}",i,j));
                        }
                    }
                }
            }
        }

        private void CreatePools()
        {
            foreach (var item in info)
            {
                Pool pool = new Pool(item.name, item.prefab, item.size, item.fixedsize);
                poolMap[item.name] = pool;
            }
        }

        public PoolObject GetObjFromPool(string name, Vector3 position, Quaternion quaternion)
        {
            PoolObject poolObject = null;
            if (poolMap.ContainsKey(name))
            {
                Pool pool = poolMap[name];
                poolObject = pool.NextAvailableObject(position, quaternion);
                if (pool == null)
                {
                    Debug.Log("Null");
                }
            }
            else
            {
                Debug.Log("poopmap khong co key "+name);
            }

            return poolObject;
        }

        public void ReturnObjToPool(PoolObject poolObject)
        {
            if (poolObject == null)
            {
                Debug.Log("error");
            }
            else
            {
                if (poolMap.ContainsKey(poolObject.poolName))
                {
                    Pool pool = poolMap[poolObject.poolName];
                    pool.ReturnObjToPool(poolObject);
                }
                else
                {
                    Debug.LogWarning("No pool available with name:"+poolObject.poolName);
                }
            }
        }
    }
}