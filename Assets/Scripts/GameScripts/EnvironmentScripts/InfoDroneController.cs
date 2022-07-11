using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class InfoDroneController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject infoTable;
    [SerializeField] private Animator infoDroneAnimator;
    [SerializeField] private String tableText;

    [Inject] private SoundPreset sound;
    
    private TextMeshProUGUI _textInfoTable;
    private Image _imageInfoTable;
    private Color _temp;

    private bool _isOpened;

    private void Start()
    {
        _imageInfoTable = infoTable.GetComponent<Image>();
        _textInfoTable = infoTable.GetComponentInChildren<TextMeshProUGUI>();
        _temp = new Color(255, 255, 255);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _textInfoTable.text = tableText;
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(sound.audioClips[3]);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            _temp.a = 1f;
            ChangeColorAlpha();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isOpened = true;
            infoDroneAnimator.SetBool("isOpened", _isOpened);
            _temp.a = 0f;
            ChangeColorAlpha();
        }
    }

    private void ChangeColorAlpha()
    {
        _imageInfoTable.color = _temp;
        _textInfoTable.color = _temp;
    }
}
