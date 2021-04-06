using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movements : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField] private float speed;
    [SerializeField] private float jump;
    [SerializeField] private float gravity;
    [SerializeField] private Animator player;
    [SerializeField] private GameObject Malcolm;
    [SerializeField] private GameObject Rin;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    private bool isGrounded;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Setting") == null || GameObject.FindGameObjectWithTag("Setting").GetComponent<Setting>().GetGender() == false) //Male
        {
            Rin.SetActive(false);
        }
        else
        {
            Malcolm.SetActive(false);
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetButton("Run"))
        { speed = 12; }
        else
        { speed = 7; }

        if (isGrounded && velocity.y < 0)
        { velocity.y = -2f; }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        { velocity.y = Mathf.Sqrt(jump * -2f * gravity); }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        if (x != 0) { player.SetFloat("Speed", x); }
        else if (z != 0) { player.SetFloat("Speed", z); }
    }
}
