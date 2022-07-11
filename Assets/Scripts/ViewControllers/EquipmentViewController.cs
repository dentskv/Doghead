using UnityEngine;
using Zenject;
using ScriptableObjects;

namespace ViewControllers
{
    public class EquipmentViewController : MonoBehaviour
    {
        [SerializeField] private Sprite[] damagePoints;
        [SerializeField] private Sprite[] ratePoints;
        [SerializeField] private Sprite yellowPoint;
        [SerializeField] private Sprite whitePoint;

        [Inject] private GunPreset gunPreset;

    }
}