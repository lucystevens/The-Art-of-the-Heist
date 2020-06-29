using System;
using Controllers;
using UnityEngine;

namespace GameObjects {
    public static class Game {
        private static GameObject _player;
        private static ScoreController _scoreController;
        private static PromptController _promptController;
        private static GameController _gameController;
        

        public static GameObject GetPlayer() {
            if (_player == null) {
                _player = GameObject
                    .FindGameObjectWithTag("Player");
            }
            return _player;
        }

        public static ScoreController GetScoreController() {
            if (_scoreController == null) {
                _scoreController = GameObject
                    .FindGameObjectWithTag("ScoreController")
                    .GetComponent<ScoreController>();
            }
            return _scoreController;
        }
        
        public static PromptController GetPromptController() {
            if (_promptController == null) {
                _promptController = GameObject
                    .FindGameObjectWithTag("PromptController")
                    .GetComponent<PromptController>();
            }
            return _promptController;
        }
        
        public static GameController GetGameController() {
            if (_gameController == null) {
                _gameController = GameObject
                    .FindGameObjectWithTag("GameController")
                    .GetComponent<GameController>();
            }
            return _gameController;
        }
    }
}