using UnityEngine;

public class Knockback : MonoBehaviour {

  MovingEntity entity;

  void Start() {
    entity = GetComponent<MovingEntity>();
  }

  void Knock(Vector3 direction) {
    // Check for collisions
    entity.CollisionCheck(ref direction.x, ref direction.y);
    // Move backwards
    transform.Translate(direction);
  }
}
