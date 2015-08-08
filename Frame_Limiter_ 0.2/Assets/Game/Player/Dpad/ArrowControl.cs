﻿using UnityEngine;
using System.Collections;


public class ArrowControl : MonoBehaviour {

	public GameObject leftBtn, rightBtn, upBtn, downBtn;


	public Vector2 direction;
	
	private static bool moveLeft, moveRight, moveUp, moveDown = false;



	void Update () 
	{

		
		moveUp = moveDown = moveLeft = moveRight = false;

		if(leftBtn.GetComponentInChildren<VCButtonNgui>().btnPressed)
		{
			direction.y = -1f;
			moveLeft = true;
		}	
		if(rightBtn.GetComponentInChildren<VCButtonNgui>().btnPressed)
		{
			direction.y = 1f;
			moveRight = true;
		}
		if(upBtn.GetComponentInChildren<VCButtonNgui>().btnPressed)
		{
			direction.x = 1f;
			moveUp = true;
		}
		if(downBtn.GetComponentInChildren<VCButtonNgui>().btnPressed)
		{
			direction.x = -1f;
			moveDown = true;
		}
		if((moveDown && moveUp && moveRight && moveLeft) || Input.touchCount==0)
			direction = new Vector2(0f, 0f);

	   
	}
	
} 