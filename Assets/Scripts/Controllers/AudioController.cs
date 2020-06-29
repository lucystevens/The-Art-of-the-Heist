using System;
using UnityEngine;

namespace Controllers {
    public class AudioController : MonoBehaviour {
        public AudioSource audioSource;
        public Animator animator;
        public string animatorParameter;

        private bool _isPlaying = false;
        

        private void Update() {
            if (animator.GetBool(animatorParameter) && !_isPlaying) {
                audioSource.Play();
                _isPlaying = true;
            }
            else if(!animator.GetBool(animatorParameter) && _isPlaying){
                audioSource.Pause();
                _isPlaying = false;
            }
        }
    }
}