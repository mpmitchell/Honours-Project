using UnityEngine;

public class BoomerangGuy : MovingEntity {

  [SerializeField] [Range(1, 10)] int speed;
  [SerializeField] [Range(1, 100)] int changeChance;
  [SerializeField] [Range(1, 100)] int fireChance;

  float distance;
  bool attacking = false;

  void Start() {
    SelectDirection();
  }

  void Update() {
    if (!attacking) {
      Vector3 translation = GetDirectionVector() * speed * Time.deltaTime;
      CollisionCheck(ref translation.x, ref translation.y);
      distance += translation.magnitude;

      if (translation == Vector3.zero) {
        SelectDirection();
      } else if (distance >= 1.0f) {
        distance = 0.0f;

        if (Random.value <= 1.0f / changeChance) {
          SelectDirection();
        } else if (Random.value <= 1.0f / fireChance) {
          SetMoving(0.0f, 0.0f);
          SendMessage("FireProjectile", direction);
          attacking = true;
        }
      } else {
        SetMoving(translation.x, translation.y);
        transform.Translate(translation);
      }
    }
  }

  void ProjectileReturned() {
    attacking = false;
    SelectDirection();
  }

  void SelectDirection() {
    distance = 0.0f;

    if (direction == Direction.Left || direction == Direction.Right) {
      direction = Random.value <= 0.5f ? Direction.Up : Direction.Down;
    } else {
      direction = Random.value <= 0.5f ? Direction.Left : Direction.Right;
    }

    UpdateDirection();
  }
}
