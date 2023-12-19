using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatchet : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    public float moveSpeed = 5f;

    [SerializeField]
    private float spinSpeed = 360f;

    private Vector2 startLocation;
    private Vector2 oldPos;
    private float totalDistance;
    private float distanceToTravel;
    private Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        if(totalDistance >= distanceToTravel)
        {
            Destroy(gameObject);
        }

        rb.velocity = direction * moveSpeed;
        totalDistance += Vector2.Distance(transform.position, oldPos);
        oldPos = transform.position;

        float rotationAmount = spinSpeed * Time.deltaTime;
        transform.Rotate(0f, 0f, -rotationAmount, Space.Self);


    }

    private void Start()
    {
        startLocation = transform.position;
        oldPos = transform.localPosition;
        direction = transform.up;
    }

    public void CalcDistancetoTravel(Vector2 position)
    {
        distanceToTravel = Vector2.Distance(transform.position, position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Objects"))
        {
            //make hit sound
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Survivor"))
        {
            //hit player
        }
    }
}
