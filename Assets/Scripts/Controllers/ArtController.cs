using System;
using System.Runtime.InteropServices;
using GameObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers {
    public class ArtController : MonoBehaviour {
        public int artValue;
        public AudioSource audioSource;
        
        private ScoreController _scoreController;
        private bool _disabled = false;
        private float _wait = 1f;

        private void Start() {
            _scoreController = Game.GetScoreController();
        }

        public void StealArt() {
            _scoreController.AddScore(artValue);
            _disabled = true;
            audioSource.Play();
        }

        private void Update() {
            if (_disabled) {
                if (_wait > 0f) {
                    _wait -= Time.deltaTime;
                }
                else {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}