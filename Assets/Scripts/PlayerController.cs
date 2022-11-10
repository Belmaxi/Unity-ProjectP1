using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    [SerializeField] private float horsePower = 2000f;
    [SerializeField] private float turnSpeed = 45f;
    public Rigidbody playerRb;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] float speed;
    [SerializeField] float rpm;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }
    private void Update()
    {
        speed = Mathf.Round(playerRb.velocity.magnitude * 2.237f);
        speedometerText.SetText("Speed: " + speed + " mph");
        rpm = Mathf.Round((speed % 30) * 40);
        rpmText.SetText("RPM: " + rpm);
    }
    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (IsOnGround())
        {
            transform.Rotate(Vector3.up, turnSpeed * horizontal * Time.deltaTime);
            //transform.Translate(Vector3.forward * vertical * horsePower * Time.deltaTime);
            playerRb.AddRelativeForce(Vector3.forward * vertical * horsePower);
        }
    }
    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }
        if (wheelsOnGround == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
