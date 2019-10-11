using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlavorBox : MonoBehaviour {

    public float fadeOutTime = 4.0f;
    public float fadeOutDuration = 1.5f;
    
    private bool hidden = false;
    private float fadeCounter = 0.0f;

    private Text title;
    private Text scienceText;
    private Text flavorText;
    private Image background;

    public void Start() {
        // Set initial variables
        title = transform.GetChild(0).GetComponent<Text>();
        scienceText = transform.GetChild(1).GetComponent<Text>();
        flavorText = transform.GetChild(2).GetComponent<Text>();
        background = transform.GetComponent<Image>();

        // Hide the box
        ChangeAlpha(0.0f);    
    }

    public void SetFlavor(Element element) {
        // Change some values
        hidden = false;
        fadeCounter = 0.0f;

        // Update the text
        title.text = "You created " + element.name + "!";
        scienceText.text = element.scienceText;
        flavorText.text = element.flavorText;

        ChangeAlpha(1.0f);    
        CancelInvoke();
        InvokeRepeating("FadeOut", fadeOutTime, 0.1f);
    }


    public void FadeOut() {
        if (fadeCounter >= fadeOutDuration) {
            hidden = true;
            CancelInvoke();
        } else {
            fadeCounter += 0.1f;
            ChangeAlpha(0.9f - (fadeCounter / fadeOutDuration));
        }
    }

    public void ChangeAlpha(float alpha) {
        title.color = GetColor(title, alpha);
        flavorText.color = GetColor(flavorText, alpha); 
        background.color = GetColor(background, alpha);
        scienceText.color = GetColor(scienceText, alpha);
    }
    
    public Color GetColor(Image image, float alpha) {
        Color color = image.color;
        color.a = alpha;

        return color;
    }

    public Color GetColor(Text text, float alpha) {
        Color color = text.color;
        color.a = alpha;

        return color;
    }
}
