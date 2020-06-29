using System;
using GameObjects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers {
    public class GameController : MonoBehaviour {
        public AudioSource endGameMusic;
        public TextMeshProUGUI winText;
        public TextMeshProUGUI failText;
        public GameObject retryButton;
        public GameObject exitButton;
        public TextMeshProUGUI scoreText;

        private ScoreController _scoreController;
        private PromptController _promptController;
        private PlayerController _playerController;
        private bool _gameEnded = false;

        private void Start() {
            _scoreController = Game.GetScoreController();
            _promptController = Game.GetPromptController();
            _playerController = Game.GetPlayer().GetComponent<PlayerController>();
        }

        public void PauseGame () {
            Time.timeScale = 0;
        }

        public void ResumeGame () {
            Time.timeScale = 1;
        }

        public void ExitGame() {
            Application.Quit();
        }
        
        public void LoadScene(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }

        public void EndGameAsFailure() {
            failText.enabled = true;
            EndGame();
        }

        public void EndGameAsSuccess() {
            winText.text = winText.text.Replace("${value}", "£" + _scoreController.FormatScore());
            winText.enabled = true;
            EndGame();
        }

        private void EndGame() {
            _gameEnded = true;
            scoreText.enabled = false;
            retryButton.SetActive(true);
            exitButton.SetActive(true);
            endGameMusic.Play();
            _promptController.HidePrompt();
            _playerController.DisableMovement();
        }

        public bool HasGameEnded() {
            return _gameEnded;
        }
        
    }
}