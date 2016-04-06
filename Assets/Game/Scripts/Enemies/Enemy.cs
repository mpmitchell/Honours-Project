using UnityEngine;

public class Enemy : MonoBehaviour {

  [HideInInspector] public Room room;

  void Dead() {
    room.SendMessage("Killed");
  }
}
