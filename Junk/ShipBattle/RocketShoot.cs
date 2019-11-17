using UnityEngine;

public class RocketShoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public GameObject rocket;
    public float Speed = 10;
    public void Fire()
    {
        var go = GameObject.Find("AR Camera");
        Vector3 pos = new Vector3(go.transform.position.x,
            go.transform.position.y - 1,
            go.transform.position.z);
        var torp = Instantiate(rocket, pos, Quaternion.Euler(go.transform.rotation.x + 90,
                                                    go.transform.rotation.y,
                                                    go.transform.rotation.z)
            );
        torp.transform.rotation = go.transform.rotation;    
        torp.GetComponent<Rigidbody>().velocity = GameObject.Find("AR Camera").transform.forward * Speed;
    }


}