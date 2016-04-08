using UnityEngine;

public class Key : MonoBehaviour {

  void OnTriggerEnter2D(Collider2D collider) {
    PlayerController.player.SendMessage("AddKey");
    Destroy(gameObject);
  }
}
