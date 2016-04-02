using UnityEngine;

public class Entity : MonoBehaviour {

  [SerializeField] protected LayerMask obstacleLayerMask;

  protected Animator animator;

  void Awake() {
    animator = GetComponent<Animator>();
    animator.GetBehaviour<DeathAnimator>().target = gameObject;
  }

  protected virtual void Dying() {
    enabled = false;
    GetComponent<Collider2D>().enabled = false;
  }

  protected virtual void Dead() {
    Destroy(gameObject);
  }
}
