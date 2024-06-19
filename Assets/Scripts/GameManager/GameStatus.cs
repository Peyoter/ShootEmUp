using System.Collections;
using GameSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameManager
{
    public sealed class GameStatus : MonoBehaviour, IGameResumeListener, IGamePauseListener, IGameFinishLister
    {
        private int _currentTime = 3;
        [SerializeField] private GameStateManager GameStateManager;
        [SerializeField] private TMP_Text TimerText;

        [SerializeField] private GameObject StartGameButton;
        [SerializeField] private GameObject PauseGameButton;

        private Coroutine _countdownCoroutine;

        public void Awake()
        {
            IGameListener.Register(this);
            var startButton = StartGameButton.GetComponent<Button>();
            var pauseButton = PauseGameButton.GetComponent<Button>();

            startButton.onClick.AddListener(ResumeGame);
            pauseButton.onClick.AddListener(PauseGame);
        }

        private void ResumeGame()
        {
            GameStateManager.Resume();
        }

        private void PauseGame()
        {
            GameStateManager.PauseGame();
        }

        public void OnFinishGame()
        {
            TimerText.text = "GAMEOVER";
            HidePauseButton();
        }

        public void OnPauseGame()
        {
            ShowStartButton();
            HidePauseButton();
            if (_countdownCoroutine != null)
            {
                StopCoroutine(_countdownCoroutine);
            }
        }

        public void OnResumeGame()
        {
            HideStartButton();
            _countdownCoroutine = StartCoroutine(Countdown());
        }

        public void ShowPauseButton()
        {
            PauseGameButton.SetActive(true);
        }

        public void ShowStartButton()
        {
            StartGameButton.SetActive(true);
        }

        public void HidePauseButton()
        {
            PauseGameButton.SetActive(false);
        }

        public void HideStartButton()
        {
            StartGameButton.SetActive(false);
        }

        IEnumerator Countdown()
        {
            var prvTime = _currentTime;
            while (_currentTime > 0)
            {
                TimerText.text = _currentTime.ToString();
                yield return new WaitForSeconds(1f);
                _currentTime--;
            }

            TimerText.text = _currentTime.ToString();
            if (_currentTime == 0)
            {
                TimerText.text = "";
            }

            StartGame();
            _currentTime = prvTime;
        }

        private void StartGame()
        {
            ShowPauseButton();
            GameStateManager.StartGame();
        }
    }
}