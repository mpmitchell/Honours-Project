using UnityEngine;

public class Damage : MonoBehaviour {

  [SerializeField] [Range(1, 10)] int damage;
  [SerializeField] LayerMask targetLayers;

  void OnTriggerEnter2D(Collider2D collider) {
    if (((1 << collider.gameObject.layer) & targetLayers) != 0) {
      collider.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
    }
  }
}
