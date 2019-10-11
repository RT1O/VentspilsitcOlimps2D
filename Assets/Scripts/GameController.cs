using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float health = 100;
    public int maxHealth = 100;

    public float diff;

    public float energy = 100;
    public int maxEnergy = 100;

    public float research = 100;
    public int maxResearch = 100;
    
    public float repeatTime;
    private CraftingManager craftingManager;

    public float healthDrop;
    public float energyDrop;
    public float ReserchDrop;

    private void Start()
    {
        craftingManager = GameObject.FindWithTag("CraftingWindow").GetComponent<CraftingManager>();
        InvokeRepeating("drop", 0f, repeatTime);

        Time.timeScale = 0;
    }

    public void drop()
    {
        Text healthGain = GameObject.Find("HealthGain").GetComponent<Text>();
        Text energyGain = GameObject.Find("EnergyGain").GetComponent<Text>();
        // Text researchGain = GameObject.Find("ResearchGain").GetComponent<Text>();

        health += (healthDrop / 3 - craftingManager.elementCount*diff) / 8f;

        if (research >= 100) {
            Time.timeScale = 0;

            RectTransform box = GameObject.Find("WinDialog").GetComponent<RectTransform>();
            
            box.SetParent(GameObject.Find("UI").transform);
            box.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, box.rect.width);
            box.localPosition = new Vector3(-231.75f, -18.898f, 0);
        } else if (health < 0)
        {
            Time.timeScale = 0;

            RectTransform box = GameObject.Find("LossDialog").GetComponent<RectTransform>();
            box.SetParent(GameObject.Find("UI").transform);
            box.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, box.rect.width);
            box.localPosition = new Vector3(-231.75f, -18.898f, 0);
        }else if(health > 100)
        {
            health = 100f;
        }

        energy += energyDrop/2f;

        if(energy > maxEnergy)
        {
            energy = maxEnergy;
        }else if(energy < 0)
        {
            energy = 0;
        }

        research += (craftingManager.elementCount-3)/20f + ReserchDrop/25f;

        if(research > 100)
        {
            research = 100;
        }

        float _hpGain = (float) System.Math.Round(((healthDrop / 3 - craftingManager.elementCount*diff) / 8f), 1) ;
     
        healthGain.text = (_hpGain > 0 ? "+" : "") + _hpGain + "%";
        energyGain.text = (energyDrop > 0 ? "+" : "") + System.Math.Round(energyDrop/2f, 1);

        ReserchDrop = 0; energyDrop = 0.5f; healthDrop = 1;
    }
}
