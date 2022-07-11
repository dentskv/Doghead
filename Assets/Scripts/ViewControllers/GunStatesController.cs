using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.UI;

public class GunStatesController : MonoBehaviour
{
    [SerializeField] private List<Button> equipButtons;
    [SerializeField] private GameObject cardPrefab;

    private Button buttonRef;
    
    private void Start()
    {
        //CreatingCards();
    }

    public void ChangeEquipment(int instanceID)
    {
        for (int i = 0; i < equipButtons.Count; i++)
        {
            if (equipButtons[i].GetInstanceID() != instanceID)
            {
                equipButtons[i].enabled = true;
            }
        }
    }

    private void CreatingCards()
    {
        for (int i = 0; i < 6; i++)
        {
            var card = Instantiate(cardPrefab);
            card.GetComponent<GunCardsController>().SetGunId(i);
            card.transform.SetParent(gameObject.transform);
            card.GetComponent<GunCardsController>().SetGunId(i);
            equipButtons.Add(GetComponentInChildren<Button>());
        }
    }
}
