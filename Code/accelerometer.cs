using UnityEngine;
using System.Collections;

public class accelerometer : MonoBehaviour {
	public GameObject red;
	public GameObject green;
	public GameObject blue;
	public GUIText a;
	public GUIText b;
	public GUIText c;
	private float x;
	private float y;
	private float z;
		public float d;
	private float xspeed=0.006f;
	// Use this for initialization
	void Start () {
				Input.gyro.enabled = true;
				Screen.sleepTimeout = SleepTimeout.NeverSleep;

	}
	
	// Update is called once per frame
	void Update () {
			
				x = Input.gyro.rotationRate.x;
				y = Input.gyro.rotationRate.y;
				z = Input.gyro.rotationRate.z;
		a.text = string.Format ("x = {0:0.00}",x);
		b.text = string.Format ("y = {0:0.00}",y);
		c.text = string.Format ("z = {0:0.00}",z);
		red.transform.Translate ( xspeed, 0, 0);
		red.transform.position = new Vector3 (red.transform.position.x, (x), red.transform.position.z);
		green.transform.Translate ( xspeed, 0, 0);
		green.transform.position = new Vector3 (green.transform.position.x, (y), green.transform.position.z);
		blue.transform.Translate ( xspeed, 0, 0);
		blue.transform.position = new Vector3 (blue.transform.position.x, (z), blue.transform.position.z);
	}
}
