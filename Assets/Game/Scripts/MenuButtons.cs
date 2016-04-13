using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuButtons : MonoBehaviour {

  public void Play() {
    Logger.OpenFile();
    Seed.seed = Random.seed;
    Room.rooms = new LinkedList<Room>();
    Room.defaultColour = Color.black;
    Room.highlightGoal = false;
    SceneManager.LoadScene(1);
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
    SceneManager.LoadScene(1);
  }

  public void Exit() {
    Application.Quit();
  }
}
