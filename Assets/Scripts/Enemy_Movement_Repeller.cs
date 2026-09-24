using System.Collections;
// using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Movement_Repeller : MonoBehaviour
{

    public GameObject playerRef;
    private bool grounded = false;
    public int health = 3;
    private bool isSlowed = false;
    private bool isNotStunned = true;
    public float shockTime = 3.5f;
    bool canAddForce = true;
    Rigidbody rb;
    public GameObject zoneRef;
    GameObject newZone;
    public float moveForce = 13;
    public float enemySpeed = 10;
    private float currentSpeed = 10;
    public float enemyMass = 1;
    bool iFrame = true;
    GameObject scoreBoard;
    public float scoreMult = 1;
    public GameObject prize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newZone = Instantiate(zoneRef, transform.position - new UnityEngine.Vector3(0,-1,0), transform.rotation);
        rb = this.GetComponent<Rigidbody>();
        rb.mass = enemyMass;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        scoreBoard = GameObject.FindGameObjectWithTag("ScoreBoard");
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
            scoreBoard.GetComponent<Count>().timeS += scoreMult;
            if (Random.Range(0,20) == 1 ) Instantiate(prize,new Vector3(transform.position.x,20f,transform.position.z),transform.rotation);
            Destroy(newZone);
            Destroy(gameObject);
        }
        UnityEngine.Vector3 directionToPlayer = playerRef.transform.position - transform.position;
        directionToPlayer.y = 0;
        UnityEngine.Vector3 rotToPlayerV3 = UnityEngine.Vector3.RotateTowards(this.transform.forward, directionToPlayer,1,0);
        transform.rotation = UnityEngine.Quaternion.LookRotation(rotToPlayerV3);

        if (isSlowed)
        {
            currentSpeed = enemySpeed/3f;
        }
        else
        {
            currentSpeed = enemySpeed;
        }
        newZone.transform.position = transform.position - new UnityEngine.Vector3(0,0.5f,0);
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
        if (iFrame)
        {
            iFrame = false;
            if (collision.gameObject.CompareTag("Explosion"))
            {
                health--;
                UnityEngine.Vector3 explForce = (this.transform.position - collision.gameObject.transform.position).normalized*600;
                explForce.y = 125;
                rb.AddForce(explForce);
            }
            if (collision.gameObject.CompareTag("PushExpl"))
            {
                Debug.Log("Enemy pushed!");
                Vector3 explForce = (this.transform.position - playerRef.transform.position).normalized*1850;
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
            Destroy(newZone);
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
