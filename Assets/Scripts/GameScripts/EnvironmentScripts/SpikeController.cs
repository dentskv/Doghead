using System;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    private GameObject _playerGameObject;
    private PlayerHealth _playerHealth;
    private int _damage = 1;

    private void Start()
    {
        try
        {
            _playerGameObject = GameObject.FindGameObjectWithTag("Player");
            _playerHealth = _playerGameObject.GetComponent<PlayerHealth>();
        }
        catch (Exception)
        {
            Debug.Log("_playerGameObject isn't found or object was destroyed. SpikeController.cs");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerHealth.TakeDamage(_damage);
        }
    }
}
