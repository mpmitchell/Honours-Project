using UnityEngine;

public class Arrow : MonoBehaviour {

  [Range(1, 10)] [SerializeField] int speed;

  [HideInInspector] public ArrowPool pool;

  Vector3 direction;

  void Update() {
    transform.Translate(direction * speed * Time.deltaTime, Space.World);
  }

  void OnCollisionEnter2D(Collision2D collision) {
    pool.ReturnArrow(gameObject);
  }

  public void Activate(Vector3 origin, Direction direction) {
    gameObject.SetActive(true);
    transform.position = origin;

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
}
