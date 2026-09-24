using UnityEngine;

public class Apply_Force : MonoBehaviour
{
    public GameObject originRef;
    Vector3 originRefLoc;
    Vector3 forceDir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originRefLoc = originRef.transform.position;
        forceDir = (this.transform.position - originRefLoc).normalized*1000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Infantry"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(forceDir);
        }
    }
}
