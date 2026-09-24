using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnClick()
    {
        //Debug.Log("Fire");
    }

    void OnMove(InputValue movementValue)
    {
        //Debug.Log(movementValue);
        //Debug.Log(movementValue.Get<Vector2>());
    }
}
