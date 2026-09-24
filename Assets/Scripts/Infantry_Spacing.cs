using UnityEngine;

public class Infantry_Spacing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Infantry"))
        {
            Rigidbody rb = coll.GetComponent<Rigidbody>();
            Vector3 forceV3 = (coll.transform.position - transform.position)/1f;
            forceV3.y = 0;
            rb.AddForce(forceV3);
        }
    }
}
