using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBot : MonoBehaviour {

    [SerializeField] private float deltaAngle = 0.1f;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float slowdown = 1.5f; // I refuse to call it "deceleration"
    [SerializeField] private float velocitCap = 3f;



    private Rigidbody2D rb;

    private float hMove;
    private float vMove;
    private Vector3 velocityVector;
    private float velocity;
    private float angle = 0f;


    // Start is called before the first frame update
    void Start()
    {
        velocityVector = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        hMove = Input.GetAxisRaw("Horizontal");
        vMove = Input.GetAxisRaw("Vertical");
    }

    // currently only doing WestCoast Drive

    private void Move() // uses vertical input
    {
        if (velocity<velocitCap)
        {
            velocity += acceleration;
            velocity = Clamp(velocity, velocitCap);
        }


    }

    private float Clamp(float num, float max)
    {
        if (num > max) return max;
        return num;
    }

    private void Rotate() // uses horizontal input
    {
        if (hMove>0)
        {
            transform.Rotate(Vector3.forward * -deltaAngle);
        }
        else if (hMove<0)
        {
            transform.Rotate(Vector3.forward * deltaAngle);
        }
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }
}
