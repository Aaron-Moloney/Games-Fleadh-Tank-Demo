using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Shotgun_Base : MonoBehaviour
{
    public GameObject pellet;
    quaternion baseRot;
    public GameObject spawnRef;
    public bool repeaterToggle = false;
    public GameObject audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(audioSource);
        StartCoroutine(delTimer());
        fire();
        if (repeaterToggle) StartCoroutine(shooter());
        else Destroy(gameObject);
    }

    void fire()
    {
        //Rotates a spawn area around the origin, checks if the area is free and spawns a projectile if so.
        //Repeats until all 7 pellets are spawned
        baseRot = transform.rotation;
        for (int i = 0; i < 7; i++)
        {
            bool notSpawned  = true;
            do
            {
                for (int h = 0; h < 1000; h++)
                {
                    float rand1 = UnityEngine.Random.Range(-2f,2f);
                    float rand2 = UnityEngine.Random.Range(-2f,2f);
                    Vector3 curRot = transform.eulerAngles;
                    transform.eulerAngles = curRot + (new Vector3(rand1,rand2,0));
                    if (!Physics.CheckSphere(spawnRef.transform.position,0.15f,7))
                    {
                        notSpawned = false;
                        Instantiate(pellet,spawnRef.transform.position, spawnRef.transform.rotation);
                        transform.rotation = baseRot;
                        break;
                    }
                    if (h == 999)
                    {
                        notSpawned = false;
                    }
                }
                
            } while (notSpawned); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator shooter()
    {
        yield return new WaitForSeconds(1f);
        fire();
        StartCoroutine(shooter());
    }

    IEnumerator delTimer()
    {
        yield return new WaitForNextFrameUnit();
        Destroy(gameObject);
    }
}
