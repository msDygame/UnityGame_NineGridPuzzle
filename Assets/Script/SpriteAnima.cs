﻿using UnityEngine;
using System.Collections;

public class SpriteAnima : MonoBehaviour
{
	public Sprite[] sprites;
	public float framesPerSecond;
	protected SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () 
	{
		spriteRenderer = renderer as SpriteRenderer;
	}
	
	// Update is called once per frame
	void Update ()
	{
		int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
		index = index % sprites.Length;
		spriteRenderer.sprite = sprites[ index ];
	}
}
