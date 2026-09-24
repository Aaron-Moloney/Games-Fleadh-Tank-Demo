using System;
using TMPro;
using UnityEngine;

public class Count : MonoBehaviour

{
    public GameObject healthRef;
    public float timeS = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (healthRef != null)
        {
            if (healthRef.GetComponent<Player_Hitbox>().health > 0)
            {
                timeS += Time.deltaTime;
            }
            this.GetComponent<TextMeshProUGUI>().text = "Score: " + String.Format("{0:0}", timeS);
        } 
    }
}
