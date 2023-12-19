using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HuntressPower : KillerPower
{

    [Header("References")]
    [SerializeField]
    private GameObject hatchetPrefab;
    [SerializeField]
    private GameObject distanceVisual;
    [SerializeField]
    private PlayerMovement playerMovement;

    [Header("Stats")]
    [SerializeField]
    private int hatchets = 5;
    [SerializeField]
    private int hatchetSpeed = 20;
    [SerializeField]
    private float distanceAimPerSecond = 6;
    [SerializeField]
    private float heldMoveSpeed = 5;
    [SerializeField]
    private float maxHoldTime = 2;
    [SerializeField]
    private float minHoldTime = 0.5f;
    [SerializeField] 
    private float baseDistance = 5f;

    private GameObject currentVisual;
    private bool isThrowing;
    private float timeHeld = 0;
    
    

    public override void ActiveAbility()
    {

        //should probably add some kind of cooldown/ may not be needed if distance is based on time held
        if(Input.GetMouseButton(1))
        {
            timeHeld += Time.deltaTime;

            isThrowing = true;
            if(currentVisual == null)
            {
                currentVisual = Instantiate(distanceVisual, transform);
                playerMovement.adjustMoveSpeed(heldMoveSpeed);
            }
            if(timeHeld <= maxHoldTime)
            {
                //scale visual
                currentVisual.transform.localScale = new Vector2(1, distanceAimPerSecond * timeHeld + baseDistance);
            }
        }
        else if (isThrowing)
        {
            if(timeHeld < minHoldTime)
            {
                timeHeld += Time.deltaTime;
                currentVisual.transform.localScale = new Vector2(1, distanceAimPerSecond * timeHeld + baseDistance);
            }
            else
            {
                isThrowing = false;
                playerMovement.resetMoveSpeed();
                hatchets--;
                timeHeld = 0;

                Hatchet hatchet = Instantiate(hatchetPrefab, transform.position, transform.rotation).GetComponent<Hatchet>();
                float offset = currentVisual.GetComponent<Collider2D>().bounds.size.magnitude;
                Vector3 direction = transform.TransformDirection(Vector3.up).normalized;
                hatchet.CalcDistancetoTravel(transform.position + direction * -offset);
                hatchet.moveSpeed = hatchetSpeed;
                Destroy(currentVisual);
            }
            
        }
        //if press m2 & hatchets > 0
            //set bool for m2 held
            //reduce movement speed
            //begin timer for velocity
            //reflect velocity timer on visual
        //else if bool is true for m2
            //spawn prefab at transform.position
            //apply velocity to prefab.forward
            //reduce hatchet count
            //set bool false
            //reset and hide visual
            
    }

    private void ThrowHatchet() { 
        
    }

    public override void StaticAbility()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        //set num hatchets based on add-ons and such
    }

    private void Update()
    {
        ActiveAbility();
    }
}
