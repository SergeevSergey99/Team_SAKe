using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class Unit1 : MonoBehaviour
{
    public int cost;
    [SerializeField] public int health = 1;
    public int damage = 5;
    public bool isOurTeam = true;
    public float speed = 10.0f;
    public int fireRate = 100;
    private Vector3 moveVector;
    private Rigidbody2D rb;

    private AudioSource source;

    public GameObject HP;

    // Use this for initialization
    protected void Start()
    {
        source = GetComponent<AudioSource>();
        moveVector = isOurTeam
            ? new Vector3(-10 - gameObject.transform.position.x, -gameObject.transform.position.y)
            : new Vector3(10 - gameObject.transform.position.x, -gameObject.transform.position.y);
        moveVector = moveVector.normalized;
        rb = gameObject.GetComponents<Rigidbody2D>()[0];

        rb.velocity = moveVector * speed;
    }

    public void Damage(int dmg)
    {
        health -= dmg;
        if (HP != null)
            HP.GetComponent<Image>().fillAmount = health / 100.0f;
        if (health <= 0)

        {
            Destroy(gameObject);
            if (speed == 0)
            {
                if (isOurTeam)
                {
                    SceneManager.LoadScene("Lose");
                }
                else
                {
                    SceneManager.LoadScene("Win");
                }
            }
        }
    }

    public AudioClip shootSound;


    // Update is called once per frame
    public float maxDistance = 1.0f;


    public GameObject bulletPrefab;
    [SerializeField] private bool isMeele;

    void ArcherAttack(Vector3 v)
    {
        source.PlayOneShot(shootSound);
        gameObject.GetComponent<Animator>().Play("Shoot");
        animCount = 100;
        Instantiate(bulletPrefab, gameObject.transform.position + moveVector, Quaternion.Euler(0, 0, 0))
            .GetComponent<Rigidbody2D>().velocity = v; //.GetComponent<Bullet>().Set(moveVector, dmg, isOurTeam);
    }

    private int animCount = 0;

    void MeeleAttack(GameObject obj)
    {
        animCount = 100;

        source.PlayOneShot(shootSound);
        gameObject.GetComponent<Animator>().Play("Shoot");
        var enemyDamage = obj.GetComponent<Unit1>().damage;
        obj.GetComponent<Unit1>().Damage(damage);
        if (obj.GetComponent<Unit1>().maxDistance < maxDistance) return;
        Damage(enemyDamage);
    }

    private int wait = 0;

    public Vector3 GetMVector()
    {
        return moveVector;
    }

    protected void Update()
    {
        if (animCount > 0)
        {
            animCount--;
        }

        if (wait <= 0)
        {
            rb.velocity = moveVector * speed;
            if (animCount <= 0) gameObject.GetComponent<Animator>().Play("Run");
        }
        else wait--;

        RaycastHit2D hit = new RaycastHit2D();
        hit = Physics2D.Raycast(
            gameObject.transform.position + moveVector * (gameObject.GetComponent<Transform>().localScale.x / 2 + 1.1f),
            moveVector, maxDistance);
        if (!hit)
            hit = Physics2D.Raycast(
                gameObject.transform.position +
                moveVector * (gameObject.GetComponent<Transform>().localScale.x / 2 + 1.1f),
                new Vector3(moveVector.x * 3, 1, 0), maxDistance);
        if (!hit)
            hit = Physics2D.Raycast(
                gameObject.transform.position +
                moveVector * (gameObject.GetComponent<Transform>().localScale.x / 2 + 1.1f),
                new Vector3(moveVector.x * 3, -1, 0), maxDistance);
        if (!hit) return;
        if (hit.collider.gameObject.CompareTag("Actor"))
        {
            if (hit.collider.gameObject.GetComponent<Unit1>().isOurTeam ^ isOurTeam)
            {
                if (Mathf.Abs(gameObject.transform.position.x - hit.collider.gameObject.transform.position.x) <
                    maxDistance)
                {
                    if (wait <= 0)
                    {
                        rb.velocity = Vector3.zero;
                        if (animCount <= 0)
                            gameObject.GetComponent<Animator>().Play("Idle");
                        if (isMeele)
                            MeeleAttack(hit.collider.gameObject);
                        else
                            ArcherAttack(new Vector3(
                                hit.collider.gameObject.transform.position.x - transform.position.x,
                                hit.collider.gameObject.transform.position.y - transform.position.y, 0));
                        wait = fireRate;
                    }
                }
            }
        }
    }
}