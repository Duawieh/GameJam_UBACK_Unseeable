using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_L3Tree : MonoBehaviour
{
    public Sprite[] treeStatus;
    public Vector3[] treePositions;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = treePositions[DimensionControl.getLevel()];
        GetComponent<SpriteRenderer>().sprite = treeStatus[DimensionControl.getLevel()];

        GetComponent<Collider>().enabled = DimensionControl.getLevel() == 4;
    }
}
