using UnityEngine;

public class PlayerController : MovingEntity {

  [SerializeField] [Range(1, 10)] int speed;

  [HideInInspector] public static GameObject player;

  [HideInInspector] public bool attacking = false;

  int keyCount = 0;

  void Start() {
    animator.GetBehaviour<PlayerAnimator>().controller = this;
    player = gameObject;
  }

  void Update() {
    if (!attacking) {
      float dx = Input.GetAxis("Horizontal");
      float dy = Input.GetAxis("Vertical");

      dx *= speed * Time.deltaTime;
      dy *= speed * Time.deltaTime;

      GameObject hit;

      SetDirection(dx, dy);
      CollisionCheck(ref dx, ref dy, out hit);
      SetMoving(dx, dy);

      if (hit && hit.tag == "Lock" && keyCount > 0) {
        keyCount--;
        hit.SendMessage("Open");
      }

      transform.Translate(new Vector3(dx, dy, 0.0f));

      if (Input.GetButtonDown("Attack")) {
        animator.SetTrigger("Attack");
        attacking = true;
        moving = false;
      }

      if (Input.GetButtonDown("Fire Arrow")) {
        SendMessage("FireProjectile", direction);
      }

      if (Input.GetButtonDown("Drop Bomb")) {
        SendMessage("DropBomb");
      }
    }
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (collider.gameObject.tag == "Key") {
      keyCount++;
      Destroy(collider.gameObject);
    } else if (collider.gameObject.tag == "Crown") {
      // Game Over
    }
  }

  protected override void Dead() {
    // Game Over
  }
}
