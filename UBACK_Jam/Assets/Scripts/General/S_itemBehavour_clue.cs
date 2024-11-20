using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_itemBehavour_clue : MonoBehaviour
{
    private void selfFloat()
    {
        float t = Time.time * Mathf.PI / 2;
        float derivative = Mathf.Sin(t) * GameMap.gameScale.y / 4;
        transform.localPosition += new Vector3(0, derivative, 0) * Time.deltaTime;
        return;
    }

    // Update is called once per frame
    void Update()
    {
        selfFloat();
    }
}
