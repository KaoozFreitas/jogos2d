using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
    public Transform Camera;
    public Transform Player1;

	// Update is called once per frame
	void Update () {
	    if (Camera.position.x-5 < Player1.position.x) {
            Camera.position = (new Vector3((Player1.position.x + 5), 0f, -10f));
        }
	}
}

