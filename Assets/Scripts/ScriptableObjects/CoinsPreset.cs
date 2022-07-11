using Controllers;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Coin", fileName = "Coin")]

    public class CoinsPreset : ScriptableObject
    {
        [SerializeField] private CoinsController.CoinsData coinsData;

        public CoinsController.CoinsData CoinsData => coinsData;
    }

}

