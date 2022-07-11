using UnityEngine;
using ScriptableObjects;
using Zenject;
using TMPro;
using UnityEngine.UI;

namespace Controllers
{
    public class GunCardsController : MonoBehaviour
    {
        [SerializeField] private GameObject lockedGunCard;
        [SerializeField] private GameObject popUpMessage;
        [SerializeField] private TMP_Text priceToBuyText;
        [SerializeField] private TMP_Text priceToUpgradeText;
        [SerializeField] private TMP_Text gunNumber;
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button unlockButton;
        [SerializeField] private Button equipButton;
        [SerializeField] private Image unlockGunImage;
        [SerializeField] private Image lockGunImage;
        [SerializeField] private int _gunID;
        
        [Inject] private CoinsController coinsController;
        [Inject] private GunPreset gun;

        private GunCardsViewController _gunCardsViewController;
        private GunStatesController _gunStatesController;
        private TMP_Text _equipButtonText;
        private float _reloadingTime;
        private float _bulletSpeed;
        private float _damage;
        private bool _isUnlocked;
        private bool _isChosen;
        private int _priceToUpgrade;
        private int _priceToBuy;
        private int _gunLevel;

        public void SetGunId(int id)
        {
            _gunID = id;
        }

        private void Start()
        {
            if (PlayerPrefs.GetInt("gunID") == _gunID)
            {
                equipButton.enabled = false;
                _isChosen = true;
            }
            _equipButtonText = equipButton.GetComponentInChildren<TMP_Text>();
            _gunStatesController = GetComponentInParent<GunStatesController>();
            _gunCardsViewController = GetComponent<GunCardsViewController>();
            _gunLevel = PlayerPrefs.GetInt(_gunID + "GunLevel");
            LoadStates();
            
            if (_isUnlocked)
            {
                lockedGunCard.SetActive(false);
            }
            
            SetCardStats();
            UpdateInfo();
            
            if (_isChosen)
            {
                SetGameStats();
            }
            
            AddButtonListeners();
        }

        private void AddButtonListeners()
        {
            upgradeButton.onClick.AddListener(UpgradeGunClick);
            unlockButton.onClick.AddListener(UnlockCardClick);
            equipButton.onClick.AddListener(EquipButtonClick);
        }
        
        private void Update()
        {
            if (_isChosen)
            {
                _equipButtonText.text = "Equipped";
                PlayerPrefs.SetInt("gunID", _gunID);
            }
            else
            {
                _equipButtonText.text = "Equip";
                equipButton.enabled = true;
            }
            if (equipButton.enabled)
            {
                _isChosen = false;
            }
        }

        private void UnlockCardClick()
        {
            if (coinsController.GetAmount >= _priceToBuy)
            {
                lockedGunCard.SetActive(false);
                _isUnlocked = true;
                PlayerPrefs.SetInt(gunNumber.text, 1);
                coinsController.SpendCoins(_priceToBuy);
            }
            else
            {
                popUpMessage.SetActive(true);
            }
        }
        
        private void UpgradeGunClick()
        {
            if (coinsController.GetAmount >= _priceToUpgrade)
            {
                if (_gunLevel < 3)
                {
                    _gunLevel++;
                    PlayerPrefs.SetInt(_gunID + "GunLevel", _gunLevel);
                    coinsController.SpendCoins(_priceToUpgrade);
                    _gunCardsViewController.ChangeDamagePoints((int) _damage);
                    _gunCardsViewController.ChangeRatePoints((int) _bulletSpeed);
                    SetCardStats();
                    UpdateInfo();
                }

                if (_isChosen)
                {
                    SetCardStats();
                    SetGameStats();
                }
            }
            else
            {
                popUpMessage.SetActive(true);
            }
        }

        private void EquipButtonClick()
        {
            _gunStatesController.ChangeEquipment(equipButton.GetInstanceID());
            SetGameStats();
            PlayerPrefs.SetInt("EquippedGun", equipButton.GetInstanceID());
            equipButton.enabled = false;
            _isChosen = true;
        }
        
        private void LoadStates()
        {
            _isUnlocked = PlayerPrefs.GetInt(gunNumber.text) == 1 ? true : false;
        }
        
        private void UpdateInfo()
        {
            _priceToUpgrade = gun.guns[_gunID].gunStats[_gunLevel].upgradePrice;
            _priceToBuy = gun.guns[_gunID].buyPrice;
            priceToBuyText.text = _priceToBuy.ToString();
            priceToUpgradeText.text = _priceToUpgrade.ToString();
            _gunCardsViewController.ChangeDamagePoints((int) _damage);
            _gunCardsViewController.ChangeRatePoints((int) _bulletSpeed);
            lockGunImage.sprite = gun.guns[_gunID].gunSprite;
            unlockGunImage.sprite = lockGunImage.sprite;
        } 
        
        private void SetCardStats()
        {
            _damage = gun.guns[_gunID].gunStats[_gunLevel].gunDamage;
            _bulletSpeed = gun.guns[_gunID].gunStats[_gunLevel].timeBetweenBullets;
            _reloadingTime = _bulletSpeed;
        }

        private void SetGameStats()
        {
            PlayerPrefs.SetFloat("damage", _damage);
            PlayerPrefs.SetFloat("reload", _reloadingTime);
            PlayerPrefs.SetFloat("bulletSpeed", _bulletSpeed);
        }

    }
}
