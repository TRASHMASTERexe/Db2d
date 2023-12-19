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
    public float spinSpeed = 180f;

    private Vector2 startLocation;
    private Vector2 oldPos;
    private float totalDistance;
    private float distanceToTravel;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * moveSpeed;
        if(totalDistance >= distanceToTravel)
        {
            Destroy(gameObject);
        }

        totalDistance += Vector2.Distance(transform.position, oldPos);
        oldPos = transform.position;

        transform.Rotate(Vector2.up, spinSpeed * Time.deltaTime);


    }

    private void Start()
    {
        startLocation = transform.position;
        oldPos = transform.localPosition;
    }

    public void CalcDistancetoTravel(Vector2 position)
    {
        distanceToTravel = Vector2.Distance(transform.position, position);
    }
}
