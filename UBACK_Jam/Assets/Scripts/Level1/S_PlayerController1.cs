using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_PlayerController1 : MonoBehaviour
{
    private GameObject gameMapPanel;

    public float horizonVelocity = 1.0f;

    private void keyboardControl()
    {
        if (GameMap.controllable == false) return;
        
        RectTransform rt = GetComponent<RectTransform>();
        if (rt == null) return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!kickWall(new Vector3(0, 0, -1), rt))
            {
                DimensionControl.levelIncrease();
            }
            gameMapPanel.GetComponent<S_GameMap1>().updateMap();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!kickWall(new Vector3(0, 0, +1), rt))
            {
                DimensionControl.levelDecrease();
            }
            gameMapPanel.GetComponent<S_GameMap1>().updateMap();
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (!kickWall(new Vector3(1, 0, 0), rt)) {
                transform.localPosition += new Vector3(horizonVelocity, 0, 0) * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (!kickWall(new Vector3(-1, 0, 0), rt))
            {
                transform.localPosition -= new Vector3(horizonVelocity, 0, 0) * Time.deltaTime;
            }
        }
        return;
    }

    private void initScale()
    {
        // 缩放初始化
        Vector2 scaleRate = UITransform.getScreenScale(GetComponent<RectTransform>());
        if (scaleRate.y == 0.0f) return;
        transform.localScale *= 0.07f / scaleRate.y;

        // 同步场景内的所有缩放
        GameMap.gameScale = transform.localScale;

        // 向量初始化
        horizonVelocity = transform.localScale.x * 5.0f;
        return;
    }

    // 初始化切片范围限制和当前所在层次
    private void initClips()
    {
        DimensionControl.setLevelRange(new Vector2(0, 15));
        DimensionControl.setLevel(3);
        return;
    }

    private void initPosition()
    {
        RectTransform rt = GetComponent<RectTransform>();
        if (rt == null) return;
        float halfHeight = rt.rect.height / 2.0f * GameMap.gameScale.y;
        transform.localPosition += new Vector3(0, halfHeight, 0);
        return;
    }

    private bool kickWall(Vector3 _walkDir, RectTransform _rt) {
        Vector3 toCenter = new Vector3(_rt.pivot.x, 0, 0) * -_rt.localScale.x;
        Vector3 curPos = _rt.transform.localPosition + toCenter;

        // 同一平面内踢墙检测
        if (_walkDir.z == 0)
        {
            // 获取待检测位置晶格坐标
            _walkDir = _walkDir / _walkDir.magnitude * horizonVelocity * Time.deltaTime;
            Vector3 checkPos = curPos + _walkDir;
            checkPos = GameMap.getIntPos(checkPos) + new Vector3(1, 0, 0);

            // 检测是否有墙面
            if (checkPos.x < 0) return true;
            if (GameMap.gameMap[DimensionControl.getLevel()][(int)checkPos.x] != 0) return true;
            else return false;
        }
        // 隐维方向上踢墙检测
        else
        {
            curPos = GameMap.getIntPos(curPos);
            curPos += new Vector3(1, 0, 0);
            if (_walkDir.z < 0)
            {
                if (DimensionControl.outOfRange((int)curPos.z + 1)) return true;
                if (GameMap.gameMap[(int)curPos.z + 1][(int)curPos.x] > curPos.y) return true;
            }
            else
            {
                if (DimensionControl.outOfRange((int)curPos.z - 1)) return true;
                if (GameMap.gameMap[(int)curPos.z - 1][(int)curPos.x] > curPos.y) return true;
            }
            return false;
        }
    }

    void Awake()
    {
        initScale();
        initClips();
        initPosition();
        gameMapPanel = GameObject.Find("GameMapPanel");
    }

    void Start()
    {
        GameMap.gameLevel = 1;
        GameMap.controllable = true;
    }

    // Update is called once per frame
    void Update()
    {
        keyboardControl();
    }
}
