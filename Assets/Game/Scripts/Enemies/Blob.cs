using UnityEngine;

public class Blob : Entity {

  [SerializeField] [Range(1, 10)] int speed;
  [SerializeField] [Range(1, 10)] int jumpingSpeed;
  [SerializeField] [Range(1, 10)] int jumpDistance;
  [SerializeField] [Range(1, 10)] int jumpCooldown;
  [SerializeField] LayerMask obstacleLayerMask;

  Animator animator;
  [HideInInspector] public bool jumping = false;
  float jumpCooldownTimer;

  void Awake() {
    animator = GetComponent<Animator>();
    animator.GetBehaviour<BlobAnimator>().controller = this;
  }

  void Update() {
  Vector3 direction = PlayerController.player.transform.position - transform.position;

    if (!jumping) {
      // Get distance
      float distance = Vector3.Distance(PlayerController.player.transform.position, transform.position);

      // If in distance jump
      // Else move toward player
      if (jumpCooldownTimer <= 0.0f && distance <= jumpDistance) {
        animator.SetTrigger("Jump");
        jumping = true;
        jumpCooldownTimer = jumpCooldown;
      } else {
        // Check for obstacles
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction * speed * Time.deltaTime, 1.0f, obstacleLayerMask);

        if (hit.collider == null) {
          transform.Translate(direction * speed * Time.deltaTime);
        }

        // Update timer
        if (jumpCooldownTimer > 0.0f) {
          jumpCooldownTimer -= Time.deltaTime;
        }
      }
    } else {
      // Check for obstacles
      RaycastHit2D hit = Physics2D.Raycast(transform.position, direction * jumpingSpeed * Time.deltaTime, 1.0f, obstacleLayerMask);

      if (hit.collider == null) {
        transform.Translate(direction * jumpingSpeed * Time.deltaTime);
      }
    }
  }
}
