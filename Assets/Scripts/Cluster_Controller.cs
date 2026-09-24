using UnityEngine;

public class Cluster_Controller : MonoBehaviour
{
    public GameObject infRef;
    Rigidbody myRB;
    public GameObject playerRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        myRB = this.GetComponent<Rigidbody>();
        for (int i = 0; i < Random.Range(12,16); i++)
        {
            float spawnRange = Random.Range(0,12.5f);
            float spawnRot = (Random.Range(0,360))*Mathf.Deg2Rad;
            Vector3 spawnRangeV3 = this.transform.position + new Vector3(spawnRange*Mathf.Sin(spawnRot),0,spawnRange*Mathf.Cos(spawnRot));
            Instantiate(infRef,spawnRangeV3,transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,playerRef.transform.position,.02f);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            Destroy(gameObject);       
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Infantry"))
        {
            Rigidbody rb = coll.GetComponent<Rigidbody>();
            Vector3 forceDir = (transform.position - coll.transform.position)/2;
            forceDir.y = 0;
            rb.AddForce(forceDir);
            myRB.AddForce(-forceDir);
        }
    }
}
