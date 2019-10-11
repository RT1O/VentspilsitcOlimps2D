using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    // Amount of nodes to be crated
    public int nodes = 12;
    
    // Angle between nodes
    public float angle = 0;
    
    // Radius of the circle
    public float radius = 3;
    public int buildingCount = 0;
    
    // Node preset
    public GameObject node;
    public GameObject building;
    
    // Centre of circle
    public Vector2 centre;
    
    void Start()
    {
        // Get information
        centre = gameObject.transform.position;
        angle = 360 / nodes;

        for (int i = 0; i < nodes; i++)
        {
            // Create node
            GameObject newNode = Instantiate(node, new Vector2(transform.position.x,Screen.height / radius + transform.position.y), Quaternion.identity, gameObject.transform);

            // Rotate and position the node relative to the planet
            newNode.transform.RotateAround(centre, Vector3.forward, -angle * i);
        }

        UpdatePlanetBuildings();
    }

    private void FixedUpdate()
    {
        transform.RotateAround(centre, Vector3.forward, -angle/1000f);
    }
    
    public void CreateBuilding(string name, int energyGain, int healthGain, int researchGain, Sprite buildingSprite)
    {
        Transform buildings = transform.GetChild(0);
        if(transform.GetChild(0).childCount < nodes)
        {
            GameObject newBuild = Instantiate(building, new Vector2(transform.position.x,radius + transform.position.y), Quaternion.identity, gameObject.transform.GetChild(0));
            Building buildScript = newBuild.GetComponent<Building>();
            buildScript.name = name;
            buildScript.energyGain = energyGain;
            buildScript.healthGain = healthGain;
            buildScript.researchGain = researchGain;
            buildScript.buildingSprite = buildingSprite;
            buildingCount++;
            buildings.GetChild(buildings.childCount-2).SetSiblingIndex(20);
        }else if(transform.GetChild(0).childCount == nodes)
        {
            GameObject newBuild = Instantiate(building, new Vector2(transform.position.x,radius + transform.position.y), Quaternion.identity, gameObject.transform.GetChild(0));
            Building buildScript = newBuild.GetComponent<Building>();
            buildScript.name = name;
            buildScript.energyGain = energyGain;
            buildScript.healthGain = healthGain;
            buildScript.researchGain = researchGain;
            buildScript.buildingSprite = buildingSprite;
            buildingCount++;
            buildings.GetChild(buildings.childCount-2).SetParent(null);
            buildings.GetChild(buildings.childCount-2).position = new Vector3(1000, 1000, 0);
        }
        UpdatePlanetBuildings(); 
    }

    public void ReplaceBuilding(string name, int energyGain, int healthGain, int researchGain, Sprite buildingSprite, Building buildScript)
    {
        buildScript.name = name;
        buildScript.energyGain = energyGain;
        buildScript.healthGain = healthGain;
        buildScript.researchGain = researchGain;            
        buildScript.buildingSprite = buildingSprite;
        buildScript.GetComponent<Image>().sprite = buildingSprite;
    }

    // Used to rotate buildings around the planet.
    public void UpdatePlanetBuildings() {
        Transform buildings = transform.GetChild(0);
        Transform parentNode;

        for (int i = 0; i < buildings.childCount; i++) {
            parentNode = transform.GetChild(i+2);
            buildings.GetChild(i).rotation = parentNode.rotation;
            buildings.GetChild(i).position = parentNode.position;
        }
    }
}
