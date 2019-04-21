using UnityEngine;

public class Pause : MonoBehaviour
{
	private float tmp;
	public void toggle()
	{
		Time.timeScale = tmp - Time.timeScale;
	}

	// Use this for initialization
	void Start ()
	{

		tmp = Time.timeScale;
	}
	
}
