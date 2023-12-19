using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HuntressPower : KillerPower
{

    [Header("References")]
    [SerializeField]
    private GameObject HatchetPrefab;
    [SerializeField]
    private GameObject DistanceVisual;

    [Header("Stats")]
    [SerializeField]
    private int Hatchets = 5;
    [SerializeField]
    private int HatchetSpeed = 10;
    [SerializeField]
    private float MaxHatchetDistance;
    [SerializeField]
    private float distanceAimPerSecond = 4;

    private GameObject currentVisual;
    private bool isThrowing;
    private float timeHeld = 0;
    private float maxHoldTime = 3;
    private float minHoldTime = 0.5f;
    

    public override void ActiveAbility()
    {

        //should probably add some kind of cooldown/ may not be needed if distance is based on time held
        if(Input.GetMouseButton(1))
        {
            timeHeld += Time.deltaTime;

            isThrowing = true;
            if(currentVisual == null)
            {
                currentVisual = Instantiate(DistanceVisual, transform);
            }
            if(timeHeld <= maxHoldTime)
            {
                //scale visual
                currentVisual.transform.localScale = new Vector2(1, distanceAimPerSecond * timeHeld);
            }
        }
        else if (isThrowing)
        {
            if(timeHeld < minHoldTime)
            {
                timeHeld += Time.deltaTime;
                currentVisual.transform.localScale = new Vector2(1, distanceAimPerSecond * timeHeld);
            }
            else
            {
                isThrowing = false;
                
                timeHeld = 0;
                Hatchet hatchet = Instantiate(HatchetPrefab, transform.position, transform.rotation).GetComponent<Hatchet>();
                float offset = currentVisual.GetComponent<Collider2D>().bounds.size.magnitude;
                Vector3 direction = transform.TransformDirection(Vector3.up).normalized;
                hatchet.CalcDistancetoTravel(transform.position + direction * -offset);
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
