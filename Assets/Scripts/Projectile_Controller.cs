using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Projectile_Controller : MonoBehaviour
{
    private float drag = -0.2f;
    public GameObject boom;
    private Rigidbody rb;
    public GameObject audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (audioSource != null)
        {
            Instantiate(audioSource);
        }
        rb = this.GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward*1.5f*2000);
        StartCoroutine(delTimer());
    }

    // Update is called once per frame
    void Update()
    {
        //To show the horizontal speed of the projectile
        //Debug.Log("Speed: " + (rb.linearVelocity.x*Mathf.Cos(Mathf.Atan(rb.linearVelocity.x/rb.linearVelocity.z))));

        Vector3 following = rb.linearVelocity.normalized;
        UnityEngine.Vector3 followingV3 = UnityEngine.Vector3.RotateTowards(this.transform.forward, following,0.1f,0);
        transform.rotation = UnityEngine.Quaternion.LookRotation(followingV3);
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(Wind_Controller.windMult*Mathf.Sin(Mathf.Deg2Rad*Wind_Controller.windDir),0,Wind_Controller.windMult*Mathf.Cos(Mathf.Deg2Rad*Wind_Controller.windDir)));
        rb.AddForce(new Vector3(drag*rb.linearVelocity.x,drag*rb.linearVelocity.y,drag*rb.linearVelocity.z));
    }

    IEnumerator delTimer()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (!coll.gameObject.CompareTag("Projectile"))
        {
            // Debug.Log("Hit");
            Instantiate(boom,transform.position,transform.rotation);
            Destroy(gameObject);
        }
        
    }
}
