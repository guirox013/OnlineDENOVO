using UnityEngine;
using System.Collections;

public class TerrainController : MonoBehaviour {
	public static Vector3 terrainSize;
	public static Terrain terrain;
	// Use this for initialization
	void Start () {
		terrain = (Terrain)GetComponent (typeof(Terrain));
		terrainSize = terrain.terrainData.size;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
