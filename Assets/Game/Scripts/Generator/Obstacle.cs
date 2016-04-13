using UnityEngine;

[System.Serializable]
public class Obstacle : MonoBehaviour {

  [Range(0, 100)] public int challengeRating;
  public Transform[] itemSpaces;
  public Transform[] stairSpaces;

  [HideInInspector] public int used;
}
