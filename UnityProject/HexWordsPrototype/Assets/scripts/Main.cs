using UnityEngine;
using System.Collections;
using System;

public class Main : MonoBehaviour 
{
	[SerializeField]
	private GameObject _hexTemplate;
	private const float FINGER_SIZE = 0.5f;
	private const int ROWS = 9;
	private const int COLS = 12;

	private void InitializeGrid()
	{
		Camera thisCamera = GetComponent<Camera>();
		float dpi = Screen.dpi;
		if(dpi != 0)
		{
			Debug.Log("screen DPI found = " + dpi);
		}
		else
		{
			Debug.LogWarning("screen DPI not found, defaulting to 50");
			dpi = 75.0f;
		}
		_hexTemplate.SetActive(true);
		float viewportWidth = FINGER_SIZE * dpi / (float)Screen.width;
		float viewportHeight = FINGER_SIZE * dpi / (float)Screen.height;
		_hexTemplate.GetComponent<ScreenAdaptingQuad>().UpdateScaleToScreen(thisCamera, viewportWidth,
		                                                                    viewportHeight);
		Vector3 localHexScale = _hexTemplate.transform.localScale;
		_hexTemplate.SetActive(false);
		float hexRadius = _hexTemplate.transform.localScale.x * 0.5f;
		float xSp = 1.5f * hexRadius;
		float ySp = Mathf.Sqrt(3.0f) * hexRadius;
		int minXInd = -COLS / 2;
		int minYInd = -ROWS / 2;
		float localZ = +_hexTemplate.transform.localPosition.z;
		for(int yInd = minYInd; yInd <= minYInd + ROWS; yInd++)
		{
			for(int xInd = minXInd; xInd <= minXInd + COLS; xInd++)
			{
				GameObject hex = GameObject.Instantiate(_hexTemplate) as GameObject;
				hex.transform.parent = _hexTemplate.transform.parent;
				hex.transform.localScale = localHexScale;
				hex.transform.localRotation = _hexTemplate.transform.localRotation;
				Vector2 gridPos = CalculateHexLocalPosition(xInd, yInd, xSp, ySp);
				hex.transform.localPosition = new Vector3(gridPos.x, gridPos.y, localZ);
				hex.SetActive(true);
			}
		}
	}

	private Vector2 CalculateHexLocalPosition(int xInd, int yInd, float xSp, float ySp)
	{
		float x = xInd * xSp;
		float y = yInd * ySp;
		if((xInd & 0x1) != 0)
		{
			y += ySp * 0.5f;
		}
		return new Vector2(x, y);
	}

	// Use this for initialization
	void Start () 
	{
		Camera thisCamera = GetComponent<Camera>();
		thisCamera.orthographicSize = Screen.height * 0.5f;
		GetComponentInChildren<ScreenAdaptingQuad>().UpdateScaleToScreen(this.GetComponent<Camera>());
		InitializeGrid();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
