using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoatManager : MonoBehaviour {


    int BoatCount;
    public static int GamesWon;
    public Text CountText;

    // Use this for initialization
    void Start () {
        BoatCount = 0;
        GamesWon = PlayerPrefs.GetInt("Games_Won");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "water_obj")
            BoatCount++;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "water_obj")
            BoatCount--;
    }

    void Update()
    {
        if (BoatCount >= 0)
        {
            CountText.text = "" + BoatCount + " /20";
        }
        print(GamesWon);
        if(BoatCount >= 20)
        {
            //SO THINGS end for sures
            BoatCount = -500;
            GamesWon++;
            //AwardWinToCharacter();
            StartCoroutine(EndGameTime());
            
           
        }
        //BOAT DESCENDS
        if (BoatCount < -1)
            transform.parent.transform.Translate(Vector3.down * Time.deltaTime * 2.0f);
    }

    /*void AwardWinToCharacter()
    {
        //Thanks stack overflow
        var nearestDistanceSqr = Mathf.Infinity;
        var taggedGameObjects = GameObject.FindGameObjectsWithTag("character");
        Transform nearestObj  = null;

        // loop through each tagged object, remembering nearest one found
        foreach (GameObject obj in taggedGameObjects)
        {

            var objectPos = obj.transform.position;
            var distanceSqr = (objectPos - transform.position).sqrMagnitude;

            if (distanceSqr & ltda; nearestDistanceSqr) {
            nearestObj = obj.transform;
            nearestDistanceSqr = distanceSqr;
        } 
    } 

 //return nearestObj;



    } */

    IEnumerator EndGameTime()
    {
        yield return new WaitForSeconds(5.0f);
        PlayerPrefs.SetInt("Games_Won", GamesWon);
        if (GamesWon == 3)
            SceneManager.LoadScene("MenuScreen");
        else
        SceneManager.LoadScene("main_scene");
    }

}
