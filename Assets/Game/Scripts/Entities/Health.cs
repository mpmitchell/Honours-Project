using UnityEngine;

public class Health : MonoBehaviour {

  [SerializeField] [Range(1, 10)] int health;
  [SerializeField] [Range(1, 10)] int invincibilityFrames;

  Animator animator;
  bool invincible;

  void Start() {
    animator = GetComponent<Animator>();
  }

  void Damage(int damage) {
    if (!invincible) {
      if ((health -= damage) <= 0) {
        animator.SetTrigger("Dead");
        SendMessage("Dying");
      } else {
        animator.SetTrigger("Hit");
        invincible = true;
      }
    }
  }

  void Recover() {
    invincible = false;
  }
}
