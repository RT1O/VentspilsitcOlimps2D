using System.Collections;
using System.Collections.Generic;

using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  public GameController controller;

  private Text text;
  private Text gain;
  private RectTransform rt;
  private RectTransform background;
  private RectTransform fillBackground;

  void Start()
  {
    controller = GameObject.FindWithTag("Controller").GetComponent<GameController>();
    
    rt = (RectTransform)transform;

    text = transform.Find("Text").GetComponent<Text>();
    background = (RectTransform)transform.Find("Background");
    fillBackground = (RectTransform)transform.Find("FillBackground");

    background.sizeDelta =
      new Vector2(rt.rect.width, rt.rect.height);
  }

  void Update()
  {
    float percent = (float)controller.health / controller.maxHealth;

    text.text = Math.Round(percent * 100) + "%";
    fillBackground.sizeDelta =
      new Vector2(rt.rect.width * percent, rt.rect.height);
  }
}
