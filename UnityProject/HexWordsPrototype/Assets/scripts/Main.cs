﻿using UnityEngine;
using System.Collections;
using System;

public class Main : MonoBehaviour 
{
	#region Constants
	private const float FINGER_SIZE = 0.5f;
	private const int ROWS = 9;
	private const int COLS = 12;
	#endregion

	#region Private Fields
	private HexGridLogic _hexGridLogic;
	private HexGridView _hexGrid;
	private Camera _thisCamera;
	private Vector3 _lastAverageTouchWorld;
	private int _lastTouchCount;

	#endregion

	private void InitializeGrid()
	{
		float dpi = Screen.dpi;
		if(dpi != 0)
		{
			Debug.Log("screen DPI found = " + dpi);
		}
		else
		{
			dpi = 150.0f;
			Debug.LogWarning("screen DPI not found, defaulting to " + dpi);
		}
		float viewportWidth = FINGER_SIZE * dpi / (float)Screen.width;
		float viewportHeight = FINGER_SIZE * dpi / (float)Screen.height;
		_hexGrid.InitializeGrid(_thisCamera, viewportWidth, viewportHeight, ROWS, COLS);
		_hexGridLogic.InitializeGrid(_hexGrid, ROWS, COLS);
	}

	#region Unity Methods Implementations
	private void Awake()
	{
		_thisCamera = GetComponent<Camera>();
		_hexGrid = GetComponentInChildren<HexGridView>();
		_hexGridLogic = GetComponentInChildren<HexGridLogic>();
	}

	// Use this for initialization
	void Start () 
	{
		GetComponent<Camera>().orthographicSize = Screen.height * 0.5f;
		GetComponentInChildren<ScreenAdaptingQuad>().UpdateScaleToScreen(this.GetComponent<Camera>());
		InitializeGrid();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 averageTouch = Vector2.zero;
		if(Input.touchCount > 1)
		{
			if(Input.touchCount != _lastTouchCount )
			{
				for(int i=0; i < Input.touches.Length; i++)
				{
					averageTouch += Input.touches[i].position;
				}
				averageTouch /= (float)Input.touches.Length;
				_lastAverageTouchWorld = _thisCamera.ScreenToWorldPoint(averageTouch);
			}
			else
			{
				for(int i=0; i < Input.touches.Length; i++)
				{
					averageTouch += Input.touches[i].position;
				}
				averageTouch /= (float)Input.touches.Length;
				Vector3 averageTouchWorld = _thisCamera.ScreenToWorldPoint(averageTouch);
				_hexGrid.SetDeltaPosition(averageTouchWorld - _lastAverageTouchWorld);
				_lastAverageTouchWorld = averageTouchWorld;
			}
		}
		else
		{
			Int2 gridIndices;
			if(Application.isEditor)
			{
				if(Input.GetMouseButton(0))
				{
					if(_hexGrid.Touched(_thisCamera.ScreenToWorldPoint(Input.mousePosition)
					                    , out gridIndices))
					{
//						Debug.Log("touched " + gridIndices.x + " " + gridIndices.y);
						_hexGridLogic.TouchedGridElement(gridIndices);
					}
				}
			}
			if(Input.touchCount == 1)
			{
				if(Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Moved)
				{
					if(_hexGrid.Touched(_thisCamera.ScreenToWorldPoint(Input.touches[0].position) 
					                    , out gridIndices))
					{
						_hexGridLogic.TouchedGridElement(gridIndices);
					}
				}
			}
		}
		_lastTouchCount= Input.touchCount;
	}
	#endregion
}
