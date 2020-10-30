using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movespeed = 7;
    public float smoothMoveTime = .1f;
    public float turnSpeed = 8;

    float angle;
    float smoothInputMagnitude;
    float smoothMoveVelocity;
    Vector3 velocity;

    Rigidbody rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        float inputMagnitude = inputDirection.magnitude;
        
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);
        

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed*inputMagnitude);
        velocity = transform.forward * movespeed * smoothInputMagnitude;
    }

    private void FixedUpdate()
    {
        rbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        rbody.MovePosition(rbody.position + velocity * Time.deltaTime);
    }
}
