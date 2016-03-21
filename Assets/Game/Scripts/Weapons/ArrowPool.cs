using UnityEngine;
using System.Collections.Generic;

public class ArrowPool : MonoBehaviour {
  [SerializeField] GameObject arrowPrefab;
  [SerializeField] int numberOfArrows;

  Stack<GameObject> arrowPool = new Stack<GameObject>();

  void Awake() {
    for (int i = 0; i < numberOfArrows; i++) {
      GameObject arrow = Instantiate(arrowPrefab) as GameObject;
      arrow.GetComponent<Arrow>().pool = this;
      ReturnArrow(arrow);
      arrowPool.Push(arrow);
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
