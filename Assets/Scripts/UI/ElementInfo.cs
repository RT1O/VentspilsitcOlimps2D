using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ElementInfo : MonoBehaviour, IPointerEnterHandler
{
    private RectTransform rt;
    private Vector3 initialPos;

    void Start() {
        rt = (RectTransform) transform;
        initialPos = transform.position;
    }

    public void ShowInfo(Element element, Transform parent) {
        transform.SetParent(parent);
        
        Text name = transform.GetChild(0).GetComponent<Text>();
        Text health = transform.GetChild(1).GetComponent<Text>();
        Text energy = transform.GetChild(2).GetComponent<Text>();
        Text research = transform.GetChild(3).GetComponent<Text>();

        name.text = element.name;

        health.text = element.healthGain + " Health";
        energy.text = element.energyGain + " Energy";
        research.text = element.researchGain + " Research";

        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, rt.rect.width);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, rt.rect.height);
        
        rt.localPosition -= new Vector3(((RectTransform) parent).rect.width, -rt.rect.height / 2, 0);
    }

    public void HideInfo() {
        transform.SetParent(null);
        rt.localPosition = initialPos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HideInfo();
    }
}
