﻿using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour {

  [SerializeField] bool startingRoom;
  [SerializeField] bool goalRoom;
  [SerializeField] SpriteRenderer mapUI;
  [SerializeField] GameObject enemyContainer;
  [SerializeField] GameObject gateContainer;
  [SerializeField] GameObject rewardContainer;

  public static Room currentRoom = null;

  static LinkedList<Room> rooms = new LinkedList<Room>();
  static Color defaultColour = Color.black;
  static bool highlightGoal = false;

  void Awake() {
    foreach (Transform enemy in enemyContainer.transform) {
      enemy.GetComponent<Enemy>().room = this;
    }

    rewardContainer.SetActive(false);

    if (startingRoom) {
      currentRoom = this;
      mapUI.color = Color.green;
    } else {
      Exit();
    }

    rooms.AddLast(this);
  }

  public void Enter() {
    enemyContainer.SetActive(true);
    mapUI.color = Color.green;
  }

  public void Exit() {
    enemyContainer.SetActive(false);
    gateContainer.SetActive(false);

    if (goalRoom && highlightGoal) {
      mapUI.color = Color.red;
    } else {
      mapUI.color = defaultColour;
    }
  }

  public void LockGates() {
    gateContainer.SetActive(true);
  }

  void Killed() {
    if (enemyContainer.transform.childCount == 1) {
      rewardContainer.SetActive(true);

      gateContainer.BroadcastMessage("RoomClear", SendMessageOptions.DontRequireReceiver);
    }
  }

  public static void GotMap() {
    defaultColour = Color.white;

    foreach (Room room in rooms) {
      if (!room.goalRoom) {
        room.mapUI.color = defaultColour;
      }
    }

    currentRoom.mapUI.color = Color.green;
  }

  public static void GotCompass() {
    foreach (Room room in rooms) {
      if (room.goalRoom) {
        room.mapUI.color = Color.red;
        break;
      }
    }
  }
}
