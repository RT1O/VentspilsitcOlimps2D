using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
  public GameController controller;

  private Text text;
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

    background.sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
  }

  void Update()
  {
    float percent = (float)controller.energy / controller.maxEnergy;

    text.text = (int)controller.energy + "/" + controller.maxEnergy;
    fillBackground.sizeDelta =
      new Vector2(rt.rect.width * percent, rt.rect.height);
  }
}

