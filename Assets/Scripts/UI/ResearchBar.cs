using System.Collections;
using System.Collections.Generic;

using System;
using UnityEngine;
using UnityEngine.UI;

public class ResearchBar : MonoBehaviour
{
  public GameController controller;

  private Text text;
  private RectTransform rt;
  private RectTransform background;
  private RectTransform fillBackground;
  private CraftingManager craftingManager;

  void Start()
  {
    craftingManager = GameObject.FindWithTag("CraftingWindow").GetComponent<CraftingManager>();

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
    float percent = (float)controller.research / controller.maxResearch;

    text.text = Math.Round(percent * 100) + "%";
    fillBackground.sizeDelta =
      new Vector2(rt.rect.width * percent, rt.rect.height);
  }
}
