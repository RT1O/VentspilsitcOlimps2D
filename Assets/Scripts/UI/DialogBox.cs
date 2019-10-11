using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    public void Close() {
        RectTransform rt = (RectTransform) GameObject.Find("DialogBox").transform;
        rt.localPosition = new Vector3(1000, 1000, 0);
        
        Time.timeScale = 1;

        //GameObject.Find("MassiveCollider").GetComponent<RectTransform>().localPosition = new Vector3(1000, 1000, 0);
    }
}
