using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_itemsInteractions : MonoBehaviour
{
    private RectTransform player;

    public int clipLevel;
    public Vector3 intPos;

    private void checkPos()
    {
        if (GameMap.gameLevel == 3)
        {
            if (GameMap.getIntPos(player.localPosition) == GameMap.getIntPos(transform.localPosition))
            {
                if (DimensionControl.getLevel() != clipLevel) return;

                Destroy(gameObject);
            }
        }
        else
        {
            if (GameMap.getIntPos(player.localPosition) == intPos)
            {
                if (DimensionControl.getLevel() != clipLevel) return;

                Destroy(gameObject);
            }
        }
        return;
    }

    private void initTransform()
    {
        if (GameMap.gameLevel == 3) return;
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
        checkPos();
    }
}
