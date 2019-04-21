using UnityEngine;
using Random = System.Random;

public class SpawnMashine : MonoBehaviour
{

	private int resourse = 10;
	private float maxDistance = 8;
	private Vector3 moveVector;
	public GameObject unitB;
	public GameObject unitZ;
	public GameObject unitK;
	public GameObject unitL;
	// Use this for initialization
	private Random rnd = new Random();
	void Start ()
	{
		moveVector = gameObject.GetComponent<Unit1>().GetMVector();
		maxValue = Mathf.Max(Mathf.Max(unitB.GetComponent<Unit1>().cost, unitL.GetComponent<Unit1>().cost),
			Mathf.Max(unitZ.GetComponent<Unit1>().cost, unitK.GetComponent<Unit1>().cost));
	}

	public int reloadMax = 60;
	private int reload = 60;
	private int resReload = 100;
	private int maxValue;
	// Update is called once per frame
	void FixedUpdate()
	{

		resReload--;
		if (resReload <= 0)
		{
			resourse++;
			resReload = 100;
		}
		
	if (reload <= 0)
	{
		if (resourse >= maxValue)
		{
			Collider2D[] clds = Physics2D.OverlapCircleAll(gameObject.transform.position + moveVector * 4.0f, 3.0f);
			int count = 0;
			foreach (var colider in clds)
			{
				if (colider.gameObject.CompareTag("Actor") && colider.gameObject.GetComponent<Unit1>().isOurTeam)
				{
					count++;
				}
			}
			int spawnY;
			int unitType;

			if (count <= 0)
			{
				unitType = rnd.Next(1, 5);
				spawnY = rnd.Next(-4, 5);
				reloadMax = 60;
			}
			else
			{
				reloadMax = 10;
				resourse = maxValue;
				unitType = rnd.Next(2, 4);
				spawnY = 0;
			}
			
			switch (unitType)
			{
				case 1:
					{
						Instantiate(unitK, new Vector3(transform.position.x, spawnY, 0), Quaternion.Euler(0, 0, 0));
						reload = reloadMax;
						resourse -= unitK.GetComponent<Unit1>().cost;
					}

					break;
				case 2:
					{
						Instantiate(unitB, new Vector3(transform.position.x, spawnY, 0), Quaternion.Euler(0, 0, 0));
						reload = reloadMax;
						resourse -= unitB.GetComponent<Unit1>().cost;
					}

					break;
				case 3:
					{
						Instantiate(unitZ, new Vector3(transform.position.x, spawnY, 0), Quaternion.Euler(0, 0, 0));
						reload = reloadMax;
						resourse -= unitZ.GetComponent<Unit1>().cost;
					}

					break;
				case 4:
					{
						Instantiate(unitL, new Vector3(transform.position.x, spawnY, 0), Quaternion.Euler(0, 0, 0));
						reload = reloadMax;
						resourse -= unitL.GetComponent<Unit1>().cost;
					}

					break;
			}
		}
	}
	else
		{
			reload--;
		}
	}
}
