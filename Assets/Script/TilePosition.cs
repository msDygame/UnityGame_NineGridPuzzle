using UnityEngine;
using System.Collections;

public class TilePosition : MonoBehaviour 
{
	public int miTilePosition ;
	public int iTilePosition
	{
		set{miTilePosition=value;}
		get{return miTilePosition;}
	}
	//
	void Awake()
	{
		miTilePosition = 0 ;
	}
	// Use this for initialization
	void Start () 
	{

	}	
	// Update is called once per frame
	void Update () 
	{	
	}
	//
	void OnGUI()
	{
	}
}
