using Unity.VisualScripting;
using UnityEngine;

public class Collision_Script : MonoBehaviour
{

    public GameObject boom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter()
    {
        Debug.Log("Hit");
        Instantiate(boom,transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
