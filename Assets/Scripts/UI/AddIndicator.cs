using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddIndicator : MonoBehaviour
{
    public GameController controller;

    // Changes the color of the add button depending if you have enough energy
    void Update() {
        Planet planet = GameObject.FindWithTag("Planet").GetComponent<Planet>();

        if (controller.energy < (planet.buildingCount + 1) * 10) {
            transform.GetComponent<Image>().color = Color.red;
        } else {
            transform.GetComponent<Image>().color = Color.green;
        }
    }
}
