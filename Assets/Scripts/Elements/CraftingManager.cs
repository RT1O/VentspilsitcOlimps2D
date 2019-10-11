using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour {
  // All elements
  public GameObject[] elements;
  public GameObject ElementInv;
  public AudioClip[] audioClip;
  public AudioSource audioSource;
  public int elementCount = 3;
  public bool elementExists;
  public int energyCost;
  private GameController gameController;
  
  // The crafting recipes
  private string[,] recipes = new string[,] {
      { "Fire", "Water", "Steam" },
      {"Earth", "Water", "Mud"},
      {"Earth","Fire","Lava"},
      {"Fire","Mud","Rock"},
      {"Lava","Water","Rock"},
      {"Mud","Lava","Rock"},
      {"Water","Mud","Plant"},
      {"Steam","Water","Hot spring"},
      {"Steam","Rock","Hot spring"},
      {"Fire","Rock","Metal"},
      {"Water","Rock","Sand"},
      {"Fire","Plant","Tea"},
      {"Earth","Plant","Wheat"},
      {"Water","Plant","Wood"},
      {"Metal","Wood","Tools"},
      {"Metal","Steam","Geothermal"},
      {"Metal","Hot spring","Geothermal"},
      {"Metal","Lava","Geothermal"},
      {"Fire","Sand","Glass"},
      {"Fire","Wheat","Bread"},
      {"Fire","Wood","Coal"},
      {"Earth","Wood","Oil"},
      {"Tools","Sand","Silicon"},
      {"Bread","Water","Mold"},
      {"Silicon","Tools","Circuit"},
      {"Mold","Glass","Penicilin"}
  };

  // The crafting slots
  private Transform[] slot = new Transform[2];
  
  // The crafting output/result
  private Transform result;

  // Set initial values
  void Start() {
    audioSource = GetComponent<AudioSource>();
    gameController = GameObject.FindWithTag("Controller").GetComponent<GameController>();

    slot[0] = transform.GetChild(0);
    slot[1] = transform.GetChild(1);
    result = transform.GetChild(2);

    ElementInv = GameObject.FindWithTag("ElementController");
  }

  // Update the result
  void Update() {
    energyCost = elementCount*3;

    if (slot[0].childCount == 3 && 
        slot[1].childCount == 3 && 
        result.childCount == 2) {
      
      string element1 = slot[0].GetChild(2).GetComponent<Element>().name;
      string element2 = slot[1].GetChild(2).GetComponent<Element>().name;
      
      for (int i = 0; i < recipes.GetLength(0); i++) {
        GameObject resultElement = GetElement(recipes[i, 2]);

        if (recipes[i, 0] == element1) {
          if (recipes[i, 1] == element2) {
            elementExists = false;
            for(int a = 0; a < ElementInv.transform.childCount; a++)
            {
              if(ElementInv.transform.GetChild(a).GetComponent<Element>().name == resultElement.name) {
                elementExists = true;
              }
            }
            if(elementExists == false) {
              GameObject rez = Instantiate(resultElement, result.position, Quaternion.identity, result);
              rez.transform.localPosition = new Vector3(-50, 0, 0 );
              audioSource.clip = audioClip[Random.Range(0,audioClip.Length-1)];
              audioSource.Play(0);
              elementCount++;
              // gameController.energy -= energyCost;
                         
              ShowFlavorText(rez.GetComponent<Element>());
            }
          }
        } else if (recipes[i, 0] == element2) {
          if (recipes[i, 1] == element1) {
            elementExists = false;
            for(int a = 0; a < ElementInv.transform.childCount; a++)
            {
              if(ElementInv.transform.GetChild(a).GetComponent<Element>().name == resultElement.name) {
                elementExists = true;
              }
            }
            if(elementExists == false) {
              GameObject rez = Instantiate(resultElement, result.position, Quaternion.identity, result);
              rez.transform.localPosition = new Vector3(-50, 0, 0 );
              audioSource.clip = audioClip[Random.Range(0,audioClip.Length-1)];
              audioSource.Play(0);
              elementCount++;
              // gameController.energy -= energyCost;

              ShowFlavorText(rez.GetComponent<Element>());
            }
          }
        }
      }
    }
  }

  void ShowFlavorText(Element element) {
    Text text = GameObject.Find("ElementCount").GetComponent<Text>();
    text.text = elementCount + "/24";

    FlavorBox flavorBox = GameObject.Find("FlavorBox").GetComponent<FlavorBox>();
    flavorBox.SetFlavor(element);
  }

  GameObject GetElement(string name) {
    for (int i = 0; i < elements.GetLength(0); i++) {
      if (elements[i].name == name) {
        return elements[i];
      }
    }
    return null;
  }
}
