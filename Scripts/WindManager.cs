using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour {


    public Transform PointA0;
    public Transform PointA1;

    public Transform PointB0;
    public Transform PointB1;

    public Transform PointC0;
    public Transform PointC1;

    public GameObject WindObj;
    public GameObject planeModel;

    Transform target;
    bool GoTravel;
    bool setTarget;
    int currentIndex;
    float speed = 3.0f;

    enum Direction { Right, Left};
    Direction currentDir;
    
    // Use this for initialization
    //0 = A
    //1 = B
    //2 = C


    void Start ()
    {
        currentDir = Direction.Right;
        GoTravel = false;
        setTarget = false;
        StartCoroutine(SetNewDirectionPosition());

	}

    void Update()
    {
        if (GoTravel)
        {
            WindObj.transform.position = Vector3.MoveTowards(WindObj.transform.position, target.transform.position, 15.0f * Time.deltaTime);


            //IF reached destination
            if (Vector3.Distance(WindObj.transform.position, target.transform.position) < 0.01f)
            {
                StartCoroutine(SetNewDirectionPosition());
            }
        }

    }

    Transform SetTarget()
    {
        //GOING RIGHT
        if (currentDir == Direction.Right)
        {
            if (currentIndex == 0)
                return PointA1;
            else if (currentIndex == 1)
                return PointB1;
            else if (currentIndex == 2)
                return PointC1;
            else
                return null;
        }
        else if (currentDir == Direction.Left)
        {
            if (currentIndex == 0)
                return PointA0;
            else if (currentIndex == 1)
                return PointB0;
            else if (currentIndex == 2)
                return PointC0;
            else
                return null;
        }
        else
            return null;
    }

    IEnumerator SetNewDirectionPosition()
    {
        GoTravel = false;

        yield return new WaitForSeconds(5.0f);

        //Get Random Row
        currentIndex = Random.Range(0, 3);

        WindObj.transform.position = SetTarget().position;
        if (currentDir == Direction.Right)
            planeModel.transform.eulerAngles =
                new Vector3(planeModel.transform.eulerAngles.x, 90.0f, planeModel.transform.eulerAngles.z);
        else
            planeModel.transform.eulerAngles =
         new Vector3(planeModel.transform.eulerAngles.x, -90.0f, planeModel.transform.eulerAngles.z);

        //Set Direction
        if (currentDir == Direction.Right)
            currentDir = Direction.Left;
        else
            currentDir = Direction.Right;

        //Go to that direction
        target = SetTarget();

        GoTravel = true;
    }


}
