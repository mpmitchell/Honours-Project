using UnityEngine;

public class Bomb : MonoBehaviour {

  [SerializeField] [Range(1, 10)] float fuseTime;
  [SerializeField] [Range(1,10)] int minSpeed;
  [SerializeField] [Range(1, 10)] int maxSpeed;
  [SerializeField] GameObject explosionPrefab;

  [HideInInspector] public BombSpawner spawner;

  Animator animator;
  float timer;

  void Awake() {
    animator = GetComponent<Animator>();
    timer = fuseTime;
  }

  void Update() {
    if (timer <= 0.0f) {
      spawner.ReturnBomb(gameObject);
      Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    } else {
      timer -= Time.deltaTime;

      animator.speed = Mathf.Lerp(minSpeed, maxSpeed, (fuseTime - timer) / fuseTime);
    }
  }

  public void Activate(Vector3 origin) {
    gameObject.SetActive(true);
    transform.position = origin;
    timer = fuseTime;
  }
}
