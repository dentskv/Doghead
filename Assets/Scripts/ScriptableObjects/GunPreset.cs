using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Gun", fileName = "Gun")]

    public class GunPreset : ScriptableObject
    {
        [SerializeField] public List<Levels> guns;
    }

    [System.Serializable]
    public class Levels
    {
        public Sprite gunSprite;
        public int gunLvl;
        public int buyPrice;
        public List<GunStats> gunStats;
    }

    [System.Serializable]
    public class GunStats
    {
        public float gunLevel;
        public float gunDamage;
        public float projectileSpeed;
        public float timeBetweenBullets;
        public int upgradePrice;
    }
}
