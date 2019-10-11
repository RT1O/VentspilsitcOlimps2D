using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public string name;

    // Game value modifiers.
    public int energyGain;
    public int healthGain;
    public int researchGain;
    public Sprite buildingSprite;
    public Transform element;
    private GameController gameController;
    private Planet planet;
    int repeatTime = 1;

    void Start()
    {
        gameController = GameObject.FindWithTag("Controller").GetComponent<GameController>();

        GetComponent<Image>().sprite = buildingSprite;

        InvokeRepeating("Generate", 0f, repeatTime);
    }

    public void Generate()
    {
        gameController.energyDrop += energyGain;
        gameController.healthDrop += healthGain;
        gameController.ReserchDrop += researchGain;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Element")
        {
            element = col.transform;
            element.GetComponent<ElementBehavior>().rep = this;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.tag == "Element")
        {
            element.GetComponent<ElementBehavior>().rep = this;
        }
    }

    // When the element leves a potential collider, reset patent to the original one
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "Element")
        {
            col.transform.GetComponent<ElementBehavior>().rep = null;
            element = null;
        }
    }

    /*
     //used to reduce flikering, when the building is moved between nodes
    public Collider2D touching;

    //When building touches a node, attach and rotate the building to align to the node
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Node")
        {
            touching = col.collider;
            transform.parent = col.transform;
            transform.rotation = col.transform.rotation;
        }
    }
    //When the building is no longer touching the node, reset it's position and rotation
    private void OnCollisionExit2D(Collision2D col)
    {
        if(touching == col.collider)
        {
            transform.parent = null;
            transform.rotation = Quaternion.identity;
        }    
    }
    //Allow th building to be draged
    private void OnMouseDrag()
    {
        transform.position =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
    //Reset the building to it's parent, if it has one
    private void OnMouseUp() {
        if(transform.parent != null)
        {
            transform.position = transform.parent.position;
        }
    }
    
     */
}
