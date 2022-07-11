using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TestCardController : MonoBehaviour
{
    
    [SerializeField] private GameObject lockedCard;
    [SerializeField] private GameObject popUpMessage;
    [SerializeField] private TMP_Text priceToBuyText;
    [SerializeField] private TMP_Text priceToUpgradeText;
    [SerializeField] private TMP_Text gunNumber;
    [SerializeField] private Button equipButton;
    
    [Inject] private GunPreset gun;
    [Inject] private CoinsController coinsController;
    
    private int _gunID = 0;
    private int _gunLvl = 0;
    private bool _isUnlocked = false;
    private float _priceToBuy;
    private float _priceToUpgrade;
    private float _damage;

    public void SetGunID(int newID)
    {
        _gunID = newID;
    }
    
    private void Start()
    {
        priceToUpgradeText.text = gun.guns[_gunID].gunStats[_gunLvl].upgradePrice.ToString();
        gunNumber.text = "Gun " + gun.guns[_gunID].gunLvl;
        LoadLockStates();
        if(_isUnlocked)
        {
            lockedCard.SetActive(false);
        }
    }

    private void CreatingCards()
    {
        for (int i = 0; i < 6; i++)
        {
            
        }
    }
    
    private void LoadLockStates()
    {
        _isUnlocked = PlayerPrefs.GetInt(gunNumber.text) == 1 ? true : false;
    }

    private void UnlockCardClick()
    {
        if(coinsController.GetAmount >= _priceToBuy)
        {
            lockedCard.SetActive(false);
            _isUnlocked = true;
            PlayerPrefs.SetInt(gunNumber.text, 1);
            coinsController.SpendCoins(_priceToBuy);
        }
        else
        {
            popUpMessage.SetActive(true);
        }
    }
}
