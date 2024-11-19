using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class S_GameMap2 : MonoBehaviour
{
    public GameObject mapTile;

    private void instantiateMapTile(Vector2 _pos, Vector3 _euler)
    {
        GameObject a = Instantiate(mapTile, transform);
        a.transform.localScale = GameMap.gameScale;
        a.transform.localPosition = _pos * GameMap.gameScale;
        a.transform.localEulerAngles = _euler;
        return;
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
            instantiateMapTile(new Vector2(i - 7.5f, GameMap.gameMap[clipLevel][i] - 2.5f), Vector3.zero);
        }

        // 绘制垂直方向切片连接线
        for (int i = 1; i < 16; i++)
        {
            int curHeight = GameMap.gameMap[clipLevel][i];
            int preHeight = GameMap.gameMap[clipLevel][i - 1];
            if (curHeight > preHeight)
            {
                for (int j = preHeight; j < curHeight; j++)
                {
                    instantiateMapTile(new Vector2(i - 8f, j - 2f), new Vector3(0, 0, +90));
                }
            }
            else
            {
                for (int j = curHeight; j < preHeight; j++)
                {
                    instantiateMapTile(new Vector2(i - 8f, j - 2f), new Vector3(0, 0, -90));
                }
            }
        }
    }

    void Awake() {
        GameMap.gameLevel = 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        updateMap();
    }
}
