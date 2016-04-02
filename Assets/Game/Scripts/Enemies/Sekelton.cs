using UnityEngine;

public class Sekelton : MovingEntity {

  [SerializeField] [Range(1, 10)] int speed;

  public float dx;
  public float dy;

  public float distance;

  void Start() {
    SelectDirection();
    SetDirection(dx, dy);
    SetMoving(dx, dy);
  }

  void Update() {
    distance += speed * Time.deltaTime;

    if (distance >= 1.0f) {
      distance = 0.0f;
      SelectDirection();
      SetDirection(dx, dy);
    } else {
      Vector3 translation = new Vector3(dx, dy, 0.0f);
      transform.Translate(translation * speed * Time.deltaTime);
    }
  }

  void SelectDirection() {
    do {
      if (Random.value > 0.5f) {
        dx = Random.value > 0.5f ? -1.0f : 1.0f;
        dy = 0.0f;
      } else {
        dy = Random.value > 0.5f ? -1.0f : 1.0f;
        dx = 0.0f;
      }
    } while (CollisionCheck(ref dx, ref dy));
  }
}
