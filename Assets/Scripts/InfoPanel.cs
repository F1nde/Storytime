using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Text))]
public class InfoPanel : MonoBehaviour
{
    public Camera cam;

    private GameObject POIController;
    private Text textField;

    void Awake()
    {
        textField = GetComponent<Text>();
    }

    void Start()
    {
        // Tie panel to main camera
        if (cam == null)
        {
            cam = Camera.main;
            transform.SetParent(cam.GetComponent<Transform>(), true);
        }

        // Find POIController for status information
        POIController = GameObject.Find("POIController");
    }

    void LateUpdate()
    {
        List<int> status = POIController.GetComponent<POIController>().POIStatus();

        if(status.Count == 2)
            textField.text = "POIs found " + status[0] + " / " + status[1];
    }
}
