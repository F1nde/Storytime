using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class POI : MonoBehaviour, IGvrGazeResponder {

	private POIController controller;
    public GameObject TextCanvas;

    public string text;

	[SerializeField]
	private bool isGazed = false;
	[Range(0, 3)]
	public float gazeThreshold = 0f;

	private float gazeStartTime = 0f;
    private bool isActive;

    AudioSource audioSource;

	void Start ()
	{
		controller = transform.parent.GetComponent<POIController>();
        audioSource = GetComponent<AudioSource>();
		SetGazedAt(false);
        isActive = true;
    }

	void LateUpdate ()
	{
		GvrViewer.Instance.UpdateState();
		if (GvrViewer.Instance.BackButtonPressed) {
			Application.Quit();
		}

		if (isGazed && isActive) {
			if ((Time.time - gazeStartTime) >= gazeThreshold) {
				Debug.Log ("POI collected!");

                // Disappear
                GetComponent<SphereCollider>().enabled = false;
                GetComponent<MeshRenderer>().enabled = false;

                // Send text to TextPanel
                TextCanvas.GetComponent<TextPanel>().Activate(text);

                // Inform POIController
                controller.POIFound();

                isActive = false;

                audioSource.Play();
            }
		}
	}

    public void ResetState()
    {
        isActive = true;
        isGazed = false;
        SetGazedAt(false);
        GetComponent<Renderer>().material.color = Color.red;
        gazeStartTime = 0f;

        // Reset textPanel
        TextCanvas.GetComponent<TextPanel>().DeActivate();

        // Reset deactivated components
        GetComponent<SphereCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }

	public void SetGazedAt(bool gazedAt) 
	{
		GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;

		// If the POI was not gazed before, start the gaze timer
		if (!isGazed && gazedAt) {
			gazeStartTime = Time.time;
		}
		isGazed = gazedAt;
	}

	#region IGvrGazeResponder implementation
	/// Called when the user is looking on a GameObject with this script,
	/// as long as it is set to an appropriate layer (see GvrGaze).
	public void OnGazeEnter() {
		SetGazedAt(true);
	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeExit() {
		SetGazedAt(false);
	}

	/// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
	public void OnGazeTrigger() {
		// Do something
	}
	#endregion
}
