using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_itemBehavour_clue : MonoBehaviour
{
    private Vector3 pos;

    private void selfRotation()
    {
        pos += new Vector3(0, Mathf.Sin(Time.time), 0) * GameMap.gameScale.y;
    }

    private void Start()
    {
        pos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        selfRotation();
    }
}
