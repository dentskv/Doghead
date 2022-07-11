using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Image gunImage;

    [Inject] private GunPreset gun;
    
    private void Start()
    {
        pauseButton.onClick.AddListener(PauseHandler);
        exitButton.onClick.AddListener(ExitHandler);
        continueButton.onClick.AddListener(ContinueHandler);
        gunImage.sprite = gun.guns[PlayerPrefs.GetInt("gunID")].gunSprite;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0f)
            {
                ContinueHandler();
            }
            else PauseHandler();
        }
    }

    private void PauseHandler()
    {
        Time.timeScale = 0f;
        pauseCanvas.SetActive(true);
    }

    private void ExitHandler()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private void ContinueHandler()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}
