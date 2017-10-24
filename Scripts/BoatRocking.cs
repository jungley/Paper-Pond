using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatRocking : MonoBehaviour {

    Quaternion currentRot;


    float Timer;
    bool goingRight;
    bool firstpass;
    float limit;
	// Use this for initialization
	void Start () {
        currentRot = transform.rotation;
        Timer = 0.0f;
        goingRight = false;
        firstpass = true;
        limit = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {

        if (goingRight)
            transform.Rotate(Vector3.forward * Time.deltaTime * 1.0f);
        else
            transform.Rotate(-Vector3.forward * Time.deltaTime * 1.0f);
          Timer += Time.deltaTime;
            if(Timer > limit)
            {
                if(firstpass)
                {
                limit = limit * 2;
                firstpass = false;
                }
                Timer = 0.0f;
                goingRight = !goingRight;
            }
        }
        
        

	}
