using System;
using UnityEngine;

namespace Controllers {
    public class PlayerController : MonoBehaviour {
        private MoveWithInput _moveScript;
        private RotateWithInput _rotateScript;

        private void Start() {
            _moveScript = GetComponent<MoveWithInput>();
            _rotateScript = GetComponent<RotateWithInput>();
        }

        public void DisableMovement() {
            _moveScript.enabled = false;
            _rotateScript.enabled = false;
        }
        
        public void EnableMovement() {
            _moveScript.enabled = true;
            _rotateScript.enabled = true;
        }
    }
}