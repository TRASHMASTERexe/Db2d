using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : Interactable
{

    [SerializeField] private Transform Location1;
    [SerializeField] private Transform Location2;

    private PlayerMovement player;
    public override void Interact(InteractableData data)
    {
        if(data.InteractingObject.CompareTag("Killer"))
        {
            player = data.InteractingObject.GetComponent<PlayerMovement>();
            player.movementLocked = true;
            player.rotationLocked = true;
            //lock killer movement and rotation
            //move killer across window
            //unlock killer movement and rotation
        }
        else if (data.InteractingObject.CompareTag("Survivor"))
        {
            //check survivor speed and toerh factors
            //lock survivor movement and rotation
            //move suvivor across window at appropriate speed
            //unlock survivor movement and rotation
        }
    }

    private void Update()
    {
        //move player
    }
}
