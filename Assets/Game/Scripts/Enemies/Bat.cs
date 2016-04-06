using UnityEngine;

public class Bat : Entity {

  [SerializeField] [Range(1, 10)] int speed;
  [SerializeField] [Range(1, 10)] int projectionFactor;

  void Start() {
    direction = Random.insideUnitCircle.normalized;
  }

  void Update() {
    Vector3 projectedPosition = transform.position + direction * speed * Time.deltaTime * projectionFactor;
    Vector3 deltaPosition = projectedPosition - transform.position;
    RaycastHit2D hit = Physics2D.Raycast(transform.position, deltaPosition, deltaPosition.magnitude, obstacleLayerMask);

    while (hit.collider != null) {
      direction = Random.insideUnitCircle.normalized;
      projectedPosition = transform.position + direction * speed * Time.deltaTime * projectionFactor;
      deltaPosition = projectedPosition - transform.position;
      hit = Physics2D.Raycast(transform.position, deltaPosition, deltaPosition.magnitude, obstacleLayerMask);
    }

    transform.Translate(direction * speed * Time.deltaTime);
  }
}
