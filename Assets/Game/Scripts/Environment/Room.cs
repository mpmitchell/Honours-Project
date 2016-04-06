using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour {

  [SerializeField] bool startingRoom;
  [SerializeField] GameObject enemyContainer;
  [SerializeField] GameObject gateContainer;
  [SerializeField] GameObject rewardContainer;

  public static Room currentRoom = null;

  void Awake() {
    foreach (Transform enemy in enemyContainer.transform) {
      enemy.GetComponent<Enemy>().room = this;
    }

    gateContainer.SetActive(false);
    rewardContainer.SetActive(false);

    if (startingRoom) {
      currentRoom = this;
    } else {
      Exit();
    }
  }

  public void Enter() {
    enemyContainer.SetActive(true);
  }

  public void Exit() {
    enemyContainer.SetActive(false);
    gateContainer.SetActive(false);
  }

  public void LockGates() {
    gateContainer.SetActive(true);
  }

  void Killed() {
    if (enemyContainer.transform.childCount == 1) {
      rewardContainer.SetActive(true);

      foreach (Transform gate in gateContainer.transform) {
        Destroy(gate.gameObject);
      }
    }
  }
}
