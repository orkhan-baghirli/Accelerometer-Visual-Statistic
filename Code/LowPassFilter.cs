using UnityEngine;

public class LowPassFilter 
{
	public float filteredValue;
	private float tau; // filter's time constant - lower = faster reponse + weaker noise suppresion, higher = slower, smoother response
	private int iteration = 0;

	public LowPassFilter (float tau)
	{
		this.tau = tau;
	}

	public float NextStep (float h, float raw) {
		if (iteration == 0) // if it's the first iteration
			filteredValue = raw; // just initate filteredValue
		else 
		{
			float alpha = Mathf.Exp (-h / tau); // calculate alfa value based on time step and filter's time constant
			filteredValue = alpha * filteredValue + (1 - alpha) * raw; // calculate new filteredValue from previous value and new raw value
		}
		iteration++; // increment iteration number
		return filteredValue;
	}

	public void Reset ()
	{
		iteration = 0; // reset iteration count / force filteredValue initalization
	}
}
