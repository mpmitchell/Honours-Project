using UnityEngine;

public class PlayerController : MovingEntity {

  [SerializeField] [Range(1, 10)] int speed;
  [SerializeField] [Range(1, 10)] int runningSpeed;

  [HideInInspector] public static GameObject player;

  [HideInInspector] public bool attacking = false;

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

  protected override void Dead() {
    // Game Over
  }
}
