using UnityEngine;
using UnityEngine.SceneManagement;

public class Crown : MonoBehaviour {

  void OnTriggerEnter2D(Collider2D collider) {
    SceneManager.LoadScene(1);
  }
}
