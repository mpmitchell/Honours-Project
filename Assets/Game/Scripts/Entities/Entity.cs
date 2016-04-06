using UnityEngine;

public class Entity : MonoBehaviour {

  [SerializeField] protected LayerMask obstacleLayerMask;

  protected Animator animator;
  protected Vector3 directionVector;

  void Awake() {
    animator = GetComponent<Animator>();
    animator.GetBehaviour<DeathAnimator>().target = gameObject;
  }

  public virtual Vector3 GetDirectionVector() {
    return directionVector;
  }

  protected virtual void Dying() {
    enabled = false;
    GetComponent<Collider2D>().enabled = false;
  }

  protected virtual void Dead() {
    Destroy(gameObject);
  }
}
