using UnityEngine;

public class Health : MonoBehaviour {

  [SerializeField] [Range(1, 10)] int health;

  Animator animator;

  void Start() {
    animator = GetComponent<Animator>();
  }

  void Damage(int damage) {
    if ((health -= damage) <= 0) {
      animator.SetTrigger("Dead");
      SendMessage("Dying");
    }
  }
}
