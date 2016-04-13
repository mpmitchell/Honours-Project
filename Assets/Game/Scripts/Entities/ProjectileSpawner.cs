using UnityEngine;
using System.Collections.Generic;

public class ProjectileSpawner : MonoBehaviour {

  [SerializeField] GameObject prefab;
  [SerializeField] [Range(1, 10)] int maxNumberOfProjectiles;

  Stack<GameObject> pool = new Stack<GameObject>();
  LinkedList<GameObject> projectiles = new LinkedList<GameObject>();

  void Start() {
    for (int i = 0; i < maxNumberOfProjectiles; i++) {
      GameObject projectile = Instantiate(prefab) as GameObject;
      projectile.GetComponent<Projectile>().spawner = this;
      projectile.SetActive(false);
      pool.Push(projectile);
      projectiles.AddLast(projectile);
    }
  }

  void FireProjectile(Direction direction) {
    GameObject projectile = GetProjectile();

    if (projectile != null) {
      projectile.GetComponent<Projectile>().Activate(transform.position, direction);
    }
  }

  void FireProjectile(Vector3 direction) {
    GameObject projectile = GetProjectile();

    if (projectile != null) {
      projectile.GetComponent<Projectile>().Activate(transform.position, direction);
    }
  }

  GameObject GetProjectile() {
    if (pool.Count != 0) {
      return pool.Pop();
    } else {
      return null;
    }
  }

  public void ReturnProjectile(GameObject arrow) {
    arrow.SetActive(false);
    pool.Push(arrow);
    SendMessage("ProjectileReturned", SendMessageOptions.DontRequireReceiver);
  }

  void Dead() {
    foreach (GameObject projectile in projectiles) {
      Destroy(projectile);
    }
  }
}
