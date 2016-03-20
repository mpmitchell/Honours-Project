using UnityEngine;

public class PlayerController : MonoBehaviour {

  [Range(1, 100)] [SerializeField] float speed;
  [Range(1, 100)] [SerializeField] float runningSpeed;

  bool running;
  float dx;
  float dy;

  Animator animator;

  static int wallLayerMask;
  static Vector2 colliderExtents;

  const float BOX_CAST_DISTANCE = 0.1f;

  void Awake() {
    animator = GetComponent<Animator>();
    wallLayerMask = LayerMask.GetMask("Wall");
    colliderExtents = GetComponent<BoxCollider2D>().bounds.extents;
  }

  void Update() {
    dx = Input.GetAxis("Horizontal");
    dy = Input.GetAxis("Vertical");
    bool running = Input.GetButton("Run");

    if (running) {
      dx *= runningSpeed * Time.deltaTime;
      dy *= runningSpeed * Time.deltaTime;
    } else {
      dx *= speed * Time.deltaTime;
      dy *= speed * Time.deltaTime;
    }

    CollisionCheck(ref dx, ref dy);
    Animate(dx, dy);

    transform.Translate(new Vector3(dx, dy, 0.0f));
  }

  void CollisionCheck(ref float dx, ref float dy) {
    float left =   transform.position.x + dx - colliderExtents.x;
    float right =  transform.position.x + dx + colliderExtents.x;
    float bottom = transform.position.y + dy - colliderExtents.y;
    float top =    transform.position.y + dy + colliderExtents.y;

    if (dx < 0) {
      RaycastHit2D hitBottom = Physics2D.Raycast(new Vector2(left, bottom), Vector2.left, BOX_CAST_DISTANCE, wallLayerMask);
      RaycastHit2D hitTop = Physics2D.Raycast(new Vector2(left, top), Vector2.left, BOX_CAST_DISTANCE, wallLayerMask);
      if (hitBottom.collider != null) {
        dx = 0;
        hitBottom.collider.SendMessage("PushLeft", SendMessageOptions.DontRequireReceiver);
      } else if (hitTop.collider != null) {
        dx = 0;
        hitTop.collider.SendMessage("PushLeft", SendMessageOptions.DontRequireReceiver);
      }
    } else if (dx > 0) {
      RaycastHit2D hitBottom = Physics2D.Raycast(new Vector2(right, bottom), Vector2.right, BOX_CAST_DISTANCE, wallLayerMask);
      RaycastHit2D hitTop = Physics2D.Raycast(new Vector2(right, top), Vector2.right, BOX_CAST_DISTANCE, wallLayerMask);
      if (hitBottom.collider != null) {
        dx = 0;
        hitBottom.collider.SendMessage("PushRight", SendMessageOptions.DontRequireReceiver);
      } else if (hitTop.collider != null) {
        dx = 0;
        hitTop.collider.SendMessage("PushRight", SendMessageOptions.DontRequireReceiver);
      }
    }

    if (dy < 0) {
      RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(left, bottom), Vector2.down, BOX_CAST_DISTANCE, wallLayerMask);
      RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(right, bottom), Vector2.down, BOX_CAST_DISTANCE, wallLayerMask);
      if (hitLeft.collider != null) {
        dy = 0;
        hitLeft.collider.SendMessage("PushDown", SendMessageOptions.DontRequireReceiver);
      } else if (hitRight.collider != null) {
        dy = 0;
        hitRight.collider.SendMessage("PushDown", SendMessageOptions.DontRequireReceiver);
      }
    } else if (dy > 0) {
      RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(left, top), Vector2.up, BOX_CAST_DISTANCE, wallLayerMask);
      RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(right, top), Vector2.up, BOX_CAST_DISTANCE, wallLayerMask);
      if (hitLeft.collider != null) {
        dy = 0;
        hitLeft.collider.SendMessage("PushUp", SendMessageOptions.DontRequireReceiver);
      } else if (hitRight.collider != null) {
        dy = 0;
        hitRight.collider.SendMessage("PushUp", SendMessageOptions.DontRequireReceiver);
      }
    }
  }

  void Animate(float dx, float dy) {
    if (dx < 0) {
      animator.SetInteger("Direction", (int)Direction.Left);
      animator.SetBool("Moving", true);
    } else if (dx > 0) {
      animator.SetInteger("Direction", (int)Direction.Right);
      animator.SetBool("Moving", true);
    }

    if (dy < 0) {
      animator.SetInteger("Direction", (int)Direction.Down);
      animator.SetBool("Moving", true);
    } else if (dy > 0) {
      animator.SetInteger("Direction", (int)Direction.Up);
      animator.SetBool("Moving", true);
    }

    if (dx == 0 && dy == 0) {
      animator.SetBool("Moving", false);
    }
  }
}
