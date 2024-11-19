using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public static class GameMap {
    public static int gameLevel;

    public static bool controllable;

    public static Vector3 gameScale = Vector3.one;

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
                                                new int[]{7, 7, 7, 5, 5, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7}};
    
    // 从世界坐标转换到地图晶格坐标
    // 仅限 Level 1 和 Level 2 调用
    public static Vector3 getIntPos(Vector3 _checkPos)
    {
        _checkPos /= gameScale.x;
        int _x = (int)Math.Floor(_checkPos.x + 7.5f);
        int _y = (int)Math.Floor(_checkPos.y + 2.0f);
        int _z = DimensionControl.getLevel();
        return new Vector3(_x, _y, _z);
    }
}

public class S_GameMap1 : MonoBehaviour
{
    public GameObject mapTile;

    private void instantiateMapTile(Vector2 _pos)
    {
        GameObject a = Instantiate(mapTile, transform);
        a.transform.localScale = GameMap.gameScale;
        a.transform.localPosition = _pos * GameMap.gameScale;
    }

    private void flushMap()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("MapTile");
        foreach (GameObject t in tiles)
        {
            DestroyImmediate(t);
        }
        return;
    }

    public void updateMap()
    {
        flushMap();
        drawMap();
        return;
    }

    private void drawMap()
    {
        int clipLevel = DimensionControl.getLevel();

        // 绘制水平方向切片连接线
        for (int i = 0; i < 16; i++)
        {
            if (GameMap.gameMap[clipLevel][i] == 0)
            {
                instantiateMapTile(new Vector2(i - 7.5f, 0));
            }
        }
    }

    void Awake() {
        GameMap.gameLevel = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        updateMap();
    }
}
