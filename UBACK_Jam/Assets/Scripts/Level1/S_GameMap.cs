using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public static class GameMap {
    public static int gameLevel;

    public static int[][] gameMap = new int[][]{new int[]{0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7},
                                                new int[]{0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7},
                                                new int[]{0, 0, 0, 0, 1, 2, 3, 4, 0, 0, 0, 0, 0, 0, 0, 7},
                                                new int[]{0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7},
                                                new int[]{0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7},
                                                new int[]{0, 0, 3, 3, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7},
                                                new int[]{0, 0, 3, 3, 1, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 7},
                                                new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7},
                                                new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 7},
                                                new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7},
                                                new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 7},
                                                new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7},
                                                new int[]{0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 0, 0, 3, 3, 3, 7},
                                                new int[]{0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 0, 0, 3, 3, 3, 7},
                                                new int[]{0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 0, 0, 3, 3, 3, 7},
                                                new int[]{7, 7, 7, 4, 4, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7}};

    public static Vector3 getIntPos(Vector3 _checkPos) {
            int _x = (int)Math.Floor(_checkPos.x);
            int _y = (int)Math.Floor(_checkPos.y);
            int _z = (int)Math.Floor(_checkPos.z);
            return new Vector3(_x, _y, _z);
        }
}

public class S_GameMap : MonoBehaviour
{
    public GameObject mapTile;

    // Start is called before the first frame update
    void Start()
    {
        GameObject a = Instantiate(mapTile, transform);
        a.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
