using System.Collections;
using UnityEngine;
using TMPro;
using System;
using ScriptableObjects;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private RectTransform achievementPanel;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int amountOfEnemies;

    [Inject] private SoundPreset sound;
    [Inject] private StarPreset starPreset;
    
    public event Action<string> enemiesAreDead;
    
    private TextMeshProUGUI _achievementText;
    private GameObject[] _enemies;
    private float _panelUpDistance;
    private float _panelDownDistance;
    private int _increasingNumber;
    
    private void Start()
    {
        _achievementText = achievementPanel.GetComponentInChildren<TextMeshProUGUI>();
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesAreDead += CallAchievement;
        amountOfEnemies = _enemies.Length + _increasingNumber;
        _panelDownDistance = achievementPanel.transform.localPosition.y;
        _panelUpDistance = _panelDownDistance + 120;
        var tempPosition = achievementPanel.transform.localPosition;
        tempPosition.y += 120;
        achievementPanel.transform.localPosition = tempPosition;
        
        Debug.Log("enemies" + amountOfEnemies);
    }

    public void IncreaseAmount(int numberOfEnemies)
    {
        _increasingNumber = numberOfEnemies;
    }

    public IEnumerator MoveAchievement(string textAchievement)
    {
        _achievementText.SetText(textAchievement);
        while (achievementPanel.transform.localPosition.y >= _panelDownDistance)
        {
            achievementPanel.transform.localPosition = new Vector2(achievementPanel.transform.localPosition.x, achievementPanel.transform.localPosition.y - 105f * Time.deltaTime);
            yield return null;
        }
        audioSource.PlayOneShot(sound.audioClips[10]);
        yield return new WaitForSeconds(1f);
        while (achievementPanel.transform.localPosition.y <= _panelUpDistance)
        {
            achievementPanel.transform.localPosition = new Vector2(achievementPanel.transform.localPosition.x, achievementPanel.transform.localPosition.y + 105f * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        enemiesAreDead -= CallAchievement;
    }

    private void CallAchievement(string text)
    {
        StartCoroutine(MoveAchievement(text));
    }
    
    public void CountEnemies()
    {
        amountOfEnemies--;
        if (amountOfEnemies == 0)
        {
            enemiesAreDead?.Invoke("Kill all enemies");
            starPreset.stars[SceneManager.GetActiveScene().buildIndex - 1].starsAmount++;
        }
    }
}

