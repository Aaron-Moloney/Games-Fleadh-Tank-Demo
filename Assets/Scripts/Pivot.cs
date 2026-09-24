using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Pivot : MonoBehaviour
{
    public Vector2 moveDir;
    public float yAngle;
    public float zAngle;
    public GameObject bulletHE;
    public GameObject bulletHV;
    public GameObject bulletPush;
    public GameObject bulletStun;
    public GameObject bulletShotgun;
    private int bulletNum = 1;
    private GameObject bulletObj;
    public GameObject muzzle;
    public TextMeshProUGUI textRot;
    public TextMeshProUGUI textEle;
    public TextMeshProUGUI textBullet;
    public float fireForce = 1;
    public float rotateSpeed = 1;
    public GameObject barrelBase;
    public static float yDir;
    String bulletString;

    bool reloaded = true;

    bool paused = false;
    public TextMeshProUGUI myGUI;

    
    public GameObject hull;
    public Rigidbody hullRB;
    // Vector3 addMove = new Vector3(0,0,0);
    float addMove = 0;
    public float moveAmount = 0.08f;
    Vector3 addRot = new Vector3(0,0,0);
    public float rotAmount = 1f;

    public GameObject cameraRef;

    public int ammoHV, ammoPush, ammoStun, ammoSG;
    
    public bool ezStart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (ezStart)
        {
            ammoHV = 100;
            ammoPush = 100;
            ammoStun = 100;
            ammoSG = 100;
        }

        bulletObj = bulletHE;
        bulletString = "";
        hull = GameObject.FindGameObjectWithTag("Player");
        hullRB = hull.GetComponent<Rigidbody>();
        cameraRef = GameObject.FindGameObjectWithTag("CameraBase");
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            Time.timeScale = 0f;
            myGUI.text = "Game paused";
        }
        else
        {
            Time.timeScale = 1f;
            myGUI.text = "";
        }
        if (bulletNum > 5) bulletNum = 1;
        else if (bulletNum < 0) bulletNum = 5;

        if (barrelBase.transform.eulerAngles.z > 95 && barrelBase.transform.eulerAngles.z < 150)
        {
            zAngle = -1f;
        }
        else if (barrelBase.transform.eulerAngles.z > 300)
        {
            zAngle = 1f;
        }
        else
        {
            zAngle = 0;
        }

        barrelBase.transform.Rotate(0,0,moveDir.y*-rotateSpeed,Space.Self);
        barrelBase.transform.Rotate(0,0,zAngle*rotateSpeed,Space.Self);
        transform.Rotate(0,moveDir.x*rotateSpeed, 0,Space.World);

        cameraRef.transform.Rotate(0,moveDir.x*rotateSpeed, 0,Space.Self);
        // cameraRef.transform.position = transform.position;

        textRot.text = "Rotation: " + String.Format("{0:0.00}", this.transform.eulerAngles.y);
        textEle.text = "Elevation: " + String.Format("{0:0.00}", ((this.transform.eulerAngles.z - 90)*-1));
        if (bulletNum == 1)
        {
            bulletString = "Shell: HE Ammo: Infinite";
            bulletObj = bulletHE;
        }
        else if (bulletNum == 2)
        {
            if (ammoHV <= 0)
            {
                bulletNum++;
            }
            else
            {
                bulletString = "Shell: HV Ammo: " + ammoHV;
                bulletObj = bulletHV;
            }
        }
        else if (bulletNum == 3)
        {
            if (ammoPush <= 0)
            {
                bulletNum++;
            }
            else
            {
                bulletString = "Shell: Push Ammo: " + ammoPush;
                bulletObj = bulletPush;
            }
        }
        else if (bulletNum == 4)
        {
            if (ammoStun <= 0)
            {
                bulletNum++;
            }
            else
            {
                bulletString = "Shell: Shock Ammo: " + ammoStun;
                bulletObj = bulletStun;
            }
        }
        else if (bulletNum == 5)
        {
            if (ammoSG <= 0)
            {
                bulletNum++;
            }
            else
            {
                bulletString = "Shell: Shotgun Ammo: " + ammoSG;
                bulletObj = bulletShotgun;  
            }
            
        }
        else
        {
            bulletString = "Shell: HE Ammo: Infinite";
            bulletObj = bulletHE;
        }

        textBullet.text = "" + bulletString;

        yDir = this.transform.eulerAngles.y;
    }

    void FixedUpdate()
    {
        // hull.transform.position -= hull.transform.right * addMove;
        hull.transform.eulerAngles += addRot;
        // Debug.Log(addMove);
        if (hullRB.linearVelocity.magnitude < 13)
        {
            hullRB.AddRelativeForce(addMove*18*UnityEngine.Vector3.left);
        }
        
    }

    void OnFire()
    {
        if (reloaded)
        {
            if (bulletNum == 2) ammoHV--;
            if (bulletNum == 3) ammoPush--;
            if (bulletNum == 4) ammoStun--;
            if (bulletNum == 5) ammoSG--;

            reloaded = false;
            Debug.Log(bulletString);
            GameObject newBullet = Instantiate(bulletObj,muzzle.transform.position,muzzle.transform.rotation);
            newBullet.gameObject.GetComponent<Rigidbody>().AddForce(hullRB.linearVelocity*newBullet.gameObject.GetComponent<Rigidbody>().mass);
            StartCoroutine(reload());
        }
    }

    void OnRightClick()
    {
        
    }



    void OnLook(InputValue movementValue)
    {
        //Debug.Log(movementValue);
        //Debug.Log(movementValue.Get<Vector2>());
        moveDir = movementValue.Get<Vector2>();
    }

    void OnMove(InputValue value)
    {
        // addMove = new Vector3(value.Get<Vector2>().y,0,0);
        addMove = value.Get<Vector2>().y;
        addRot = new Vector3(0,value.Get<Vector2>().x,0);
        addRot *= rotAmount;
    }

    void OnScrollWheel(InputValue input)
    {
        // Debug.Log(input.Get<Vector2>().y);
        if (input.Get<Vector2>().y > 0)
        {
            // if (bulletNum > 4) bulletNum = 1;
            bulletNum++;
        }
        else if (input.Get<Vector2>().y < 0)
        {
            // if (bulletNum < 1) bulletNum = 4;
            bulletNum--;
        }
        // Debug.Log(bulletNum);
    }

    void OnDPad(InputValue input)
    {
        if (input.Get<Vector2>().y > 0)
        {
            // if (bulletNum > 4) bulletNum = 1;
            bulletNum++;
        }
        else if (input.Get<Vector2>().y < 0)
        {
            // if (bulletNum < 1) bulletNum = 4;
            bulletNum--;
        }
    }

    IEnumerator reload()
    {
        yield return new WaitForSeconds(1);
        reloaded = true;
    }
    void OnPauseKey()
    {
        paused  = !paused;
    }

    // void OnTriggerEnter(Collider collision)
    // {
    //     if (collision.CompareTag("Collectable"))
    //     {
    //         Destroy(collision.gameObject);
    //         addAmmo();
    //     }
    // }
    public void addAmmo()
    {
        int randInt = UnityEngine.Random.Range(1,6);
        if (randInt == 2) ammoHV += 10;
        if (randInt == 3) ammoPush += 10;
        if (randInt == 4) ammoStun += 10;
        if (randInt == 5) ammoSG += 10;
    }
}
