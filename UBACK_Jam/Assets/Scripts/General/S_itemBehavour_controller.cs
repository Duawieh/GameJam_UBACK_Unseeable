using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_itemBehavour_controller : MonoBehaviour
{
    private void selfRotation()
    {
        // 每两秒旋转一周
        float t = Time.time * Mathf.PI;
        transform.localScale = new Vector3(Mathf.Sin(t) * 0.25f, 0.25f, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        selfRotation();
    }
}
