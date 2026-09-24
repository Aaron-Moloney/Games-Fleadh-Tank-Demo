using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Demo_Spawner : MonoBehaviour
{
    public GameObject shield;
    public GameObject regular;
    public GameObject tank;
    public GameObject speed;
    public GameObject inf;
    public float topEnd = 5;
    public GameObject playerRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(spawn());
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (topEnd > 0.75f)
        {
            topEnd -= Time.deltaTime/10;
        }
        //To view longest possible time between spawns
        //Debug.Log(topEnd);
    }

    void FixedUpdate()
    {
        transform.position = playerRef.transform.position;
    }

    IEnumerator spawn()
    {
        bool notSpawned = false;

        yield return new WaitForSeconds(Random.Range(0.2f,topEnd));
        int choice = Random.Range(0,16);

        do
        {
            int spawnRange = Random.Range(115,175);
            float spawnRot = (Random.Range(0,360))*Mathf.Deg2Rad;
            Vector3 spawnRangeV3 = this.transform.position + new Vector3(spawnRange*Mathf.Sin(spawnRot),0,spawnRange*Mathf.Cos(spawnRot));
            spawnRangeV3.y = 2;
            if (!Physics.CheckSphere(spawnRangeV3,7f,1))
            {
                if (choice <=8)
                {
                    Instantiate(regular,spawnRangeV3,transform.rotation);
                }
                else if (choice <=10)
                {
                    Instantiate(speed,spawnRangeV3,transform.rotation);
                }
                else if (choice <=12)
                {
                    Instantiate(shield,spawnRangeV3,transform.rotation);
                }
                else if (choice <=14)
                {
                    Instantiate(inf,spawnRangeV3,transform.rotation);
                }
                else
                {
                    Instantiate(tank,spawnRangeV3,transform.rotation);
                }
                notSpawned = false;
            }
        } while (notSpawned);
        StartCoroutine(spawn());
    }
}
