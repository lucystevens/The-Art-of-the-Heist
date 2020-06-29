using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers {
    public class ScoreController : MonoBehaviour {
        public int score = 0;
        public TextMeshProUGUI scoreText;

        private string initialText;
        void Start() {
            initialText = scoreText.text;
            UpdateScoreText();
        }

        public void AddScore(int amount) {
            score += amount;
            UpdateScoreText();
        }

        public void UpdateScoreText() {
            if (scoreText != null) {
                scoreText.text = initialText + FormatScore();
            }
        }

        public string FormatScore() {
            if (score > 1000) {
                return ((float)score/1000).ToString("n2") + "m";
            }
            else {
                return score + "k";
            }
        }
    }
}