using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Stars", fileName = "Stars")]
    
    public class StarPreset : ScriptableObject
    {
        [SerializeField] public Stars[] stars;
    }

    [System.Serializable]
    public class Stars
    {
        public int idLvl;
        public int starsAmount;
    }
}
