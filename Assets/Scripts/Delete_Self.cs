using System.Collections;
using UnityEngine;

public class Delete_Self : MonoBehaviour
{
    private bool check = false;
    public GameObject audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(audioSource);
        // StartCoroutine(delTimer());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (check)
        {
            Destroy(gameObject);
        }
        check = true;
    }

    // IEnumerator delTimer()
    // {
    //     yield return new WaitForSeconds(0.1f);
    //     Destroy(gameObject);
    // }
}
