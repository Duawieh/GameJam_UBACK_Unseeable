using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_L3Scene : MonoBehaviour
{
    public Material floor_clean;
    public Material floor_boom;

    // Update is called once per frame
    void Update()
    {
        Renderer r = GetComponent<Renderer>();
        if (DimensionControl.getLevel() == 5)
        {
            r.material = floor_boom;
        }
        else
        {
            r.material = floor_clean;
        }
    }
}
