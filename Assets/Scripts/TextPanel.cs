using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Text))]
public class TextPanel : MonoBehaviour
{
    private Text textField;

    void Awake()
    {
        textField = GetComponent<Text>();
    }

    void Start()
    {
        textField.enabled = false;
    }

    public void Activate(string text)
    {
        textField.enabled = true;
        textField.text = text;
    }

    public void DeActivate()
    {
        textField.enabled = false;
        textField.text = "";
    }
}
