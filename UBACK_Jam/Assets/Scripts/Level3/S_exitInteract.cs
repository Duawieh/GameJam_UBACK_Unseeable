using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_exitInteract : MonoBehaviour
{
    private GameObject player;

    private void interact() 
    {
        Vector3 dis = player.transform.localPosition - transform.localPosition;
        if (dis.magnitude <= 0.256f) {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<S_levelManager>().NextLevel();
            Destroy(gameObject);
        }
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        interact();
    }
}
