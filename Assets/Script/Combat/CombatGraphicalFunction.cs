﻿using UnityEngine;
using System.Collections;

public class CombatGraphicalFunction {

	private GUIStyle currentStyle;

	public void DrawHealthBar(float fractionalHealth, float barHeight, float barWidth, float barLeft, float barTop){
		SetHPContainerStyle();
		GUI.Box(new Rect(barLeft, barTop, barWidth , barHeight), "", currentStyle);

		SetHPFillStyle();
		GUI.Box(new Rect(barLeft, barTop, barWidth * fractionalHealth, barHeight), "", currentStyle);
	}
	
	public void DrawReversedHealthBar(float fractionalHealth, float barHeight, float barWidth, float barRight, float barTop){
		SetHPContainerStyle();
		GUI.Box(new Rect(barRight - barWidth, barTop, barWidth , barHeight), "", currentStyle);
		
		SetHPFillStyle();
		float remmainingHPFraction = barWidth * fractionalHealth;
		GUI.Box(new Rect(barRight - remmainingHPFraction, barTop, remmainingHPFraction, barHeight), "", currentStyle);
	}

	public void ShowWin(){
		SetOverlayStyle();
		GUI.Box(new Rect(Screen.width/2 - 100, Screen.height/2 - 100, 200, 100), "You Win!", currentStyle);
		if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height /2 + 10, 150, 25),"Continue Exploring!")) 
		{
			//Continue
		}
	}
	
	public void ShowLose(){
		SetOverlayStyle();
		GUI.Box(new Rect(Screen.width/2 - 100, Screen.height/2 - 100, 200, 100), "Game Over!", currentStyle);
		if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height /2 + 10, 150, 25),"Return Home...")) 
		{
			//Exit
		}
	}

	private void SetHPContainerStyle()
	{
		currentStyle = new GUIStyle( GUI.skin.box );
		currentStyle.normal.background = MakeTex(  1, 1, new Color( 0f, 0f, 0f, 1f ) );
	}
	
	private void SetHPFillStyle()
	{
		currentStyle = new GUIStyle( GUI.skin.box );
		currentStyle.normal.background = MakeTex( 1, 1, new Color( 255f, 0f, 0f, 1f ) );
	}
	
	private void SetOverlayStyle()
	{
		currentStyle = new GUIStyle( GUI.skin.box );
		currentStyle.normal.background = MakeTex( 1, 1, new Color( 0f, 0f, 0f, 1f ) );
	}

	//Makes a Texture of size width * height, uniform color col.
	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}
}