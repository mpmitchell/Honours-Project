using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuButtons : MonoBehaviour {

  static bool pcg = false;

  public void Play() {
    Logger.OpenFile();
    Seed.seed = Random.seed;
    Room.rooms = new LinkedList<Room>();
    Room.defaultColour = Color.black;
    Room.highlightGoal = false;

    if (Random.value <= 0.5f) {
      SceneManager.LoadScene(1);
      pcg = true;
    } else {
      Logger.Log("Prefab Level");
      pcg = false;
      SceneManager.LoadScene(2);
    }
  }

  public void ReturnToMenu() {
    SceneManager.LoadScene(0);
  }

  public void Replay() {
    Logger.OpenFile();
    Random.seed = Seed.seed;
    Room.rooms = new LinkedList<Room>();
    Room.defaultColour = Color.black;
    Room.highlightGoal = false;

    if (pcg) {
      SceneManager.LoadScene(1);
    } else {
      Logger.Log("Prefab Level");
      SceneManager.LoadScene(2);
    }
  }

  public void Exit() {
    Application.Quit();
  }
}
