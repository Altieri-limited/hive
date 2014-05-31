using UnityEngine;
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
	private HexGridView _hexGrid;
	private Camera _thisCamera;
	private Vector3 _lastAverageTouchWorld;
	private int _lastTouchCount;
	private string[] _alphabet;
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
		const int ALPHABET_LENGTH = 26;
		_alphabet = new string[ALPHABET_LENGTH];
		char c = 'A';
		for(int i = 0; i < _alphabet.Length; i++)
		{
			_alphabet[i] = new string(new char[]{c});
			c++;
		}
		int a = 0;
		for(int y = 0; y < ROWS; y++)
		{
			for(int x = 0; x < COLS; x++)
			{
				_hexGrid.SetLetter(x, y, _alphabet[a++]);
				if(a == _alphabet.Length)
				{
					a = 0;
				}
			}
		}
	}

	#region Unity Methods Implementations
	private void Awake()
	{
		_thisCamera = GetComponent<Camera>();
		_hexGrid = GetComponentInChildren<HexGridView>();
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
			if(Application.isEditor)
			{
				if(Input.GetMouseButton(0))
				{
					_hexGrid.Touched(_thisCamera.ScreenToWorldPoint(Input.mousePosition));
				}
			}
			if(Input.touchCount == 1)
			{
				if(Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Moved)
				{
					_hexGrid.Touched(_thisCamera.ScreenToWorldPoint(Input.touches[0].position));
				}
			}
		}
		_lastTouchCount= Input.touchCount;
	}
	#endregion
}
