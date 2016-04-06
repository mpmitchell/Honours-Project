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
    CollisionCheck(ref translation.x, ref translation.y);
    distance += translation.magnitude;

    if (translation == Vector3.zero) {
      SelectDirection();
    } else if (distance >= 1.0f) {
      distance = 0.0f;

      if (Random.value <= 1.0f / changeChance) {
        SelectDirection();
      }
    } else {
      SetMoving(translation.x, translation.y);
      transform.Translate(translation);
    }
  }

  void SelectDirection() {
    if (Random.value <= 1.0f / changeChance) {
      distance = 0.0f;

      if (direction == Direction.Left || direction == Direction.Right) {
        direction = Random.value <= 0.5f ? Direction.Up : Direction.Down;
      } else {
        direction = Random.value <= 0.5f ? Direction.Left : Direction.Right;
      }

      UpdateDirection();
    }
  }
}
