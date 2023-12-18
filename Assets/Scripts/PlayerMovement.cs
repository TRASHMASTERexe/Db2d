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
    private float moveSpeed = 4;


    void Update()
    {
        Vector2 PlayerInput; 
        PlayerInput.x = Input.GetAxisRaw("Horizontal");
        PlayerInput.y = Input.GetAxisRaw("Vertical");

        PlayerInput.Normalize();

        rb.velocity = PlayerInput * moveSpeed;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mousePosition - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        rb.rotation = angle - 90;
        foreach(FieldOfView f in fovs)
        {
            f.setAimDirection(transform.up);
            f.SetOrigin(transform.position);
        }
    }

    public void SetFov(FieldOfView[] fovs)
    {
        this.fovs = fovs;
    }
}