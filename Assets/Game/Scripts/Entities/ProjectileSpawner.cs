﻿using UnityEngine;
using System.Collections.Generic;

public class ProjectileSpawner : MonoBehaviour {

  [SerializeField] GameObject prefab;
  [SerializeField] [Range(1, 10)] int maxNumberOfProjectiles;

  Stack<GameObject> pool = new Stack<GameObject>();

  void Awake() {
    for (int i = 0; i < maxNumberOfProjectiles; i++) {
      GameObject projectile = Instantiate(prefab) as GameObject;
      projectile.GetComponent<Projectile>().spawner = this;
      ReturnProjectile(projectile);
      pool.Push(projectile);
    }
  }

  void FireProjectile(Direction direction) {
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
  }
}