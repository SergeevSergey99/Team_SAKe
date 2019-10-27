using UnityEngine;
using Random = System.Random;

public class SpawnMashine_3d : MonoBehaviour
{

	private int resourse = 10;
//	private float maxDistance = 8;
	private Vector3 moveVector;
	public GameObject unitB;
	public GameObject unitZ;
	public GameObject unitK;
	public GameObject unitL;
	// Use this for initialization
	private Random rnd = new Random();
	void Start ()
	{
		moveVector = gameObject.GetComponent<Unit1_3d>().GetMVector();
		maxValue = Mathf.Max(Mathf.Max(unitB.GetComponent<Unit1_3d>().cost, unitL.GetComponent<Unit1_3d>().cost),
			Mathf.Max(unitZ.GetComponent<Unit1_3d>().cost, unitK.GetComponent<Unit1_3d>().cost));
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
			Collider[] clds = Physics.OverlapSphere(gameObject.transform.position + moveVector * 4.0f, 3.0f);
			int count = 0;
			foreach (var colider in clds)
			{
				if (colider.gameObject.CompareTag("Actor") && colider.gameObject.GetComponent<Unit1_3d>().isOurTeam^gameObject.GetComponent<Unit1_3d>().isOurTeam)
				{
					count++;
				}
			}
			int spawnZ;
			int unitType;

			if (count <= 0)
			{
				unitType = rnd.Next(1, 5);
				spawnZ = rnd.Next(-4, 5);
				reloadMax = 60;
			}
			else
			{
				reloadMax = 10;
				resourse = maxValue;
				unitType = rnd.Next(2, 4);
				spawnZ = 0;
			}
			
			switch (unitType)
			{
				case 1:
					{
						Instantiate(unitK, new Vector3(transform.position.x, 0, spawnZ), Quaternion.Euler(45, 0, 0));
						reload = reloadMax;
						resourse -= unitK.GetComponent<Unit1_3d>().cost;
					}

					break;
				case 2:
					{
						Instantiate(unitB, new Vector3(transform.position.x, 0, spawnZ), Quaternion.Euler(45, 0, 0));
						reload = reloadMax;
						resourse -= unitB.GetComponent<Unit1_3d>().cost;
					}

					break;
				case 3:
					{
						Instantiate(unitZ, new Vector3(transform.position.x, 0, spawnZ), Quaternion.Euler(45, 0, 0));
						reload = reloadMax;
						resourse -= unitZ.GetComponent<Unit1_3d>().cost;
					}

					break;
				case 4:
					{
						Instantiate(unitL, new Vector3(transform.position.x, 0, spawnZ), Quaternion.Euler(45, 0, 0));
						reload = reloadMax;
						resourse -= unitL.GetComponent<Unit1_3d>().cost;
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
