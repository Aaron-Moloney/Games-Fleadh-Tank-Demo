using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stun_Controller : MonoBehaviour
{
    private float drag = -0.2f;
    public GameObject boom;
    private Rigidbody rb;
    private bool isArmed = false;
    public float armTime = 0.75f;
    public GameObject audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(audioSource);
        rb = this.GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward*1.5f*2000);
        StartCoroutine(delTimer());
        StartCoroutine(armTimer());
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
        yield return new WaitForSeconds(60f);
        Destroy(gameObject);
    }
    IEnumerator armTimer()
    {
        yield return new WaitForSeconds(armTime);
        isArmed = true;
    }

    void OnCollisionEnter()
    {
        if (isArmed)
        {
            Debug.Log("Hit");
            Instantiate(boom,transform.position,transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (isArmed && coll.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Airburst hit");
            Instantiate(boom,transform.position,transform.rotation);
            Destroy(gameObject);
        } 
    }

    void OnTriggerStay(Collider coll)
    {
        if (isArmed && coll.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Airburst hit");
            Instantiate(boom,transform.position,transform.rotation);
            Destroy(gameObject);
        } 
    }
}
