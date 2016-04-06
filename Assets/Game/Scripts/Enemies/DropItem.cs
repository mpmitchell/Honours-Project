using UnityEngine;

public class DropItem : MonoBehaviour {

  [SerializeField] GameObject itemPrefab;

  void Dead() {
    Instantiate(itemPrefab, transform.position, Quaternion.identity);
  }
}
