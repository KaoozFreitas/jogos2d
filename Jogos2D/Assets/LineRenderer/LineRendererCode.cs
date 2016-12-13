using UnityEngine;
using System.Collections;

public class LineRendererCode : MonoBehaviour {

	//Line Renderer
	public LineRenderer myLineRenderer;
	public Transform point1;
	public Transform point2;

	//Material
	public Material myMaterial;
	public Texture myTexture;
	public float xScaleFactor;
	private float adjustedXSize;

	// Update is called once per frame
	void Update () {
		myLineRenderer.SetPosition(0,point1.position);
		myLineRenderer.SetPosition(1,point2.position);

        myMaterial.SetTexture(0, myTexture);
        adjustedXSize = xScaleFactor * (Mathf.Abs(Vector2.Distance(point1.position,point2.position)));

		myMaterial.mainTextureScale = new Vector2(adjustedXSize, myMaterial.mainTextureScale.y);
	}
}
