using UnityEngine;

public class Wind_Controller : MonoBehaviour
{
    public float windStr = 1;
    public static float windDir = 0;
    public static float windMult = 20;
    public ParticleSystem pr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        windMult = windMult*windStr;
        windDir = Random.Range(0,360);
        //windMult = Random.Range(0,2);
        this.transform.eulerAngles = new Vector3(0,0,windDir+90);
        pr.transform.eulerAngles = new Vector3(0,windDir-90,0);
    }

    // Update is called once per frame
    void Update()
    {
        //windMult = windMult*windStr;
    }
}
