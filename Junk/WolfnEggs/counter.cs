using UnityEngine;
using UnityEngine.UI;

public class counter : MonoBehaviour
{
    public GameObject text;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private int cnt = 0;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject.transform.parent.transform.parent.gameObject);
        cnt++;
        text.GetComponent<Text>().text = cnt.ToString();
    }

    // Update is called once per frame
   
}
