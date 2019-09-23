using UnityEngine;

public class Bullet : MonoBehaviour
{

	public int Speed;
	// Use this for initialization
	private Rigidbody2D rb;
	[SerializeField]private bool isOurTeam;
	void Awake ()
	{
		Vector3 moveVector = isOurTeam ? Vector3.left : Vector3.right;
		gameObject.transform.position += new Vector3(0,0,-1);
		rb = gameObject.GetComponent<Rigidbody2D>();
		rb.velocity = new Vector3(Speed * moveVector.x, rb.velocity.y);
	}

	[SerializeField]private int damage;


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Bullet")) return;
		if (other.gameObject.CompareTag("Area")) return;
		if (!other.gameObject.GetComponent<Unit1>().isOurTeam ^ isOurTeam) return;
		other.gameObject.GetComponent<Unit1>().Damage(damage);
		Destroy(gameObject);
	}

	private int lifetime = 500;

	void FixedUpdate()
	{
		lifetime--;
		if (lifetime <= 0)
		{
			Destroy(gameObject);
		}
	}
}
