using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers {
    public class IntroController : MonoBehaviour {
        public void StartGame() {
            SceneManager.LoadScene("Level01");
        }
        
        public void ExitGame() {
            Application.Quit();
        }
    }
}