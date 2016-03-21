using UnityEngine;
using System.Collections.Generic;

public class Bow : MonoBehaviour {

  [SerializeField] GameObject arrowPrefab;
  [SerializeField] int numberOfArrows;

  Stack<GameObject> arrowPool = new Stack<GameObject>();

  void Awake() {
    for (int i = 0; i < numberOfArrows; i++) {
      GameObject arrow = Instantiate(arrowPrefab) as GameObject;
      arrow.GetComponent<Arrow>().bow = this;
      ReturnArrow(arrow);
      arrowPool.Push(arrow);
    }
  }

  public void Fire(Direction direction) {
    GameObject arrow = GetArrow();

    if (arrow != null) {
      arrow.GetComponent<Arrow>().Activate(transform.position, direction);
    }
  }

  public GameObject GetArrow() {
    if (arrowPool.Count != 0) {
      return arrowPool.Pop();
    } else {
      return null;
    }
  }

  public void ReturnArrow(GameObject arrow) {
    arrow.SetActive(false);
    arrowPool.Push(arrow);
  }
}
