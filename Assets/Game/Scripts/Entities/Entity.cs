using UnityEngine;

public class Entity : MonoBehaviour {

  void Start() {
     GetComponent<Animator>().GetBehaviour<DeathAnimator>().target = gameObject;
  }

  protected virtual void Dying() {
    enabled = false;
    GetComponent<Collider2D>().enabled = false;
  }

  protected virtual void Dead() {
    Destroy(gameObject);
  }
}
