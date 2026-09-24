using UnityEngine;

public class Collectable_Controller : MonoBehaviour
{
    Rigidbody rb;
    public float moveForce = 350f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude < 4f) rb.AddForce(new Vector3(Random.Range(-3f,3f)*moveForce,0,Random.Range(-3f,3f)*moveForce));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Cull"))
        {
            Destroy(gameObject);
        }       
    }
}
