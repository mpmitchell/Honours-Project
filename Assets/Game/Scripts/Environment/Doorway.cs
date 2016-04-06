
﻿using UnityEngine;

public class Doorway : MonoBehaviour {

  [SerializeField] Room room;

  static GameObject previousDoorway = null;

  void OnTriggerEnter2D(Collider2D collider) {
    if (room != Room.currentRoom) {
      // Move Camera & player
      Camera.main.transform.position = new Vector3(room.transform.position.x, room.transform.position.y, Camera.main.transform.position.z);
      PlayerController.player.transform.position = transform.position;

      // Block previousDoorway
      previousDoorway.layer = LayerMask.NameToLayer("Obstacle");

      Room.currentRoom.Exit();
      room.Enter();
      Room.currentRoom = room;
    } else {
      previousDoorway = gameObject;
    }
  }

  void OnTriggerExit2D(Collider2D collider) {
    if (room == Room.currentRoom && previousDoorway) {
      previousDoorway.layer = LayerMask.NameToLayer("Doorway");
      previousDoorway = null;

      // Trigger gate spawns
      room.LockGates();
    }

  }
}
