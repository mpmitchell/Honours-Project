using UnityEngine;

public class Damage : MonoBehaviour {

  [SerializeField] [Range(1, 10)] int damage;
  [SerializeField] bool canBreak;
  [SerializeField] LayerMask targetLayers;

  void OnTriggerEnter2D(Collider2D collider) {
    if (((1 << collider.gameObject.layer) & targetLayers) != 0) {
      collider.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);

      Vector3 position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
      Vector3 colliderPosition = new Vector3(Mathf.Round(collider.transform.position.x), Mathf.Round(collider.transform.position.y));
      Vector3 direction = new Vector3();

      if (colliderPosition.x < position.x) {
        direction.x = -1.0f;
      } else if (colliderPosition.x > position.x) {
        direction.x = 1.0f;
      }

      if (colliderPosition.y < position.y) {
        direction.y = -1.0f;
      } else if (colliderPosition.y > position.y) {
        direction.y = 1.0f;
      }

      collider.SendMessage("Knock", direction, SendMessageOptions.DontRequireReceiver);
    } else if (canBreak && collider.tag == "Breakable") {
      collider.SendMessage("Break");
    }
  }
}
