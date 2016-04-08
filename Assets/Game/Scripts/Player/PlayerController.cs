using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MovingEntity {

  [SerializeField] [Range(1, 10)] int speed;

  [HideInInspector] public static PlayerController instance = null;
  [HideInInspector] public static GameObject player = null;

  [HideInInspector] public bool attacking = false;

  int keyCount = 0;

  void Start() {
    if (instance) {
      Debug.LogError("Player already exists");
    }

    animator.GetBehaviour<PlayerAnimator>().controller = this;
    player = gameObject;
    instance = this;
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
        Camera.main.SendMessage("UseKey");
      }

      transform.Translate(new Vector3(dx, dy, 0.0f));

      if (Input.GetButtonDown("Attack")) {
        animator.SetTrigger("Attack");
        attacking = true;
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
      Camera.main.SendMessage("CollectKey");
    } else if (collider.gameObject.tag == "Crown") {
      SceneManager.LoadScene(1);
    }
  }

  void Damage(int damage) {
    Camera.main.SendMessage("Damage", damage);
  }

  protected override void Dead() {
    SceneManager.LoadScene(2);
  }
}
