using System;
using TMPro;
using UnityEngine;

namespace Controllers {
    public class PromptController : MonoBehaviour {
        
        public TextMeshProUGUI promptText;

        private string _template;

        private void Start() {
            _template = promptText.text;
        }

        public void ShowPrompt(KeyCode key, string action) {
            promptText.text = FormatPrompt(key, action);
            promptText.enabled = true;
        }

        public void HidePrompt() {
            promptText.enabled = false;
        }

        private string FormatPrompt(KeyCode key, string action) {
            return _template.Replace("${key}", key.ToString())
                .Replace("${action}", action);
        }
    }
}