using UnityEngine;

public class Explosion : MonoBehaviour {

  void Awake() {
    GetComponent<Animator>().GetBehaviour<ExplosionAnimator>().gameObject = gameObject;
  }

  void OnTriggerEnter2D(Collider2D collider) {
    collider.SendMessage("Explosion", SendMessageOptions.DontRequireReceiver);
    collider.SendMessage("Break", SendMessageOptions.DontRequireReceiver);
  }
}
