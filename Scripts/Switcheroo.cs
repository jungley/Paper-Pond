using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcheroo : MonoBehaviour
{

    public GameObject turtle;
    public GameObject frog;

    Vector3 turtleTarget;
    Vector3 frogTarget;

    bool isSwitching;
    bool isSpinning;

    void Start()
    {
        isSwitching = false;
        isSpinning = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "character" && !isSwitching)
        {
            print("HIT A CHAR");
            StartCoroutine(SwitchCharacters());
            isSwitching = true;
        }
    }

    IEnumerator SwitchCharacters()
    {
        print("in coroutine");
        //freeze the character movement
        turtle.GetComponent<CharacterMovement>().enabled = false ;
        frog.GetComponent<CharacterMovement>().enabled = false;


        //PLAY CRUNCH SOUNDS
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();

        //PlayAnimation for both characeters
        //Animator Play spin turtle
        //Animator play spin frog

        isSpinning = true;
        yield return new WaitForSeconds(2.5f);
        isSpinning = false;




        //Switch Characters
        Vector3 tmp;
        tmp = frog.transform.position;
        frog.transform.position = turtle.transform.position;
        turtle.transform.position = tmp; 


        //Vector3 turtleTarget = frog.transform.position;
        //Vector3 frogTarget = turtle.transform.position;


        //UNfreeze character movement
        turtle.GetComponent<CharacterMovement>().enabled = true;
        frog.GetComponent<CharacterMovement>().enabled = true;

        print("ReachedEnd of coroutine");
        isSwitching = false;
    }

    void Update()
    {
        if (isSpinning)
        {
            print("GOT HERE");
            turtle.transform.Rotate(Vector3.up * Time.deltaTime * 1100.0f);
            frog.transform.Rotate(Vector3.up * Time.deltaTime * 1100.0f);
        }
    }


}
