using System;
using UnityEngine;
using UnityEngine.UI;

public class Reload : MonoBehaviour
{

	private int res;
	private int maxRes;
	
	void Start ()
	{
		res = Int32.Parse(gameObject.GetComponent<Text>().text);
		maxRes = res;
	}
	private int reload = 120;
	// Update is called once per frame
	
	void FixedUpdate ()
	{
		res = Int32.Parse(gameObject.GetComponent<Text>().text);

		if (res < maxRes)
		{
			reload--;
			if (reload <= 0)
			{
				res++;
				gameObject.GetComponent<Text>().text = res.ToString();
				reload = 120;
			}
		}
	}
}
