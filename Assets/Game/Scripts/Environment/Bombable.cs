using UnityEngine;

public class Bombable : MonoBehaviour {

  [SerializeField] GameObject rubblePrefab;

  void Explosion() {
    Logger.Log("bombable," + gameObject.name);
    Instantiate(rubblePrefab, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }
}
