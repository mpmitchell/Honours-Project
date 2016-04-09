using UnityEngine;

public class Bow : MonoBehaviour {

  void OnTriggerEnter2D(Collider2D collider) {
    PlayerController.instance.GetBow();
    Destroy(gameObject);
  }
}
