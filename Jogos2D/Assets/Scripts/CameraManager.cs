using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
    public Transform Camera;
    public Transform Player1;
    public Transform Player2;

    // Update is called once per frame
    void Update () {
        float mostAdvancedPlayerX = Mathf.Max(Player1.position.x, Player2.position.x);
        if (Camera.position.x < mostAdvancedPlayerX) {
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, (new Vector3(mostAdvancedPlayerX, 0f, -10f)), 1.5f * Time.deltaTime);
        }

        if (Camera.position.x - Player1.position.x > 9.5 ) {
            DestroyImmediate(Player1.gameObject);
            Player1 = Camera;
        }
    }
}

