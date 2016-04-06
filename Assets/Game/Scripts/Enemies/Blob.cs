using UnityEngine;

public class Blob : Entity {

  [SerializeField] [Range(1, 10)] int speed;
  [SerializeField] [Range(1, 10)] int jumpingSpeed;
  [SerializeField] [Range(1, 10)] int jumpDistance;
  [SerializeField] [Range(1, 10)] int jumpCooldown;

  [HideInInspector] public bool jumping = false;
  float jumpCooldownTimer;

  void Start() {
    animator.GetBehaviour<BlobAnimator>().controller = this;
  }

  void Update() {
    directionVector = (PlayerController.player.transform.position - transform.position).normalized;

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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionVector * speed * Time.deltaTime, 1.0f, obstacleLayerMask);

        if (hit.collider == null) {
          transform.Translate(directionVector * speed * Time.deltaTime);
        }

        // Update timer
        if (jumpCooldownTimer > 0.0f) {
          jumpCooldownTimer -= Time.deltaTime;
        }
      }
    } else {
      // Check for obstacles
      RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(directionVector.x, directionVector.y) * jumpingSpeed * Time.deltaTime, 1.0f, obstacleLayerMask);

      if (hit.collider == null) {
        transform.Translate(directionVector * jumpingSpeed * Time.deltaTime);
      }
    }
  }
}
