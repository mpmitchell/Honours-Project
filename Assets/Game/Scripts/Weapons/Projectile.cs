using UnityEngine;

public class Projectile : MonoBehaviour {

  [SerializeField] [Range(1, 10)] int speed;
  [SerializeField] bool boomerang;
  [SerializeField] [Range(1, 10)] int distance;

  [HideInInspector] public ProjectileSpawner spawner;

  Vector3 direction;

  float distanceTraveled = 0.0f;
  bool boomeranged = false;

  void Update() {
    distanceTraveled += speed * Time.deltaTime;

    if (boomerang && distanceTraveled >= distance) {
      if (!boomeranged) {
        direction = -direction;
        distanceTraveled = 0.0f;
        boomeranged = true;
      } else {
        spawner.ReturnProjectile(gameObject);
      }
    } else {
      transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (boomerang && !boomeranged) {
      direction = -direction;
      distanceTraveled = distance - distanceTraveled;
      boomeranged = true;
    } else {
      spawner.ReturnProjectile(gameObject);
    }
  }

  public void Activate(Vector3 origin, Direction direction) {
    gameObject.SetActive(true);
    transform.position = origin;
    distanceTraveled = 0.0f;
    boomeranged = false;

    switch (direction) {
      case Direction.Left: {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 315.0f);
        this.direction = Vector3.left;
        break;
      }

      case Direction.Right: {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 135.0f);
        this.direction = Vector3.right;
        break;
      }

      case Direction.Down: {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 45.0f);
        this.direction = Vector3.down;
        break;
      }

      case Direction.Up: {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 225.0f);
        this.direction = Vector3.up;
        break;
      }
    }
  }

  public void Activate(Vector3 origin, Vector3 direction) {
    gameObject.SetActive(true);
    transform.position = origin;
    distanceTraveled = 0.0f;
    boomeranged = false;

    this.direction = direction;
  }
}
