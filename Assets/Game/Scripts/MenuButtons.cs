using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuButtons : MonoBehaviour {

  public void Play() {
    Logger.OpenFile();
    Logger.Log("Prefabbed Level");
    Room.rooms = new LinkedList<Room>();
    Room.defaultColour = Color.black;
    Room.highlightGoal = false;
    SceneManager.LoadScene(2);
  }

  public void ReturnToMenu() {
    SceneManager.LoadScene(0);
  }

  public void Replay() {
    Logger.OpenFile();
    Logger.Log("Prefabbed Level");
    Room.rooms = new LinkedList<Room>();
    Room.defaultColour = Color.black;
    Room.highlightGoal = false;
    SceneManager.LoadScene(2);
  }

  public void Exit() {
    Application.Quit();
  }
}
