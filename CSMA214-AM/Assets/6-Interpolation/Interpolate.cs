using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpolate : MonoBehaviour
{
    [SerializeField] private Transform[] points = new Transform[9];
    //pointA, pointB, pointAB, pointC, pointBC, pointABBC, pointD, pointCD, pointBCCD;
    //    0,       1,       2,      3,         4,       5,      6,      7,          8


    //pointA, pointB, pointC, pointD, pointAB, pointBC, pointABBC, pointCD, pointBCCD;
    //    0,       1,       2,      3,      4,       5,      6,      7,          8
    [Range(0, 1)]
    public float interpolateAmount = 0.0f;

    private void Update()
    {
        interpolateAmount = (interpolateAmount + Time.deltaTime) % 1;

        for(int i = 5; i <= points.Length-4; i++)
        {
            Debug.Log(points[i]);
            points[i].position = Vector3.Lerp(points[i].position, points[i++].position, interpolateAmount);
            Debug.Log("finished");

        }
//        //point AB goes from pt A to pt B
//        pointAB.position = Vector3.Lerp(pointA.position, .position, interpolateAmount);
//pointB
//        //point BC goes from pt B to pt C
//        pointBC.position = Vector3.Lerp(pointB.position, pointC.position, interpolateAmount);

//        //point ABBC goes from pt AB to pt BC
//        pointABBC.position = Vector3.Lerp(pointAB.position, pointBC.position, interpolateAmount);

//        //point CD goes from pt C to pt D
//        pointCD.position = Vector3.Lerp(pointC.position, pointD.position, interpolateAmount);

//        //point BCCD goes from pt BC to pt CD
//        pointBCCD.position = Vector3.Lerp(pointBC.position, pointCD.position, interpolateAmount);
    }

}
