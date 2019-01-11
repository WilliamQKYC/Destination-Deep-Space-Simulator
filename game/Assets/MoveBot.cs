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
    private bool goingForward = false;


    // Start is called before the first frame update
    void Start()
    {
        velocityVector = new Vector3();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        hMove = Input.GetAxisRaw("Horizontal");
        vMove = Input.GetAxisRaw("Vertical");
        angle = transform.rotation.eulerAngles.z;
    }


    // currently only doing WestCoast Drive

    private void Move() // uses vertical input
    {
        if (vMove>0) {
            goingForward = true;
            if (velocity<velocitCap) {
                velocity += acceleration;
                velocity = Clamp(velocity, 0, velocitCap);
            }
            velocityVector = new Vector3(velocity*Mathf.Cos(toRadians(angle)), velocity*Mathf.Sin(toRadians(angle)), 0);

        } else if (vMove<0) {
            if (goingForward) {
                velocity -= slowdown;
                velocity = Clamp(velocity, 0f, velocitCap);
                if (velocity == 0) goingForward = false;
            } else {
                velocity += acceleration;
                velocity = Clamp(velocity, 0f, velocitCap);
                velocityVector = new Vector3(-velocity*Mathf.Cos(toRadians(angle)), -velocity*Mathf.Sin(toRadians(angle)), 0);
            }

        } else {
            velocity -= slowdown;
            velocity = Clamp(velocity, 0, velocitCap);
            if (velocity == 0) goingForward = false;
            velocityVector = new Vector3(velocity * Mathf.Cos(toRadians(angle)), velocity * Mathf.Sin(toRadians(angle)), 0);
        }

        rb.velocity = new Vector2(velocityVector.x, velocityVector.y);

    }

    private float Clamp(float num, float min, float max)
    {
        if (num > max) return max;
        else if (num < min) return min;
        return num;
    }

    private float toRadians(float num) {
        return num*Mathf.PI/180;
    }

    private void Rotate() // uses horizontal input
    {
        if (hMove>0)
        {
            // Vector3.forward = (0, 0, 1)
            transform.Rotate(Vector3.forward * -deltaAngle);
        }
        else if (hMove<0)
        {
            transform.Rotate(Vector3.forward * deltaAngle);
        }
    }

    // Called less frequently than Update()
    private void FixedUpdate()
    {
        Rotate();
        Move();
    }
}
