using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public bool IsTurtle;
    public bool isPickingUp;

    float travelSpeed = 9.0f;
    float rotateSpeed = 1.0f;

    float PowerThrow = 0.0f;

    protected Rigidbody rb;
    PickUp HoldObj;

   Vector3 originalSpot;


    //INPUTS
    KeyCode Forward;
    KeyCode Back;
    KeyCode Left;
    KeyCode Right;
    public KeyCode PickUpObj;
    public KeyCode ChuckObj;


    // Use this for initialization
    void Start()
    {
        isPickingUp = false;
        rb = GetComponent<Rigidbody>();
        HoldObj = GetComponent<PickUp>();

        originalSpot = transform.position;



        SetPlayerInput();
    }

    void SetPlayerInput()
    {
        //Turtle Character
        if(IsTurtle)
        {
            Forward = KeyCode.W;
            Back = KeyCode.S;
            Left = KeyCode.A;
            Right = KeyCode.D;
            PickUpObj = KeyCode.Q;
            //ChuckObj = KeyCode.R;
        }
        //Frog Charcter
        else
        {
            Forward = KeyCode.UpArrow;
            Back = KeyCode.DownArrow;
            Left = KeyCode.LeftArrow;
            Right = KeyCode.RightArrow;
            PickUpObj = KeyCode.RightControl;
            //ChuckObj = KeyCode.P;
        }
    }

    //Can only enter THROWMODE if picking up an object

    //Handle player input / transition states
    //E = object pickup
    //R = Holddown and throw
    //Q = Enter throwMode
    void Update()
    {

        //Check if fell off the boat
        //print(transform.position.y);
        if (transform.position.y < -10.0f)
        { 
            transform.position = originalSpot;
        }

        Debug.DrawLine(HoldObj.pickUpLoc.transform.position, HoldObj.pickUpLoc.transform.position + HoldObj.pickUpLoc.transform.forward, Color.green);



        //ENTER / EXIT THROW MODE AND HOLDING OBJECT
        //Lookout this thing can be buggy
        
            //Throwing STUFF
            if (Input.GetKeyUp(/*ChuckObj */PickUpObj) && isPickingUp)
            {   
            if (PowerThrow < 1.0f)
            {
                PowerThrow += Time.deltaTime * 0.5f;
            }
            //print(PowerThrow);
            }

        //Relased throw - reset the float
        if (Input.GetKeyUp(PickUpObj) && isPickingUp && HoldObj.canDrop)
        {

            //PROJECTILE CODE
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();

            Rigidbody projRb = HoldObj.objectHolding.gameObject.GetComponent<Rigidbody>();

            //drop object / unparent it
            HoldObj.DropObject();


            //Vector3 throwDir = (HoldObj.pickUpLoc.transform.position + HoldObj.pickUpLoc.transform.forward);
            Vector3 throwDir = HoldObj.transform.forward;
            throwDir.y = throwDir.y + 0.5f;
            throwDir = throwDir.normalized;

            projRb.AddForce(throwDir * 800.0f);

            PowerThrow = 0.0f;
            print("POWER IS 0");
        } 

    }


    // Update is called once per frame
    //character movement
    void FixedUpdate()
    {
        //In Regular movement mode
            Vector3 v3 = new Vector3(0, 0, 0);
            //Going and rotating forward
            if (Input.GetKey(Forward))
            {
                v3 += Vector3.forward;
            }
            if (Input.GetKey(Back))
            {
                v3 += -Vector3.forward;

            }
            if (Input.GetKey(Left))
            {
                v3 += -Vector3.right;
            }
            if (Input.GetKey(Right))
            {
                v3 += Vector3.right;
            }
            //VECTOR MOVEMENT
            rb.MovePosition(transform.position + v3.normalized * Time.fixedDeltaTime * travelSpeed);
        
        //IN Rotating mode
        Quaternion newRot = new Quaternion(0, 0, 0, 0);
        Vector3 newDir = new Vector3(0, 0, 0);
        if (Input.GetKey(Forward))
        {
            Vector3 target = transform.position + Vector3.forward * 2.0f;
            newDir += Vector3.RotateTowards(Vector3.forward, target, rotateSpeed * Time.deltaTime, 0.0f);
            newRot = Quaternion.LookRotation(newDir);
        }

        if (Input.GetKey(Back))
        {
            Vector3 target = transform.position + -Vector3.forward * 2.0f;
            newDir += Vector3.RotateTowards(-Vector3.forward, target, rotateSpeed * Time.deltaTime, 0.0f);
            newRot = Quaternion.LookRotation(newDir);
        }

        if (Input.GetKey(Left))
        {
            Vector3 target = transform.position + -Vector3.right * 2.0f;
            newDir += Vector3.RotateTowards(-Vector3.right, target, rotateSpeed * Time.deltaTime, 0.0f);
            newRot = Quaternion.LookRotation(newDir);
        }

        if (Input.GetKey(Right))
        {
            Vector3 target = transform.position + Vector3.right * 2.0f;
            newDir += Vector3.RotateTowards(Vector3.right, target, rotateSpeed * Time.deltaTime, 0.0f);
            newRot = Quaternion.LookRotation(newDir);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 0.2f);
    }
}
