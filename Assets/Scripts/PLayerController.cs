/// Umang Agarwal

using UnityEngine;
using System.Collections;


[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PLayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	void Update ()   //Called everytime frame is updated
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audio.Play ();
		}
	}

	void FixedUpdate()    //Called whenever physics is used
	{   float moveHorizontal = Input.GetAxis ("Horizontal");  //Input.GetAxis("Mouse X");
		float moveVertical = Input.GetAxis ("Vertical");  //Input.GetAxis("Mouse Y");

		if (Input.touchCount > 0)
				{
						moveHorizontal = Input.touches [0].deltaPosition.x; //Input.GetAxis ("Horizontal");
						moveVertical = Input.touches [0].deltaPosition.y; //Input.GetAxis ("Vertical");
				}

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;

		rigidbody.position = new Vector3 
			(
				Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
				);
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}
	

}
