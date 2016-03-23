using UnityEngine;
using System.Collections.Generic;

public class BombSpawner : MonoBehaviour {

  [SerializeField] GameObject prefab;
  [SerializeField] [Range(1, 10)] int maxNumberOfBombs;

  Stack<GameObject> pool = new Stack<GameObject>();

  void Awake() {
    for (int i = 0; i < maxNumberOfBombs; i++) {
      GameObject bomb = Instantiate(prefab) as GameObject;
      bomb.GetComponent<Bomb>().spawner = this;
      ReturnBomb(bomb);
      pool.Push(bomb);
    }
  }

  void DropBomb() {
    GameObject bomb = GetBomb();

    if (bomb != null) {
      bomb.GetComponent<Bomb>().Activate(transform.position);
    }
  }

  GameObject GetBomb() {
    if (pool.Count != 0) {
      return pool.Pop();
    } else {
      return null;
    }
  }

  public void ReturnBomb(GameObject bomb) {
    bomb.SetActive(false);
    pool.Push(bomb);
  }
}
