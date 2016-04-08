using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour {

  [SerializeField] bool startingRoom;
  [SerializeField] SpriteRenderer mapUI;
  [SerializeField] GameObject enemyContainer;
  [SerializeField] GameObject gateContainer;
  [SerializeField] GameObject rewardContainer;

  public static Room currentRoom = null;

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
  }

  public void Enter() {
    enemyContainer.SetActive(true);
    mapUI.color = Color.green;
  }

  public void Exit() {
    enemyContainer.SetActive(false);
    gateContainer.SetActive(false);
    mapUI.color = Color.white;
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
}
