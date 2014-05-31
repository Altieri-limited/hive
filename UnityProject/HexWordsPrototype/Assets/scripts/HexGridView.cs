﻿using UnityEngine;
using System.Collections;

public class HexGridView : MonoBehaviour 
{
	#region Unity Editor Fields
	[SerializeField]
	private GameObject _hexTemplate;
	
	[SerializeField]
	private GameObject _hexStatesTemplate;

	[SerializeField]
	private GameObject touchedStateTemplate;
	
	[SerializeField]
	private GameObject solvedStateTemplate;
	#endregion

	private HexElement[,] _grid;

	private Vector3 _desiredLocalPosition;
	private const float LERP_INV_TIME_CONST = 5.0f;// = 1.0 / 0.2
	private int _minXInd;
	private int _minYInd;
	private int _maxXInd;
	private int _maxYInd;
	private float _xSpacing;
	private float _ySpacing;
	private float _hexRadius;

	public void Touched(Vector3 worldPosition)
	{
		Vector3 localPosition = this.transform.InverseTransformPoint(worldPosition);
		float minSqHexX = -_hexRadius * 0.5f;
		float sqHexW = 1.5f * _hexRadius;
		if(localPosition.x < 0)
		{
			minSqHexX = _hexRadius;
		}
		float minSqHexY = - _ySpacing * 0.5f;
		if(localPosition.y < 0)
		{
			minSqHexY = -minSqHexY;
		}
		int xInd = (int)((localPosition.x - minSqHexX) / sqHexW);
		float y = localPosition.y;
		if((xInd & 0x1) != 0)
		{
			minSqHexX += _ySpacing * 0.5f;
		}
		int yInd = (int)((y - minSqHexY) / _ySpacing);

		if(xInd < _minXInd || xInd > _maxXInd || yInd < _minYInd || yInd > _maxYInd)
		{}
		else
		{
			HexElement hexElement = _grid[xInd - _minXInd, yInd - _minYInd];
			if((localPosition - hexElement.transform.localPosition).sqrMagnitude < _ySpacing * _ySpacing * 0.25f)
			{
				hexElement.Touched(this);
			}
		}
	}

	public GameObject GetTouchedStateTemplate()
	{
		return touchedStateTemplate;
	}
	
	public void SetDeltaPosition(Vector3 deltaPos)
	{
		Vector3 deltaLocalPos = this.transform.InverseTransformDirection(deltaPos);
		deltaLocalPos.z = 0.0f;
		this.transform.Translate(deltaLocalPos, Space.Self);
	}

	public void InitializeGrid(Camera camera, float viewPortHexElementWidth 
	                           , float viewportHexElementHeight, int rowsCount, int colsCount)
	{
		_grid = new HexElement[colsCount, rowsCount];
		_minXInd = -colsCount / 2;
		_minYInd = -rowsCount / 2;
		_maxXInd = _minXInd + colsCount;
		_maxYInd = _minYInd + rowsCount;
		_hexTemplate.SetActive(true);
		_hexTemplate.SetActive(true);
		_hexTemplate.GetComponent<ScreenAdaptingQuad>().UpdateScaleToScreen(camera, viewPortHexElementWidth,
		                                                                    viewportHexElementHeight);
		_hexStatesTemplate.GetComponent<ScreenAdaptingQuad>().UpdateScaleToScreen(camera, viewPortHexElementWidth,
		                                                                         viewportHexElementHeight);
		Vector3 localHexScale = _hexTemplate.transform.localScale;
		_hexTemplate.SetActive(false);
		_hexRadius = _hexTemplate.transform.localScale.x * 0.5f;
		_xSpacing = 1.5f * _hexRadius;
		_ySpacing = Mathf.Sqrt(3.0f) * _hexRadius;
		float localZ = +_hexTemplate.transform.localPosition.z;
		for(int yInd = _minYInd; yInd < _minYInd + rowsCount; yInd++)
		{
			for(int xInd = _minXInd; xInd < _minXInd + colsCount; xInd++)
			{
				GameObject hex = GameObject.Instantiate(_hexTemplate) as GameObject;
				hex.transform.parent = _hexTemplate.transform.parent;
				hex.transform.localScale = localHexScale;
				hex.transform.localRotation = _hexTemplate.transform.localRotation;
				Vector2 gridPos = CalculateHexLocalPosition(xInd, yInd, _xSpacing, _ySpacing);
				hex.transform.localPosition = new Vector3(gridPos.x, gridPos.y, localZ);
				hex.SetActive(true);
				_grid[xInd - _minXInd, yInd - _minYInd] = hex.GetComponent<HexElement>();
			}
		}
		_hexTemplate.SetActive(false);
		_hexStatesTemplate.SetActive(false);
	}

	public bool SetLetter(int xInd, int yInd, string letter)
	{
		bool result = true;
		if(xInd < 0 || xInd > _maxXInd - _minXInd
		   || yInd < 0 || yInd > _maxYInd - _minYInd)
		{
			result = false;
		}
		else
		{
			_grid[xInd, yInd].SetLetter(letter);
		}
		return result;
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


}
