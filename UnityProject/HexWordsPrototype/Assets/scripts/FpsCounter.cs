using UnityEngine;
using System.Collections;

public class FpsCounter : MonoBehaviour 
{
	private float _lastTime;
	private float _timer;
	private int _framesCount;
	private GUIText _guiText;

	private void OnEnable()
	{
		_guiText = GetComponent<GUIText>();
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		_framesCount++;
		float realTime = Time.realtimeSinceStartup;
		float time = realTime - _lastTime;
		_lastTime = realTime;
		_timer += time;
		if(_timer > 0.250f)
		{
			if(_framesCount < 30 * 0.25)
			{
				_guiText.color = Color.red;
			}
			else
			{
				_guiText.color = Color.black;
			}
			_guiText.text = "" + ((float) _framesCount / _timer);
			_timer = 0;
			_framesCount = 0;
		}
	}
}
