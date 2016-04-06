using UnityEngine;

public class SpikedBlock : MovingEntity {

  [HideInInspector] public int directions;
  [SerializeField] [Range(1, 10)] int speed;
  [SerializeField] LayerMask triggerLayerMask;

  public enum State {
    Idle, Moving, HitObstacle
  }

  State state = State.Idle;
  Vector3 initialPosition;

  void Awake() {
    // Override MovingEntity.Awake()
    colliderExtents = GetComponent<BoxCollider2D>().bounds.extents;
    initialPosition = transform.position;
  }

  void OnEnable() {
    // Override Entity.OnEnabled();
  }

  void Update() {
    switch (state) {
      case State.Idle: {
        // Check if player in Range
        if ((directions & (int)Direction.Left) != 0) {
          RaycastHit2D hit = Physics2D.BoxCast(transform.position, colliderExtents, 0.0f, Vector2.left, Mathf.Infinity, triggerLayerMask.value);
          Debug.DrawLine(transform.position, transform.position + Vector3.left * 10.0f);

          if (hit.collider && hit.collider.gameObject.tag == "Player") {
            state = State.Moving;
            direction = Direction.Left;
          }
        }

        if ((directions & (int)Direction.Right) != 0) {
          RaycastHit2D hit = Physics2D.BoxCast(transform.position, colliderExtents, 0.0f, Vector2.right, Mathf.Infinity, triggerLayerMask.value);
          Debug.DrawLine(transform.position, transform.position + Vector3.right * 10.0f);

          if (hit.collider && hit.collider.gameObject.tag == "Player") {
            state = State.Moving;
            direction = Direction.Right;
          }
        }

        if ((directions & (int)Direction.Down) != 0) {
          RaycastHit2D hit = Physics2D.BoxCast(transform.position, colliderExtents, 0.0f, Vector2.down, Mathf.Infinity, triggerLayerMask.value);
          Debug.DrawLine(transform.position, transform.position + Vector3.down * 10.0f);

          if (hit.collider && hit.collider.gameObject.tag == "Player") {
            state = State.Moving;
            direction = Direction.Down;
          }
        }

        if ((directions & (int)Direction.Up) != 0) {
          RaycastHit2D hit = Physics2D.BoxCast(transform.position, colliderExtents, 0.0f, Vector2.up, Mathf.Infinity, triggerLayerMask.value);
          Debug.DrawLine(transform.position, transform.position + Vector3.up * 10.0f);

          if (hit.collider && hit.collider.gameObject.tag == "Player") {
            state = State.Moving;
            direction = Direction.Up;
          }
        }
        break;
      }

      case State.Moving: {
        // Move in that direction
        Vector3 translation = GetDirectionVector() * speed * Time.deltaTime;

        if (CollisionCheck(ref translation.x, ref translation.y)) {
          // When hit obstacle move back to starting point
          state = State.HitObstacle;

          switch (direction) {
            case Direction.Left: {
              direction = Direction.Right;
              break;
            }

            case Direction.Right: {
              direction = Direction.Left;
              break;
            }

            case Direction.Down: {
              direction = Direction.Up;
              break;
            }

            case Direction.Up: {
              direction = Direction.Down;
              break;
            }
          }
        } else {
          transform.Translate(translation);
        }
        break;
      }

      case State.HitObstacle: {
        Vector3 translation = GetDirectionVector() * speed * Time.deltaTime;

        // If moved beyond initialPosition become Idle
        if (translation.x < 0.0f && transform.position.x <= initialPosition.x ||
            translation.x > 0.0f && transform.position.x >= initialPosition.x ||
            translation.y < 0.0f && transform.position.y <= initialPosition.y ||
            translation.y > 0.0f && transform.position.y >= initialPosition.y) {
          state = State.Idle;
        } else {
          transform.Translate(translation);
        }
        break;
      }
    }
  }
}
