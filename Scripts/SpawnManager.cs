using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public List<Transform> items;
    public GameObject waterObject;

    public int length;
    bool updateLock;


	// Use this for initialization
	void Start ()
    {
        updateLock = true;
        items = new List<Transform>();
        foreach (Transform child in transform)
        {
            items.Add(child);
        }

        length = items.Count;

        StartCoroutine(SpawnWater());
	}
	
    IEnumerator SpawnWater()
    {
        while (true)
        {
            //spawn in 4 areas
            //wait 5 seconds
            int index = 0;
            int index2 = 0;
            int index3 = 0;
            do
            {
                index = Random.Range(1, length);
                index2 = Random.Range(1, length);
                index3 = Random.Range(1, length);
            }
            while (index != index2 && index2 != index3 && index != index3);

            //spawn water
            Instantiate(waterObject, items[index]);
            Instantiate(waterObject, items[index2]);
            Instantiate(waterObject, items[index3]);

            //timer to wait till next spawn
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator WaitSeconds()
    {
        updateLock = false;
        yield return new WaitForSeconds(1.0f);
        updateLock = true;
    }


	// Update is called once per frame
	void Update ()
    {
        if (updateLock)
        {
            //spawn in 4 areas
            //wait 5 seconds
            int index = 0;
            int index2 = 0;
            int index3 = 0;
            do
            {
                index = Random.Range(1, length);
                index2 = Random.Range(1, length);
                index3 = Random.Range(1, length);
            }
            while (index != index2 && index2 != index3 && index != index3);

            //spawn water
            Instantiate(waterObject, items[index]);
            Instantiate(waterObject, items[index2]);
            Instantiate(waterObject, items[index3]);
            StartCoroutine(WaitSeconds());
        }
    }
}
