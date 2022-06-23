using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptTableObject
{
    [Serializable]
    public class Data
    {
        public float speed;
        public int points;
        public Sprite spritePoints;
        public int health;
        
    }
    [Serializable]
    public class Enemies
    {
        public Enemy enemy;
        public Data data;
    }
    [CreateAssetMenu(menuName = "Data/Enemy")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField]public List<Enemies> DataInspector;
        private Dictionary<Enemy, Data> data=new Dictionary<Enemy, Data>();
        public Dictionary<Enemy, Data> Data => data;

        private void OnEnable()
        {
            foreach (var item in DataInspector)
            {
                data.Add(item.enemy,item.data);
            }
        }
    }
    
}
