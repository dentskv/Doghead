using Core.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

namespace ViewControllers
{
    public class ChaptersViewController : ViewController<MainViewController.MainStates>
    {
        public override MainViewController.MainStates ViewState => MainViewController.MainStates.Chapters;

        [Inject] private IStateMachine<MainViewController.MainStates> stateMachine;

        [SerializeField] private TMP_Text selectedLevelText;
        [SerializeField] private Button[] levelButtons;
        [SerializeField] private Button shopButton;
        [SerializeField] private Button backButton;

        public override void Initialize()
        {
            for (int i = 0; i < levelButtons.Length; i++)
            {
                int indexButton = i;
                levelButtons[indexButton].onClick.AddListener(() => PlayButtonClick(indexButton));
            }
            backButton.onClick.AddListener(BackButtonClick);
            shopButton.onClick.AddListener(ShopButtonClick);
        }

        private void PlayButtonClick(int index)
        {
            index++;
            selectedLevelText.SetText("" + index);
            stateMachine.Fire(MainViewController.MainStates.SelectLevel);
        }

        private void ShopButtonClick()
        {
            stateMachine.Fire(MainViewController.MainStates.Shop);
        }

        private void BackButtonClick()
        {
            stateMachine.Fire(MainViewController.MainStates.Main);
        }
    }
}
