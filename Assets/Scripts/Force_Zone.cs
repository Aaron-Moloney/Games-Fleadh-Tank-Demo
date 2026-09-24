using Unity.VisualScripting;
using UnityEngine;

public class Force_Zone : MonoBehaviour
{
    GameObject obj;
    public float repForce = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!(obj == null))
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.AddForce((rb.transform.position - this.transform.position).normalized*50*repForce);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            // Debug.Log("Test");
            obj = collision.gameObject;
        }
        
    }
    void OnTriggerExit(Collider collision)
        {
        if (collision.CompareTag("Projectile"))
        {
            // Debug.Log("Test 2");
            obj = null;
        }
    }
}
