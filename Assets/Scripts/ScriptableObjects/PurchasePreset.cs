using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Purchases", fileName = "Purchases")]
    
    public class PurchasePreset : ScriptableObject
    {
        [SerializeField] public List<Products> purchases;
    }

    [System.Serializable]
    public class Products
    {
        public string purchaseName;
        public float purchasePrice;
        public float purchaseCoinsContent;
        public string purchaseDescription;
    }
}
