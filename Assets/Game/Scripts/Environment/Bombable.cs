using UnityEngine;

public class Bombable : MonoBehaviour {

  [SerializeField] GameObject rubblePrefab;

  void Explosion() {
    Instantiate(rubblePrefab, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }
}
