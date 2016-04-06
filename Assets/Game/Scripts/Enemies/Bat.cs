using UnityEngine;

public class Bat : Entity {

  [SerializeField] [Range(1, 10)] int speed;
  [SerializeField] [Range(1, 10)] int projectionFactor;

  void Start() {
    directionVector = Random.insideUnitCircle.normalized;
  }

  void Update() {
    Vector3 projectedPosition = transform.position + directionVector * speed * Time.deltaTime * projectionFactor;
    Vector3 deltaPosition = projectedPosition - transform.position;
    RaycastHit2D hit = Physics2D.Raycast(transform.position, deltaPosition, deltaPosition.magnitude, obstacleLayerMask);

    while (hit.collider != null) {
      directionVector = Random.insideUnitCircle.normalized;
      projectedPosition = transform.position + directionVector * speed * Time.deltaTime * projectionFactor;
      deltaPosition = projectedPosition - transform.position;
      hit = Physics2D.Raycast(transform.position, deltaPosition, deltaPosition.magnitude, obstacleLayerMask);
    }

    transform.Translate(directionVector * speed * Time.deltaTime);
  }
}
