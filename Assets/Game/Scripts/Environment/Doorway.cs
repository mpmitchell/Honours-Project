
﻿using UnityEngine;

public class Doorway : MonoBehaviour {

  [SerializeField] Room room;

  void OnTriggerEnter2D(Collider2D collider) {
    if (room != Room.currentRoom) {
      // Move Camera & player
      Camera.main.transform.position = new Vector3(room.transform.position.x, room.transform.position.y, Camera.main.transform.position.z);
      PlayerController.player.transform.position = transform.position;

      Room.currentRoom.Exit();
      room.Enter();

      Room.currentRoom = room;
    }
  }

  void OnTriggerExit2D(Collider2D collider) {
    if (room == Room.currentRoom) {
      // Trigger gate spawns
      room.LockGates();
    }

  }
}
