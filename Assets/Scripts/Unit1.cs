using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

using Random = System.Random;
public class Unit1 : MonoBehaviour
{
    public int cost;
    [SerializeField] public int health = 1;
    public int damage = 5;
    public bool isOurTeam = true;
    public float speed = 10.0f;
    private Vector3 moveVector;
    private Rigidbody2D rb;

    private AudioSource source;

    public GameObject HP;

    private Random rnd = new Random();
    private int rand;
    protected void Start()
    {
        rand = rnd.Next(-2,3);
        gameObject.layer = isOurTeam ? 8 : 9;

        source = GetComponent<AudioSource>();
        moveVector = isOurTeam
            ? Vector3.left
            : Vector3.right;
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

    public float maxDistance = 1.0f;
    public GameObject bulletPrefab;
    [SerializeField] private bool isMeele;

    public Vector3 GetMVector()
    {
        return moveVector;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Area"))
        {
            if (other.gameObject.transform.parent.GetComponent<Unit1>().isOurTeam ^ isOurTeam)
            {
                moveVector = new Vector3(
                    other.gameObject.transform.parent.transform.position.x - gameObject.transform.position.x,
                    other.gameObject.transform.parent.transform.position.y - gameObject.transform.position.y);

                moveVector = moveVector.normalized;
            }
        }
    }

    Collider2D IsHit(int d)
    {
        RaycastHit2D hit = new RaycastHit2D();
        hit = Physics2D.Raycast(
            gameObject.transform.position + moveVector * (gameObject.GetComponent<Transform>().localScale.x / 2 + 1.1f),
            moveVector + new Vector3(0, d, 0), maxDistance, gameObject.layer == 8 //LayerMask.GetMask("Team1")
                ? LayerMask.GetMask("Team2")
                : LayerMask.GetMask("Team1"));
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Actor"))
            {
                if (hit.collider.gameObject.GetComponent<Unit1>().isOurTeam ^ isOurTeam)
                {
                    return hit.collider;
                }
            }
        }

        return null;
    }

    protected void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = rand + (int) (-Mathf.Floor(transform.position.y * 10) + 100);

        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Run")) //wait <= 0)
        {
            rb.velocity = moveVector * speed;
        }

        ///смотрим вперед
        Collider2D hit = IsHit(0);
        /// если перед нами никого нет смотрим вверх
        if (hit == null)
            hit = IsHit(1);
        /// если перед нами и сверху никого нет смотрим вних
        if (hit == null)
            hit = IsHit(-1);

        ///если анимация атаки закончилась
        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Finish Shoot"))
        {
            ///при атаке играем звук
            source.PlayOneShot(shootSound);

            ///если это рукопашный юнит
            if (isMeele)
            {
                if (hit != null)
                {
                    ///не из нашей команды
                    if (isOurTeam ^ hit.gameObject.GetComponent<Unit1>().isOurTeam)
                    {
                        ///наносим ему урон
                        hit.gameObject.GetComponent<Unit1>().Damage(damage);
                    }
                }
            }
            ///если стрелок
            else
            {
                if (hit != null)
                {
                    ///создаем патрон который летит туда где мы увидели врага
                    Instantiate(bulletPrefab, gameObject.transform.position,    // + moveVector,
                                Quaternion.Euler(0, 0,
                                    Mathf.Atan2(hit.gameObject.transform.position.y - transform.position.y,
                                        hit.gameObject.transform.position.x - transform.position.x) * Mathf.Rad2Deg))
                            .GetComponent<Rigidbody2D>().velocity = new Vector3(
                                                                            hit.gameObject.transform
                                                                                .position
                                                                                .x - transform.position.x,
                                                                            hit.gameObject.transform
                                                                                .position
                                                                                .y - transform.position.y, 0)
                                                                        .normalized * bulletPrefab
                                                                        .GetComponent<Bullet>().Speed;
                }
            }

            gameObject.GetComponent<Animator>().Play("Idle");
        }

        ///если нигде никого нет идем дальше
        if (hit == null) return;

        ///если мы видим что то и оно Актор
        if (hit.gameObject.CompareTag("Actor"))
        {
            ///если оно не из нашей команды
            if (hit.gameObject.GetComponent<Unit1>().isOurTeam ^ isOurTeam)
            {
                ///и в нашей зоне действия
                if (Mathf.Abs(gameObject.transform.position.x - hit.gameObject.transform.position.x) < maxDistance)
                {
                    ///и если мы готовы атаковать
                    if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Run")
                        || gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        ///останавливаемся
                        rb.velocity = Vector3.zero;
                        ///говорим аниматору что нужно вызвать анимацию удара
                        gameObject.GetComponent<Animator>().SetTrigger("Fire");
                    }
                }
            }
        }
    }
}