using UnityEngine;

public class Bullet_3d : MonoBehaviour
{

	public int Speed;
	// Use this for initialization
	private Rigidbody rb;
	[SerializeField]public bool isOurTeam = false;
	void Awake ()
	{
		Vector3 moveVector = isOurTeam ? Vector3.left : Vector3.right;
		gameObject.transform.position += new Vector3(0,0,-1);
		rb = gameObject.GetComponent<Rigidbody>();
		rb.velocity = new Vector3(Speed * moveVector.x, rb.velocity.y);
	}

	[SerializeField]private int damage = 0;


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Bullet")) return;
		if (other.gameObject.CompareTag("Area")) return;
		if (!other.gameObject.GetComponent<Unit1_3d>().isOurTeam ^ isOurTeam) return;
		other.gameObject.GetComponent<Unit1_3d>().Damage(damage);
		Destroy(gameObject);
	}

	private int lifetime = 500;

	void FixedUpdate()
	{
		GetComponent<SpriteRenderer>().sortingOrder = (int) (-Mathf.Floor(transform.position.z * 10) + 100)*(transform.position.y>=0?1:-1);


		if (transform.position.y < 0)
		{
			Destroy(gameObject);
			return;
		}
		lifetime--;
		if (lifetime <= 0)
			Destroy(gameObject);
		
	}
}
