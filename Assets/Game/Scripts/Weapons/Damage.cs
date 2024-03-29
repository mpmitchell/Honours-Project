﻿using UnityEngine;

public class Damage : MonoBehaviour {

  [SerializeField] [Range(1, 10)] int damage;
  [SerializeField] bool canBreak;
  [SerializeField] LayerMask targetLayers;

  void OnTriggerEnter2D(Collider2D collider) {
    if (((1 << collider.gameObject.layer) & targetLayers) != 0) {
      Logger.Log("damage," + gameObject.name + "," + collider.gameObject.name);
      
      collider.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
    } else if (canBreak && collider.tag == "Breakable") {
      collider.SendMessage("Break");
    }
  }
}
