using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthGUI : MonoBehaviour
{
    public GameObject target;
    private Text textComponent;
    public Vector3 offset; //amount to offset from target so that it floats to the side of the target

    private void Awake()
    {
        textComponent = gameObject.GetComponent<Text>();
        offset = new Vector3(-20, 0, 0);
    }

    void Start()
    {
    }

    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.transform.position);
        transform.position += offset;
    }

    public void updateText(string newText) {
        textComponent.text = newText;
    }
}
