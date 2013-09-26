using UnityEngine;
using System.Collections;

public class Tiler : MonoBehaviour {

public float ScaleToTiles;
public Texture texture;
Vector3 Scale;
	
	
//I AM SO GOOD.	
	void Start () {
		float Length = GetComponent<MeshFilter>().mesh.bounds.max.x - GetComponent<MeshFilter>().mesh.bounds.min.x;
		float Height = GetComponent<MeshFilter>().mesh.bounds.max.y - GetComponent<MeshFilter>().mesh.bounds.min.y;
		
		float ScaleToTilesx = ScaleToTiles/Length;
		float ScaleToTilesy = ScaleToTiles/Height;  
		transform.gameObject.renderer.material.mainTexture = texture;
//		renderer.material.shader = Shader.Find("Diffuse");
		renderer.material.mainTextureScale = new Vector2(transform.lossyScale.x*ScaleToTilesx,transform.lossyScale.y*ScaleToTilesy);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
