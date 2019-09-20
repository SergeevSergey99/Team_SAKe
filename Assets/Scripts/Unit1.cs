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
    private Vector3 moveVector;
    private Rigidbody2D rb;

    private AudioSource source;

    public GameObject HP;

    // Use this for initialization
    protected void Start()
    {
        target = Vector3.back;

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

    // Update is called once per frame
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

        Collider2D[] cld = Physics2D.OverlapPointAll(transform.position + moveVector + new Vector3(0,d,0));
        foreach (var collider2D in cld)
        {
            if (collider2D.gameObject.CompareTag("Actor"))
            {
                if (collider2D.gameObject.GetComponent<Unit1>().isOurTeam ^ isOurTeam)
                {
                    return collider2D;
                }
            }
        }

        return null;
    }

    private Vector3 target;

    protected void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = (int) (-Mathf.Floor(transform.position.y * 10) + 100);

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
            ///и перед нами что-то есть
            if (target != Vector3.back)
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
                    ///создаем патрон который летит туда где мы увидели врага
                    Instantiate(bulletPrefab, gameObject.transform.position + moveVector, Quaternion.Euler(0, 0, 0))
                        .GetComponent<Rigidbody2D>().velocity = target; /*new Vector3(
                            hit.gameObject.transform.position.x - transform.position.x,
                            hit.gameObject.transform.position.y - transform.position.y, 0);*/
                }
            }

            target = Vector3.back;
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
                        target = hit.gameObject.transform.position;
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