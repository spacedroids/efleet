using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoutGUI : MonoBehaviour
{
    public GameObject target;
    public Text myText;
    public Vector3 offset; //amount to offset from target so that it floats to the side of the target

    private void Awake()
    {
        offset = new Vector3(80, 0, 0); //right side
        myText.text = "";
    }

    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.transform.position);
        transform.position += offset;
    }

    private int baseFontSize = 40;
    public void updateIntensity(float intensity) {
        transform.localScale = Vector3.one * intensity;
        myText.fontSize = intensity > 1 ? (int)(baseFontSize * intensity) : baseFontSize;
        updateText(intensity.ToString());
    }

    public void updateText(string newString)
    {
        myText.text = newString;
    }
}
