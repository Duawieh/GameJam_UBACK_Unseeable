using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_itemsInteractions : MonoBehaviour
{
    private RectTransform player;
    private bool taken = false;

    public int clipLevel;
    public Vector3 intPos;
    // 0：通关道具 1：线索道具
    public int itemType;

    private void updateVisiable()
    {
        if (DimensionControl.getLevel() == clipLevel) {
            GetComponent<SpriteRenderer>().enabled = true;
            if (itemType == 0) {
                transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else {
            GetComponent<SpriteRenderer>().enabled = false;
            if (itemType == 0) {
                transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        return;
    }

    private void checkPos()
    {
        Vector3 playerIntPos = GameMap.getIntPos(player.localPosition);
        if (GameMap.gameLevel == 1) playerIntPos -= new Vector3(0, 2, 0);
        if (playerIntPos == intPos)
        {
            if (DimensionControl.getLevel() != clipLevel) return;
            if (taken == true) return;
            taken = true;
            switch (itemType) {
                case 0:
                    GameObject mc = GameObject.FindGameObjectWithTag("MainCamera");
                    mc.GetComponent<S_levelManager>().NextLevel();
                    GameMap.controllable = false;
                    break;
                case 1:
                    break;
            }
        }
        return;
    }

    private void initTransform()
    {
        // Level 3 不是使用 intPos 来初始化物品位置
        // 而是直接对在场景内摆放，用 GameMap.getIntPos(Vector3) 来计算 intPos
        if (GameMap.gameLevel == 3) 
        {
            intPos = GameMap.getIntPos(transform.localPosition);
            return;
        }
        float _x = 0.0f, _y = 0.0f;
        if (GameMap.gameLevel == 2)
        {
            _x = (intPos.x - 7.5f) * GameMap.gameScale.x;
            _y = (intPos.y - 2.0f) * GameMap.gameScale.y;
        }
        if (GameMap.gameLevel == 1)
        {
            _x = (intPos.x - 7.5f) * GameMap.gameScale.x;
            _y = (intPos.y + 0.5f) * GameMap.gameScale.y;
        }
        transform.localPosition = new Vector3(_x, _y, 0);
        transform.localScale = GameMap.gameScale;
        return;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<RectTransform>();
        initTransform();
        return;
    }

    // Update is called once per frame
    void Update()
    {
        updateVisiable();
        checkPos();
    }
}
