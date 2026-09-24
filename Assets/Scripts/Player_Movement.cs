using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Player_Movement : MonoBehaviour
{
    public float moveAmount = 1;
    public GameObject turret;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turret = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMove(InputValue value)
    {
        Vector3 addMove = new Vector3(value.Get<Vector2>().x,0,value.Get<Vector2>().y);
        addMove *= moveAmount;
        transform.position += addMove;
        Debug.Log(addMove);
    }
}
