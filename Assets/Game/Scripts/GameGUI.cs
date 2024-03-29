﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameGUI : MonoBehaviour {

  [SerializeField] GameObject healthBar;
  [SerializeField] GameObject heartPrefab;
  [SerializeField] Sprite emptyHeartSprite;
  [SerializeField] Text keyText;
  [SerializeField] Text bombText;
  [SerializeField] Text versionText;

  Stack<Image> health = new Stack<Image>();

  Health playerHealth;

  void Start() {
    versionText.text = SceneManager.GetActiveScene().buildIndex.ToString();

    foreach (Transform child in healthBar.transform) {
      health.Push(child.gameObject.GetComponent<Image>());
    }

    playerHealth = PlayerController.player.GetComponent<Health>();
    int maxHealth = playerHealth.GetHealth();

    for (int i = 1; i < maxHealth; i++) {
      GameObject heart = Instantiate(heartPrefab) as GameObject;
      heart.transform.SetParent(healthBar.transform);
      heart.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
      RectTransform rt = heart.GetComponent<RectTransform>();
      RectTransform rt2 = health.Peek().GetComponent<RectTransform>();
      rt.anchoredPosition = rt2.anchoredPosition + new Vector2(30.0f, 0.0f);
      health.Push(heart.GetComponent<Image>());
    }
  }

  void Damage(int damage) {
    for (int i = 0; i < damage; i++) {
      if (health.Count > 0) {
        Image heart = health.Pop();
        heart.sprite = emptyHeartSprite;
      }
    }
  }

  void CollectKey() {
    keyText.text = (int.Parse(keyText.text) + 1).ToString();
  }

  void UseKey() {
    keyText.text = (int.Parse(keyText.text) - 1).ToString();
  }

  void BombCount(int count) {
    bombText.text = count.ToString();
  }

  void DropBomb() {
    bombText.text = (int.Parse(bombText.text) - 1).ToString();
  }
}
