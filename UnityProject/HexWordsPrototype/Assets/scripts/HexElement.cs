using UnityEngine;
using System.Collections;

public class HexElement : MonoBehaviour 
{
	private GameObject _touchStateTemplateClone;

	public void Touched(HexGridView hexGrid)
	{
		if(_touchStateTemplateClone == null)
		{
			ChangeStateToTuched(hexGrid.GetTouchedStateTemplate());
		}
	}

	public void ClearTouch()
	{
		if(_touchStateTemplateClone != null)
		{
			GameObject.Destroy(_touchStateTemplateClone);
			_touchStateTemplateClone = null;
		}
	}

	public void SetLetter(string letter)
	{
		GetComponentInChildren<TextMesh>().text = letter;
	}

	private void ChangeStateToTuched(GameObject touchStateTemplate)
	{
		if(_touchStateTemplateClone == null)
		{
			_touchStateTemplateClone = GameObject.Instantiate(touchStateTemplate) as GameObject;
			Vector3 localPosition = touchStateTemplate.transform.localPosition;
			_touchStateTemplateClone.transform.parent = this.transform;
			_touchStateTemplateClone.transform.localScale = Vector3.one;
			_touchStateTemplateClone.transform.localPosition = localPosition;
			_touchStateTemplateClone.transform.localRotation = Quaternion.identity;
			_touchStateTemplateClone.SetActive(true);
		}
	}

	private void Solved()
	{

	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
