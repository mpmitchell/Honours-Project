using UnityEngine;

public class Health : MonoBehaviour {

  [SerializeField] [Range(1, 10)] int health;
  [SerializeField] [Range(0.0f, 1.0f)] float invincibilityFrames;

  Animator animator;
  float invincible;

  void Start() {
    animator = GetComponent<Animator>();
  }

  void Update() {
    if (invincible > 0) {
      invincible -= Time.deltaTime;
      if (invincible <= 0) {
        animator.SetTrigger("Recover");
      }
    }
  }

  void Damage(int damage) {
    if (invincible <= 0) {
      if ((health -= damage) <= 0) {
        animator.SetTrigger("Dead");
        SendMessage("Dying");
      } else {
        animator.SetTrigger("Hit");
        invincible = invincibilityFrames;
      }
    }
  }

  public bool IsInvincible() {
    return invincible > 0;
  }

  public int GetHealth() {
    return health;
  }
}
