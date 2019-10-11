using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElementBehavior : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Transform originalParent;
    public Transform shouldParent;
    private GameController gameController;
    private AudioSource audioSource;
    private Planet planet;
    public bool add = false;
    public Building rep;
    
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        originalParent = GameObject.FindWithTag("ElementController").transform;
        gameController = GameObject.FindWithTag("Controller").GetComponent<GameController>();
        planet = GameObject.FindWithTag("Planet").GetComponent<Planet>();

        shouldParent = originalParent;
    }

    // When the element is touching a crafting slot, place it in the slot
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "CraftingSlot" && 
            col.transform.childCount == 2) {
            shouldParent = col.transform;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.tag == "CraftingSlot" && 
            col.transform.childCount == 2)
        {
            shouldParent = col.transform;
        }
    }

    // When the element leves a potential collider, reset patent to the original one
    void OnTriggerExit2D(Collider2D col) {
        if (col.transform.tag == "CraftingSlot" && 
            shouldParent == col.transform) {
            shouldParent = originalParent;
        }
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) {
      rep = transform.GetComponent<Building>();
      transform.SetParent(GameObject.FindWithTag("Result").transform);
    }

    // Allow the element to be dragged
    void IDragHandler.OnDrag(PointerEventData eventData) {
        ElementInfo info = GameObject.Find("ElementInfo").GetComponent<ElementInfo>();
        info.HideInfo();
        transform.position = Input.mousePosition;
    }

    // Reset the element to it's parent, if it has one
    void IEndDragHandler.OnEndDrag(PointerEventData eventData) {
        audioSource.Play(0);
        transform.SetParent(shouldParent);
        transform.position = shouldParent.position;
        transform.localPosition = new Vector3(50, 0, 0 );

        if (add && gameController.energy >= (planet.buildingCount + 1) * 10) {
            gameController.energy -= (planet.buildingCount + 1) * 10;
            Element el = gameObject.GetComponent<Element>();
            GameObject.FindWithTag("Planet").GetComponent<Planet>().CreateBuilding(el.name, el.energyGain, el.healthGain, el.researchGain, el.buildingSprite);
        }

        if(rep != null) {
            bool containsBuilding = false;
            Element draggedElement = transform.GetComponent<Element>();

            for (int i = 1; i < GameObject.Find("Buildings").transform.childCount; i++) {
                Building building = GameObject.Find("Buildings").transform.GetChild(i - 1).GetComponent<Building>();

                if (!building) continue;
                if (building.name == draggedElement.name) {
                    containsBuilding = true;
                    break;
                }
            }    

            if (containsBuilding) {
                GameObject.Find("Buildings").transform.GetChild(0).GetComponent<Building>().Play();
                return;
            }
            
            Element el = gameObject.GetComponent<Element>();
            GameObject.FindWithTag("Planet").GetComponent<Planet>().ReplaceBuilding(el.name, el.energyGain, el.healthGain, el.researchGain, el.buildingSprite, rep);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
     {
        ElementInfo info = GameObject.Find("ElementInfo").GetComponent<ElementInfo>();
        info.ShowInfo(gameObject.GetComponent<Element>(), transform);
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
        ElementInfo info = GameObject.Find("ElementInfo").GetComponent<ElementInfo>();
        info.HideInfo();
     }
}
