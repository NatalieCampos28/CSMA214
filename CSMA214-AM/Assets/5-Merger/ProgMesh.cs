using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ProgMesh : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[]
        {
            new Vector3(0,0,0),
            new Vector3(0,0,1),
            new Vector3(1,0,0),
            new Vector3(1,0,1),

        };

        triangles = new int[]
        {
            0,1,2,
            1,3,2
        };
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

//HW for spring break 
// - recording of 30 of genes and 5 screenshots
// combine all thre projects into onr single project and submit 30 sec record and 5 scnreens to moodle
//Flocking algorithm
// - goal where flock tries to flock too (attractor)
// - include anti goal(detractor) flying away 
//      - alg in Vector direction
//Gene Algorithm:
//- flock should evolve based off of citeria i design
//    - color changes or faster or slower or neighb distance changes
//    - flock has to be flying over a programmatic mesh or in a canyon or between prog mesh 
// 