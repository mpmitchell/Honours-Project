using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

  public void Play() {
    SceneManager.LoadScene(1);
  }

  public void ReturnToMenu() {
    SceneManager.LoadScene(0);
  }

  public void Replay() {
    SceneManager.LoadScene(1);
  }

  public void Exit() {
    Application.Quit();
  }
}
