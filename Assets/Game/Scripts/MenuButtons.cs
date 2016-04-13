using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

  public void Play() {
    Logger.OpenFile();
    Seed.seed = Random.seed;
    SceneManager.LoadScene(1);
  }

  public void ReturnToMenu() {
    SceneManager.LoadScene(0);
  }

  public void Replay() {
    Logger.OpenFile();
    Random.seed = Seed.seed;
    SceneManager.LoadScene(1);
  }

  public void Exit() {
    Application.Quit();
  }
}
