using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributionManager : MonoBehaviour
{
    [SerializeField] GameObject unit;
    [SerializeField] public Vector3 gridSize = new Vector3();

    public float neighborDistance = 1;

    public List<GameObject> allObj = new List<GameObject>();

    private void Awake()
    {
        DistributeUnits();
    }

    void DistributeUnits()
    {
        for(int i = 0; i < gridSize.x; i++)
        {
            for(int j = 0; j < gridSize.y; j++)
            {
                for(int k = 0; k < gridSize.z; k++)
                {
                    Vector3 pos = new Vector3(i, j, k);
                    GameObject obj = Instantiate(unit, pos, Quaternion.identity, this.transform);

                    obj.GetComponent<CellDNA>().r = Random.value;
                    obj.GetComponent<CellDNA>().g = Random.value;
                    obj.GetComponent<CellDNA>().b = Random.value;

                    obj.GetComponent<CellDNA>().distManager = this;

                    allObj.Add(obj);
                }
            }
        }
    }
}
