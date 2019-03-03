using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthGUI : MonoBehaviour
{
    public GameObject target;
    public Text hullHealthTxt;
    public Text shieldHealthTxt;
    public Vector3 offset; //amount to offset from target so that it floats to the side of the target

    private void Awake()
    {
        offset = new Vector3(-40, 0, 0);
        shieldHealthTxt.text = "";
        hullHealthTxt.text = "";
    }

    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.transform.position);
        transform.position += offset;
    }

    public void updateText(string shieldHealth, string hullHealth) {
        hullHealthTxt.text = hullHealth;
        shieldHealthTxt.text = shieldHealth == "0" ? "" : shieldHealth;
    }

    public void updateHullHealth(string newText) {
        hullHealthTxt.text = newText;
    }
}
