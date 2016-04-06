using UnityEngine;

public class Dragon : MovingEntity {

  [SerializeField] [Range(1, 10)] int speed;
  [SerializeField] [Range(1, 10)] int changeChance;
  [SerializeField] [Range(1, 100)] int fireChance;

  [HideInInspector] public bool attacking = false;

  float distance;

  void Start() {
    animator.GetBehaviour<DragonAnimator>().dragon = this;
    direction = Random.value <= 0.5f ? Direction.Down : Direction.Up;
  }

  void Update() {
    if (!attacking) {
      Vector3 translation = GetDirectionVector() * speed * Time.deltaTime;
      CollisionCheck(ref translation.x, ref translation.y);
      distance += translation.magnitude;

      if (translation == Vector3.zero) {
        direction = Random.value <= 0.5f ? Direction.Down : Direction.Up;
      } else if (distance >= 1.0f) {
        distance = 0.0f;

        if (Random.value <= 1.0f / changeChance) {
          direction = Random.value <= 0.5f ? Direction.Down : Direction.Up;
        }
      } else {
        transform.Translate(translation);
      }

      if (Random.value <= 1.0f / fireChance) {
        animator.SetTrigger("Attack");
        SendMessage("FireProjectile", new Vector3(-1.0f, 0.0f, 0.0f));
        SendMessage("FireProjectile", new Vector3(-1.0f, -0.5f, 0.0f));
        SendMessage("FireProjectile", new Vector3(-1.0f, 0.5f, 0.0f));
        attacking = true;
      }
    }
  }
}
