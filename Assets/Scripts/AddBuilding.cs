using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuilding : MonoBehaviour
{
    Transform element;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Element")
        {
            element = col.transform;
            Add();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.tag == "Element")
        {
            element = col.transform;
            Add();
        }
    }

    // When the element leves a potential collider, reset patent to the original one
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "Element")
        {
            element.GetComponent<ElementBehavior>().add = false;
            element = null;
        }
    }

    void Add() {
        bool containsBuilding = false;
        for (int i = 1; i < GameObject.Find("Buildings").transform.childCount; i++) {
            Building building = GameObject.Find("Buildings").transform.GetChild(i - 1).GetComponent<Building>();

            if (!building) continue;
            if (building.name == element.GetComponent<Element>().name) {
                containsBuilding = true;
                break;
            }
        }    

        if (!containsBuilding) {
            element.GetComponent<ElementBehavior>().add = true;
        } else {
            audioSource.Play(0);
            element.GetComponent<ElementBehavior>().add = false;
        }
    }
}
