using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private FieldOfView[] fovs;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float moveSpeed = 10;

    private float adjustedMoveSpeed;
    Vector3 previousMousePosition;

    public bool movementLocked = false;
    public bool rotationLocked = false;


    private void Start()
    {
        adjustedMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if(!movementLocked)
        {
            MovePlayer();
        }
        
        if(!rotationLocked)
        {
            Rotateplayer();
        }

        UpdateFov();
    }

    private void MovePlayer()
    {
        Vector2 PlayerInput;
        PlayerInput.x = Input.GetAxisRaw("Horizontal");
        PlayerInput.y = Input.GetAxisRaw("Vertical");

        PlayerInput.Normalize();

        rb.velocity = PlayerInput * adjustedMoveSpeed;
    }

    private void UpdateFov()
    {
        foreach (FieldOfView f in fovs)
        {
            f.setAimDirection(transform.up);
            f.SetOrigin(transform.position);
        }
    }

    private void Rotateplayer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition != previousMousePosition)
        {
            Vector3 dir = mousePosition - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            rb.rotation = angle - 90;

            previousMousePosition = mousePosition;
        }
    }

    public void SetFov(FieldOfView[] fovs)
    {
        this.fovs = fovs;
    }

    public void setMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        this.adjustedMoveSpeed = moveSpeed;
    }

    public void adjustMoveSpeed(float moveSpeed)
    {
        this.adjustedMoveSpeed = moveSpeed;
    }

    public void resetMoveSpeed()
    {
        this.adjustedMoveSpeed = moveSpeed;
    }
}