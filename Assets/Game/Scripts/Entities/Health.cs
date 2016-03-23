using UnityEngine;

public class Health : MonoBehaviour {

  [SerializeField] [Range(1, 10)] int health;

  void Damage(int damage) {
    if ((health -= damage) <= 0) {
      // Send Dying message
      // Start death animation
    }
  }
}
