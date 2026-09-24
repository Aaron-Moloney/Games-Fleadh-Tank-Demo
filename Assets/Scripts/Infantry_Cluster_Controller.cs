using System.Collections;
using UnityEngine;

public class Infantry_Cluster_Controller : MonoBehaviour
{

    public GameObject playerRef;
    private bool grounded = false;
    public int health = 3;
    private bool isSlowed = false;
    private bool isNotStunned = true;
    public float shockTime = 3.5f;
    bool canAddForce = true;
    public float moveForce = 13;
    public float enemySpeed = 10;
    private float currentSpeed = 10;
    public float enemyMass = 1;
    //opposite than expected
    bool iFrame = true;
    Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        rb = this.GetComponent<Rigidbody>();
        rb.mass = enemyMass;
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > currentSpeed)
        {
            canAddForce = false;
        }
        else
        {
            canAddForce = true;
        }

        if (grounded && isNotStunned && canAddForce)
        {
            rb.AddRelativeForce(moveForce*UnityEngine.Vector3.forward);
        }
        // if(rb.linearVelocity.magnitude > 10)
        // {
        //     rb.linearVelocity = rb.linearVelocity.normalized * 10;
        // }
    }

    void Update()
    {
        if (health <= 1)
        {
            Destroy(gameObject);
        }
        UnityEngine.Vector3 directionToPlayer = playerRef.transform.position - transform.position;
        directionToPlayer.y = 0;
        UnityEngine.Vector3 rotToPlayerV3 = UnityEngine.Vector3.RotateTowards(this.transform.forward, directionToPlayer,1,0);
        transform.rotation = UnityEngine.Quaternion.LookRotation(rotToPlayerV3);

        if (isSlowed)
        {
            currentSpeed = enemySpeed/2f;
        }
        else
        {
            currentSpeed = enemySpeed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }

        if (iFrame)
        {
            iFrame = false;
            if (collision.gameObject.CompareTag("Projectile"))
            {
                health--;
            }
            if (collision.gameObject.CompareTag("Railgun"))
            {
                health = health - 4;
            }
            StartCoroutine(iFrameWait());
        }
        if (collision.gameObject.CompareTag("Cull"))
        {
            health = -999;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        //The iFrame bool stops overlapping hitboxes from dealing damage multiple times to the same enemy,
        // at least I hope it does.
        if (iFrame)
        {
            iFrame = false;
            if (collision.gameObject.CompareTag("Explosion"))
            {
                health--;
                UnityEngine.Vector3 explForce = (this.transform.position - collision.gameObject.transform.position).normalized*600;
                explForce.y = 125;
                rb.AddForce(explForce);
                Debug.Log("Tank health : " + health);
            }
            if (collision.gameObject.CompareTag("PushExpl"))
            {
                Debug.Log("Enemy pushed!");
                UnityEngine.Vector3 explForce = (this.transform.position - playerRef.transform.position).normalized*1750;
                explForce.y = 250;
                rb.AddForce(explForce);
            }
            if (collision.gameObject.CompareTag("Stunner"))
            {
                Debug.Log("Enemy stunned!");
                isNotStunned = false;
                StartCoroutine(stunWait());
            }
            
            StartCoroutine(iFrameWait());
        }
        
        if (collision.gameObject.CompareTag("PlayerHitBox"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Slowing"))
            {
                Debug.Log("Enemy slowed!");
                isSlowed = true;
            }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Slowing")
        {
            isSlowed = false;
        }
        
    }

    IEnumerator stunWait()
    {
        yield return new WaitForSeconds(shockTime);
        isNotStunned = true;
    }
    IEnumerator iFrameWait()
    {
        yield return new WaitForFixedUpdate();
        iFrame = true;
    }
}

