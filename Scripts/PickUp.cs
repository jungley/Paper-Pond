using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Attached to the character
//Must attach obj_loc to pickUpLoc public transform variable
public class PickUp : MonoBehaviour {

    CharacterMovement charMovement;
    public Transform pickUpLoc;
    public Collider objectHolding;

    public bool canDrop;

	// Use this for initialization
	void Start (){
        charMovement = GetComponent<CharacterMovement>();
        canDrop = false;
            
	}


    void OnTriggerStay(Collider other)
    {
        //if in range of object and press E key and not already picking something up
        if(other.tag == "water_obj" && Input.GetKeyUp(charMovement.PickUpObj) && objectHolding == null)
        {
            objectHolding = other;

            //mark Script as picked up
            charMovement.isPickingUp = true;

            //Zero out rotation
            objectHolding.transform.rotation = new Quaternion(0, 0, 0, 0);

            //disable box Trigger in water object
            foreach (Collider c in other.gameObject.GetComponents<Collider>())
            {
                if (c.GetType() == typeof(BoxCollider))
                    c.isTrigger = true;
            }

            //disable rigidbody gravity
            objectHolding.gameObject.GetComponent<Rigidbody>().useGravity = false;
            objectHolding.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            //go to location of holder
            objectHolding.transform.position = pickUpLoc.position;

            //make it a child of character
            objectHolding.transform.parent = pickUpLoc;
            StartCoroutine(waitLockTime());
        }
    }

    //Because Update has some weird behavior
    IEnumerator waitLockTime()
    {
        yield return new WaitForSeconds(0.01f);
        canDrop = true;
    }

    //Unparents
    //Enables physics
    public void DropObject()
    {
        //Re-enable everything 
        //disable box Trigger in water object
        foreach (Collider c in objectHolding.GetComponents<Collider>())
        {
            if (c.GetType() == typeof(BoxCollider))
                c.isTrigger = false;
        }

        //disable rigidbody gravity
        objectHolding.gameObject.GetComponent<Rigidbody>().useGravity = true;
        objectHolding.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        //remove waterObjectParent
        objectHolding.transform.parent = null;

        //No longer holding object
        objectHolding = null;
        charMovement.isPickingUp = false;
        canDrop = false;
    } 

    /*private void Update()
    {
        //DROP OBJECT
        if(charMovement.isPickingUp && Input.GetKeyUp(charMovement.PickUpObj) && canDrop)
        {
            DropObject();
        } 
    } */
}
