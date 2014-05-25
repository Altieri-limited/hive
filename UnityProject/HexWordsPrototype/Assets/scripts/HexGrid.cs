using UnityEngine;
using System.Collections;

public class HexGrid : MonoBehaviour 
{
	private Vector3 _desiredLocalPosition;
	private const float LERP_INV_TIME_CONST = 5.0f;//1.0 / 0.2

	public void SetDeltaPosition(Vector3 deltaPos)
	{
		Vector3 deltaLocalPos = this.transform.InverseTransformDirection(deltaPos);
		deltaLocalPos.z = 0.0f;
		this.transform.Translate(deltaLocalPos, Space.Self);
	}

//	public void SetDesiredPostion(Vector3 desiredPosition)
//	{
//		Vector3 desiredLocalPosition = this.transform.InverseTransformPoint(desiredPosition);
//		desiredLocalPosition.z = this.transform.localPosition.z;//no need to change the z for the purpose of this function
//		_desiredLocalPosition = desiredLocalPosition;
//		Debug.Log ("desired local position " + _desiredLocalPosition);
//	}
//
//	// Use this for initialization
//	void Start ()
//	{
//		SetDesiredPostion(transform.position);
//	}
//	
//	// Update is called once per frame
//	void Update ()
//	{
//		transform.localPosition = Vector3.Lerp(transform.localPosition, _desiredLocalPosition, Time.deltaTime * LERP_INV_TIME_CONST);
//	}
}
