using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptTableObject
{
    public enum Enemy
    {
        BasicTank,
        FastTank,
        PowerTank,
        ArmorTank
    }
    [System.Serializable]
    public class EnemiesInLevel
    {
        public Enemy enemy;
        public int number;
    }
    [System.Serializable]
    public class Level
    {
        public GameObject map;
        public List<EnemiesInLevel> eneiesInspector;
        private List<Enemy> enemies;
        public List<Enemy> Enemies
        {
            get => enemies;
            set => enemies = value;
        }
    }
    [CreateAssetMenu(menuName = "Data/Level")]
    public class LevelData : ScriptableObject
    {
        public List<Level> levels;
        private void OnEnable()
        {
            foreach (var item in levels)
            {
                foreach (var enemies in item.eneiesInspector)
                {
                    for (int i = 0; i < enemies.number; i++)
                    {
                        item.Enemies.Add(enemies.enemy);
                    }
                }
            }
        }
    }
    

}
