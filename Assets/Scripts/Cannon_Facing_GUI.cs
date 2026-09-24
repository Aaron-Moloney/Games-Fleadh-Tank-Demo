using UnityEngine;

public class Cannon_Facing_GUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Pivot.yDir);
        this.transform.eulerAngles = new Vector3(0,0,Pivot.yDir);
    }
}
