using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;

public class MeasurementsToFile : MonoBehaviour 
{
	public float delay = 0.1f; // delay between readings
	public float[] taus; // array of time constants for filters
	public Text buttonText;

	[HideInInspector] public bool measuring = false;

	private List<float[]> measurements = new List<float[]> (); // list of accelerometer readings
	private IEnumerator coroutine; // reference to measurements coroutine
	private LowPassFilter[] filters; // array of filters; filters.Length == taus.Length
	private float startTime;

	void Awake () 
	{
		// create array of filters with different taus' values
		filters = new LowPassFilter[taus.Length];
		for (int i = 0; i < filters.Length; i++) 
			filters [i] = new LowPassFilter (taus [i]);
	}

	public void StartMeasurements ()
	{
		if (coroutine != null)
			StopCoroutine (coroutine);

		coroutine = Measure ();
		measurements.Clear (); // clear all stored readings
		measuring = true;
		startTime = Time.time; // store time when collecting started
		for (int i = 0; i < filters.Length; i++)
			filters [i].Reset (); // reinitialize all filters

		StartCoroutine (coroutine);

		Debug.Log ("Measurements start!");
	}

	public void StopMeasurements () 
	{
		StopCoroutine (coroutine);
		measuring = false;
		WriteToFile ();
	}

	public void UpdateButtonText ()
	{
		if (measuring)
			buttonText.text = "Stop";
		else
			buttonText.text = "Start";
	}

	public void ButtonClick ()
	{
		if (measuring)
			StopMeasurements ();
		else
			StartMeasurements ();
		UpdateButtonText ();
	}

	IEnumerator Measure () 
	{
		while (true) // run forever
		{
			float rawX = Input.acceleration.x;
			float rawY = Input.acceleration.y;
			float rawZ = Input.acceleration.z;
			
			float t=System.DateTime.Now.Hour;
			float m=System.DateTime.Now.Minute;
			float s=System.DateTime.Now.Second;
			float ms=System.DateTime.Now.Millisecond;
			float[] row = new float[7 + filters.Length]; // create new array with readings

			row [0] = t;
			row [1] = m;
			row [2] = s;
			row [3] = ms;
			row [4] = rawX;
			row [5] = rawY;
			row [6] = rawZ;
			for (int i = 0; i < filters.Length; i++)
				row [7 + i] = filters [i].NextStep (delay, rawX); // calculate new filtered values from filters
			measurements.Add (row); // add row to list

			yield return new WaitForSeconds (delay);
		}
	}

	// creates files with all measurements 
	private void WriteToFile () 
	{
		StringBuilder sb = new StringBuilder ();
		for (int i = 0; i < measurements.Count; i++) 
		{
		
sb.AppendFormat ("{0:00}:{1:00}:{2:00}:{3:000}   {4:0.000}  {5:0.000}  {6:0.000}", measurements [i][0],measurements [i][1],measurements [i][2],measurements [i][3],measurements [i][4],measurements [i][5],measurements [i][6]);
				
					sb.Append (", ");
		
			sb.Append (Environment.NewLine);
		}
		File.WriteAllText (@Application.persistentDataPath + "/measurements.txt", sb.ToString ());
		Debug.Log ("File written! Measures count: " + measurements.Count);
	}
}
