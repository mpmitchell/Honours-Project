using UnityEngine;

public class Bow : MonoBehaviour {

  void OnTriggerEnter2D(Collider2D collider) {
    Logger.Log("pickup bow");
    PlayerController.instance.GetBow();
    Destroy(gameObject);
  }
}
