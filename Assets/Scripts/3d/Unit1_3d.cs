using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

using Random = System.Random;
public class Unit1_3d : MonoBehaviour
{
    public int cost;
    [SerializeField] public int health = 1;
    public int damage = 5;
    public bool isOurTeam = true;
    public float speed = 10.0f;
    private Vector3 moveVector;
    private Rigidbody rb;

    private AudioSource source;

    public GameObject HP;

    private Random rnd = new Random();
    private int rand;
    protected void Start()
    {
        rand = rnd.Next(-2,3);
        gameObject.layer = isOurTeam ? 8 : 9;
        GetComponent<Animator>().SetInteger("HP", health);

        source = GetComponent<AudioSource>();
        moveVector = isOurTeam
            ? Vector3.left
            : Vector3.right;
        moveVector = moveVector.normalized;
        rb = gameObject.GetComponents<Rigidbody>()[0];

        rb.velocity = moveVector * speed;
    }

    public bool isMain = false;

    public void Damage(int dmg)
    {
        health -= dmg;
        GetComponent<Animator>().SetInteger("HP", health);
        if (HP != null)
            HP.GetComponent<Image>().fillAmount = health / 100.0f;
    }

    public AudioClip shootSound;

    public float maxDistance = 1.0f;
    public GameObject bulletPrefab;
    [SerializeField] private bool isMeele = false;

    public Vector3 GetMVector()
    {
        return moveVector;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Area"))
        {
            if (other.gameObject.transform.parent.GetComponent<Unit1_3d>().isOurTeam ^ isOurTeam)
            {
                moveVector = new Vector3(
                    other.gameObject.transform.parent.transform.position.x - gameObject.transform.position.x, 
                    0,
                    other.gameObject.transform.parent.transform.position.z - gameObject.transform.position.z);

                moveVector = moveVector.normalized;
            }
        }
    }

    Collider IsHit(float d)
    {
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(gameObject.transform.position ,moveVector + new Vector3(0, 0, d), out hit, maxDistance, gameObject.layer == 8 
                ? LayerMask.GetMask("Team2")
                : LayerMask.GetMask("Team1"));
        if (hit.collider != null)
        {
            if (Mathf.Abs((hit.collider.gameObject.transform.position.x - transform.position.x) * (hit.collider.gameObject.transform.position.x - transform.position.x) +
                (hit.collider.gameObject.transform.position.z - transform.position.z) * (hit.collider.gameObject.transform.position.z - transform.position.z))
                < maxDistance * maxDistance)
            {
                if (hit.collider.gameObject.CompareTag("Actor"))
                {
                    if (hit.collider.gameObject.GetComponent<Unit1_3d>().isOurTeam ^ isOurTeam)
                    {
                        return hit.collider;
                    }
                }
            }
        }

        return null;
    }

    protected void Update()
    {
        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("End"))//health <= 0) ///Проверка на смерть
        {
            Destroy(gameObject);
            if (isMain)
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
	    return;
        }

        GetComponent<SpriteRenderer>().sortingOrder = rand + (int) (-Mathf.Floor(transform.position.z * 10) + 100);

        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Run")) //wait <= 0)
        {
            rb.velocity = moveVector * speed;
        }

        ///смотрим вперед
        Collider hit = IsHit(0);
        /// если перед нами никого нет смотрим вверх
        if (hit == null)
            hit = IsHit(0.5f);
        /// если перед нами и сверху никого нет смотрим вних
        if (hit == null)
            hit = IsHit(-0.5f);
        if (hit == null)
            hit = IsHit(1);
        /// если перед нами и сверху никого нет смотрим вних
        if (hit == null)
            hit = IsHit(-1);

        ///если анимация атаки закончилась
//        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Finish Shoot"))
        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Shoot") && GetComponent<Animator>().IsInTransition(0))
        {
            ///при атаке играем звук
            source.PlayOneShot(shootSound);

            ///если это рукопашный юнит
            if (isMeele)
            {
                if (hit != null)
                {
                    ///не из нашей команды
                    if (isOurTeam ^ hit.gameObject.GetComponent<Unit1_3d>().isOurTeam)
                    {
                        ///наносим ему урон
                        hit.gameObject.GetComponent<Unit1_3d>().Damage(damage);
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
                                Quaternion.Euler(45,
                                    Mathf.Atan2(hit.gameObject.transform.position.z - transform.position.z,
                                        hit.gameObject.transform.position.x - transform.position.x) * Mathf.Rad2Deg,0))
                            .GetComponent<Rigidbody>().velocity = new Vector3(
                                                                            hit.gameObject.transform
                                                                                .position
                                                                                .x - transform.position.x,
                                                                            0,
                                                                            hit.gameObject.transform
                                                                                .position
                                                                                .z - transform.position.z)
                                                                        .normalized * bulletPrefab
                                                                        .GetComponent<Bullet_3d>().Speed;
                }
            }

//            Debug.Log(Animator.GetNextAnimatorClipInfo(0)[0].clip.name);
//            gameObject.GetComponent<Animator>().Play("AfterShoot");
            gameObject.GetComponent<Animator>().Play("Finish Shoot");
        }

        ///если нигде никого нет идем дальше
        if (hit == null) return;

        ///если мы видим что то и оно Актор
        if (hit.gameObject.CompareTag("Actor"))
        {
            ///если оно не из нашей команды
            if (hit.gameObject.GetComponent<Unit1_3d>().isOurTeam ^ isOurTeam)
            {
                ///и в нашей зоне действия
                if (Mathf.Abs((gameObject.transform.position.x - hit.gameObject.transform.position.x)*(gameObject.transform.position.x - hit.gameObject.transform.position.x)
                              - (gameObject.transform.position.z - hit.gameObject.transform.position.z)*(gameObject.transform.position.z - hit.gameObject.transform.position.z)) 
                    < maxDistance * maxDistance)
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