using System;
using Controllers;
using GameObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Collisions {
    public class InteractableObjectCollider : MonoBehaviour {
        public KeyCode triggerKey = KeyCode.Space;
        public UnityEvent interaction;
        public AnimationTrigger playerAnimation;
        public string actionName;
        
        private Animator _playerAnim;
        private bool _playerNearby = false;
        private bool _interactionComplete = false;
        private GameController _gameController;
        private PromptController _promptController;
        private bool _promptShown = false;

        private void Start() {
            _playerAnim = Game.GetPlayer().GetComponent<Animator>();
            _promptController = Game.GetPromptController();
            _gameController = Game.GetGameController();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (GameTag.Player.CheckObject(other)) {
                _playerNearby = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (GameTag.Player.CheckObject(other)) {
                _playerNearby = false;
            }
        }

        private void Update() {
            // Check is game is over
            if (_gameController.HasGameEnded()) {
                return;
            }
            
            if (_playerNearby && !_interactionComplete) {
                _promptController.ShowPrompt(triggerKey, actionName);
                _promptShown = true;
            }
            else if(_promptShown){
                _promptController.HidePrompt();
                _promptShown = false;
            }
            
            if (Input.GetKey(triggerKey) && _playerNearby && !_interactionComplete) {
                playerAnimation.SetOnAnimator(_playerAnim);
                interaction.Invoke();
                _interactionComplete = true;
                _promptController.HidePrompt();
                _promptShown = false;
            }

        }
        
        
    }
}