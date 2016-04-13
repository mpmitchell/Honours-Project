using UnityEngine;
using UnityEngine.SceneManagement;

public class Crown : MonoBehaviour {

  void OnTriggerEnter2D(Collider2D collider) {
    Logger.Log("win");
    SceneManager.LoadScene(3);
  }
}
