using UnityEngine;

public class Skeleton : MovingEntity {

  [SerializeField] [Range(1, 10)] int speed;
  [SerializeField] [Range(1, 10)] int changeChance;

  float distance;

  void Start() {
    SelectDirection();
  }

  void Update() {
    Vector3 translation = GetDirectionVector() * speed * Time.deltaTime;
    distance += translation.magnitude;

    CollisionCheck(ref translation.x, ref translation.y);
    SetMoving(translation.x, translation.y);

    if (distance >= 1.0f || translation == Vector3.zero) {
      distance = 0.0f;
      SelectDirection();
    } else {
      transform.Translate(translation);
    }
  }

  void SelectDirection() {
    if (Random.value <= 1.0f / changeChance) {
      if (direction == Direction.Left || direction == Direction.Right) {
        direction = Random.value > 0.5f ? Direction.Up : Direction.Down;
      } else {
        direction = Random.value > 0.5f ? Direction.Left : Direction.Right;
      }
    }

    UpdateDirection();
  }
}
