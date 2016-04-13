using UnityEngine;

public class Enemy : MonoBehaviour {

  [Range(0, 100)] public int challengeRating;

  [HideInInspector] public Room room;

  void Dead() {
    room.SendMessage("Killed");
  }
}
