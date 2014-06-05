using UnityEngine;
using System.Collections;

public class ScreenAdaptingQuad : MonoBehaviour 
{
	public void UpdateScaleToScreen(Camera camera)
	{
		//assuming the quad is aligned to the current camera
		//the quad normal is parallel to the camera z axis
		//resetting the scale
		float zFromCamera = Vector3.Dot (transform.position - camera.transform.position, camera.transform.forward);
		Vector3 worldScreenMin = camera.ViewportToWorldPoint (new Vector3 (0.0f, 0.0f, zFromCamera));
		Vector3 worldScreenMax = camera.ViewportToWorldPoint (new Vector3 (1.0f, 1.0f, zFromCamera));
		float xScreenSizeWorld = Mathf.Abs( Vector3.Dot (camera.transform.right, worldScreenMax - worldScreenMin));
		float yScreenSizeWorld = Mathf.Abs( Vector3.Dot (camera.transform.up, worldScreenMax - worldScreenMin));
		transform.localScale = new Vector3 (xScreenSizeWorld, yScreenSizeWorld, 1.0f);
	}

	/// <summary>
	/// Updates the scale to screen. It uses the passed parameters to se the 3D scale
	/// of the game object to which it is attached so that the viewport (rendered by the Camera in the parameters)
	/// width and height are equals to those passed as parameters.
	/// It assumes that the local x and y axes are, respectively, the horizontal and the vertical
	/// axes and the local z is parallel to the camera forward axis.
	/// </summary>
	/// <param name="camera">Camera.</param>
	/// <param name="viewportWidth">Viewport width.</param>
	/// <param name="viewportHeight">Viewport height.</param>
	public void UpdateScaleToScreen(Camera camera, float viewportWidth, float viewportHeight)
	{
		//assuming the quad is aligned to the current camera
		//the quad normal is parallel to the camera z axis
		//resetting the scale
		float zFromCamera = Vector3.Dot (transform.position - camera.transform.position, camera.transform.forward);
		Vector3 cameraViewportPosition = camera.WorldToViewportPoint(transform.position);
		Vector3 viewportDesiredMin = cameraViewportPosition - new Vector3(viewportWidth * 0.5f, 
		                                                                  viewportHeight * 0.5f, 0.0f);
		Vector3 viewportDesiredMax = viewportDesiredMin + new Vector3(viewportWidth, viewportHeight, 0.0f);
		Vector3 worldScreenMin = camera.ViewportToWorldPoint (new Vector3 (viewportDesiredMin.x,
		                                                                   viewportDesiredMin.y, zFromCamera));
		Vector3 worldScreenMax = camera.ViewportToWorldPoint (new Vector3 (viewportDesiredMax.x,
		                                                                   viewportDesiredMax.y, zFromCamera));
		float xScreenSizeWorld = Mathf.Abs( Vector3.Dot (camera.transform.right, worldScreenMax - worldScreenMin));
		float yScreenSizeWorld = Mathf.Abs( Vector3.Dot (camera.transform.up, worldScreenMax - worldScreenMin));
		transform.localScale = new Vector3 (xScreenSizeWorld, yScreenSizeWorld, 1.0f);
	}
}
