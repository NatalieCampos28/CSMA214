using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDNA : MonoBehaviour
{
    public float r;
    public float g;
    public float b;

    public List<GameObject> neighbors = new List<GameObject>();

    public DistributionManager distManager;

    float _i;
    float _j;
    float _k;

    float totalCount;

    bool distribComp = false;

    //var of prefab
    Renderer rend;
    //BoxCollider collider;
    BoxCollider collider;

    private void OnMouseDown()
    {
        RandomColor();
    }
    void Start()
    {
        rend = GetComponent<Renderer>();
        collider = GetComponent<BoxCollider>();

        rend.material.color = new Color(r, g, b);

        _i = distManager.gridSize.x;
        _j = distManager.gridSize.y;
        _k = distManager.gridSize.z;

        totalCount = _i*_j*_k;
    }

    private void Update()
    {
        if (totalCount == distManager.allObj.Count && !distribComp)
        {
            distribComp = true;
            FindNeighbors();
        }
    }

    void FindNeighbors()
    {
        foreach(GameObject unit in distManager.allObj)
        {
            float distance = Vector3.Distance(unit.transform.position, this.transform.position);

            distance *= Random.value;

            if(distance <= distManager.neighborDistance)
            {
                neighbors.Add(unit);
            }
        }
    }
    void RandomColor()
    {
        r = Random.value;
        g = Random.value;
        b = Random.value;

        rend.material.color = new Color(r, g, b);
    }
}
