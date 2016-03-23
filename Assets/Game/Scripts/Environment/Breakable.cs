using UnityEngine;

public class Breakable : MonoBehaviour {

  [SerializeField] GameObject brokenPrefab;

  void Break() {
    Instantiate(brokenPrefab, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }
}
