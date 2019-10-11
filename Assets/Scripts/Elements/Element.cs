using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour {
    public enum ElementType {
        Energy, Health, Research
    }

    // Identifaction info.
    public string name;
    public ElementType type;

    // Game value modifiers.
    public int energyGain;
    public int healthGain;
    public int researchGain;

    public Sprite buildingSprite;

    public string flavorText;
    public string scienceText;
}
