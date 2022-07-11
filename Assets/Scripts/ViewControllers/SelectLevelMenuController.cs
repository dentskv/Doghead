using Core.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UnityEngine.SceneManagement;
using System;
using ScriptableObjects;
using TMPro;

namespace ViewControllers
{
    public class SelectLevelMenuController : ViewController<MainViewController.MainStates>
    {
        [SerializeField] private TMP_Text levelNumberText;
        [SerializeField] private Button playButton;
        [SerializeField] private Button backButton;
        [SerializeField] private Sprite fullStar;
        [SerializeField] private Sprite emptyStar;
        [SerializeField] private Image[] stars;
        
        [Inject] private IStateMachine<MainViewController.MainStates> stateMachine;
        [Inject] private StarPreset starPreset;
        
        public override MainViewController.MainStates ViewState => MainViewController.MainStates.SelectLevel;
        
        public override void Initialize()
        {
            playButton.onClick.AddListener(PlayButtonClick);
            backButton.onClick.AddListener(BackButtonClick);
        }

        private void OnEnable()
        {
            UpdateStars();
        }
        
        private void OnDisable()
        {
            ReturnStars();
        }

        private void UpdateStars()
        {
            for (int i = 0; i < starPreset.stars[Int32.Parse(levelNumberText.text) - 1].starsAmount; i++)
            {
                stars[i].sprite = fullStar;
            }
        }

        private void ReturnStars()
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].sprite = emptyStar;
            }
        }

        private void PlayButtonClick()
        {
            SceneManager.LoadScene(Int32.Parse(levelNumberText.text));
        }
        
        private void BackButtonClick()
        {
            stateMachine.Fire(MainViewController.MainStates.Chapters);
        }
    }
}
