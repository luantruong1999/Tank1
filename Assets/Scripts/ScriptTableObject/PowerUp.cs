using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptTableObject
{
    public enum Power
    {
        Grenade,
        Helmet,
        Shovel,
        Star,
        Tank,
        Timer
    }
    [Serializable]
    public class SpritePower
    {
        public Power power;
        public Sprite sprite;
        
    }
    [CreateAssetMenu(menuName = "Data/Power")]
    public class PowerUp : ScriptableObject
    {
        public List<SpritePower> powers;
    }
}
