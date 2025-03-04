using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCam : MonoBehaviour {
	public GameObject webPlane;
	GameObject camParent;
	// Use this for initialization
	void Start () {
		camParent = new GameObject ("CamParent");
		camParent.transform.position = this.transform.position;
		this.transform.parent = camParent.transform;
		camParent.transform.Rotate (Vector3.right, 90);
		Input.gyro.enabled = true;
		
		///WebCamTexture webcamTexture = new WebCamTexture ();// permissiona gore sondurulub istifade edildikc=de slah siline biler
		//webPlane.GetComponent<MeshRenderer> ().material.mainTexture = webcamTexture;
		//webcamTexture.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		//Quaternion rotFix = new Quaternion (Input.gyro.attitude.x, Input.gyro.attitude.y, -Input.gyro.attitude.z, -Input.gyro.attitude.w);
	//	this.transform.localRotation = rotFix;
		
	}
}