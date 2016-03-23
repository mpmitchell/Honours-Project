using UnityEngine;

public class PlayerController : MonoBehaviour {

  [Range(1, 10)] [SerializeField] int speed;
  [Range(1, 10)] [SerializeField] int runningSpeed;

  const float BOX_CAST_DISTANCE = 0.1f;
  static int wallLayerMask;
  static Vector2 colliderExtents;

  Animator animator;
  bool moving = false;
  Direction direction = Direction.Down;
  [HideInInspector] public bool attacking = false;

  void Awake() {
    wallLayerMask = LayerMask.GetMask("Wall");
    colliderExtents = GetComponent<BoxCollider2D>().bounds.extents;
    animator = GetComponent<Animator>();
    animator.GetBehaviour<PlayerAnimator>().controller = this;
  }

  void Update() {
    if (!attacking) {
      float dx = Input.GetAxis("Horizontal");
      float dy = Input.GetAxis("Vertical");
      bool running = Input.GetButton("Run");

      if (running) {
        dx *= runningSpeed * Time.deltaTime;
        dy *= runningSpeed * Time.deltaTime;
      } else {
        dx *= speed * Time.deltaTime;
        dy *= speed * Time.deltaTime;
      }

      SetDirection(dx, dy);
      CollisionCheck(ref dx, ref dy);
      SetMoving(dx, dy);

      transform.Translate(new Vector3(dx, dy, 0.0f));

      if (Input.GetButtonDown("Attack")) {
        animator.SetTrigger("Attack");
        attacking = true;
        moving = false;
      }

      if (Input.GetButtonDown("Fire Arrow")) {
        SendMessage("FireProjectile", direction);
      }
    }
  }

  void SetDirection(float dx, float dy) {
    if (dx < 0.0f) {
      direction = Direction.Left;
      animator.SetInteger("Direction", (int)direction);
    } else if (dx > 0.0f) {
      direction = Direction.Right;
      animator.SetInteger("Direction", (int)direction);
    }

    if (dy < 0.0f) {
      direction = Direction.Down;
      animator.SetInteger("Direction", (int)direction);
    } else if (dy > 0.0f) {
      direction = Direction.Up;
      animator.SetInteger("Direction", (int)direction);
    }
  }

  void CollisionCheck(ref float dx, ref float dy) {
    float left =   transform.position.x + dx - colliderExtents.x;
    float right =  transform.position.x + dx + colliderExtents.x;
    float bottom = transform.position.y + dy - colliderExtents.y;
    float top =    transform.position.y + dy + colliderExtents.y;

    if (dx < 0.0f) {
      RaycastHit2D hitBottom = Physics2D.Raycast(new Vector2(left, bottom), Vector2.left, BOX_CAST_DISTANCE, wallLayerMask);
      RaycastHit2D hitTop = Physics2D.Raycast(new Vector2(left, top), Vector2.left, BOX_CAST_DISTANCE, wallLayerMask);
      if (hitBottom.collider != null) {
        dx = 0.0f;
        hitBottom.collider.SendMessage("PushLeft", SendMessageOptions.DontRequireReceiver);
      } else if (hitTop.collider != null) {
        dx = 0.0f;
        hitTop.collider.SendMessage("PushLeft", SendMessageOptions.DontRequireReceiver);
      }
    } else if (dx > 0.0f) {
      RaycastHit2D hitBottom = Physics2D.Raycast(new Vector2(right, bottom), Vector2.right, BOX_CAST_DISTANCE, wallLayerMask);
      RaycastHit2D hitTop = Physics2D.Raycast(new Vector2(right, top), Vector2.right, BOX_CAST_DISTANCE, wallLayerMask);
      if (hitBottom.collider != null) {
        dx = 0.0f;
        hitBottom.collider.SendMessage("PushRight", SendMessageOptions.DontRequireReceiver);
      } else if (hitTop.collider != null) {
        dx = 0.0f;
        hitTop.collider.SendMessage("PushRight", SendMessageOptions.DontRequireReceiver);
      }
    }

    if (dy < 0.0f) {
      RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(left, bottom), Vector2.down, BOX_CAST_DISTANCE, wallLayerMask);
      RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(right, bottom), Vector2.down, BOX_CAST_DISTANCE, wallLayerMask);
      if (hitLeft.collider != null) {
        dy = 0.0f;
        hitLeft.collider.SendMessage("PushDown", SendMessageOptions.DontRequireReceiver);
      } else if (hitRight.collider != null) {
        dy = 0.0f;
        hitRight.collider.SendMessage("PushDown", SendMessageOptions.DontRequireReceiver);
      }
    } else if (dy > 0.0f) {
      RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(left, top), Vector2.up, BOX_CAST_DISTANCE, wallLayerMask);
      RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(right, top), Vector2.up, BOX_CAST_DISTANCE, wallLayerMask);
      if (hitLeft.collider != null) {
        dy = 0.0f;
        hitLeft.collider.SendMessage("PushUp", SendMessageOptions.DontRequireReceiver);
      } else if (hitRight.collider != null) {
        dy = 0.0f;
        hitRight.collider.SendMessage("PushUp", SendMessageOptions.DontRequireReceiver);
      }
    }
  }

  void SetMoving(float dx, float dy) {
    if (dx == 0.0f && dy == 0.0f) {
      if (moving) {
        animator.SetTrigger("StoppedMoving");
        moving = false;
      }
    } else {
      if (!moving) {
        animator.SetTrigger("StartedMoving");
        moving = true;
      }
    }
  }
}
