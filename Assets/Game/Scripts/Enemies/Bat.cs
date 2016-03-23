using UnityEngine;

public class Bat : MonoBehaviour {

  [SerializeField] [Range(1, 10)] int speed;
  [SerializeField] LayerMask wallLayerMask;
  [SerializeField] [Range(1, 10)] int projectionFactor;

  Vector3 direction;

  void Awake() {
    direction = Random.insideUnitCircle.normalized;
  }

  void Update() {
    Vector3 projectedPosition = transform.position + direction * speed * Time.deltaTime * projectionFactor;
    Vector3 deltaPosition = projectedPosition - transform.position;
    RaycastHit2D hit = Physics2D.Raycast(transform.position, deltaPosition, deltaPosition.magnitude, wallLayerMask);
    Debug.DrawLine(transform.position, projectedPosition);

    if (hit.collider != null) {
      direction = Random.insideUnitCircle.normalized;
      projectedPosition = transform.position + direction * speed * Time.deltaTime * projectionFactor;
      deltaPosition = projectedPosition - transform.position;
      hit = Physics2D.Raycast(transform.position, deltaPosition, deltaPosition.magnitude, wallLayerMask);
      Debug.DrawLine(transform.position, projectedPosition);
    }

    transform.Translate(direction * speed * Time.deltaTime);
  }
}
