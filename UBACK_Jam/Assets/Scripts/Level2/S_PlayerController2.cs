using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_PlayerController2 : MonoBehaviour
{
    private GameObject gameMapPanel;

    public float dropVelocity = 0.0f;

    public bool uponGround = false;
    public float horizonVelocity = 1.0f;
    public float gravityAccelerate = 1.0f;

    private void gravityBehaviours()
    {
        RectTransform rt = GetComponent<RectTransform>();
        if (rt == null) return;

        float rootHeight = rt.localPosition.y - (rt.rect.height / 2.0f * rt.localScale.y);
        float groundHeight = getGroundHeight(rt);

        // 下坠
        if (rootHeight > groundHeight + 1e-4f)
        {
            dropVelocity += gravityAccelerate * Time.deltaTime;
            rt.localPosition += new Vector3(0, -dropVelocity, 0) * Time.deltaTime;
            uponGround = true;
        }
        // 落地
        else
        {
            float halfBodyHeight = rt.rect.height / 2.0f * rt.localScale.y;
            rt.localPosition = new Vector3(rt.localPosition.x, groundHeight + halfBodyHeight, 0);
            dropVelocity = 0.0f;
            uponGround = false;
        }
    }

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
            gameMapPanel.GetComponent<S_GameMap2>().updateMap();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!kickWall(new Vector3(0, 0, +1), rt))
            {
                DimensionControl.levelDecrease();
            }
            gameMapPanel.GetComponent<S_GameMap2>().updateMap();
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (!kickWall(new Vector3(1, 0, 0), rt)) {
                Vector3 trans = new Vector3(+horizonVelocity, 0, 0) * Time.deltaTime;
                if (uponGround) trans *= 0.65f;
                transform.localPosition += trans;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (!kickWall(new Vector3(-1, 0, 0), rt))
            {
                Vector3 trans = new Vector3(-horizonVelocity, 0, 0) * Time.deltaTime;
                if (uponGround) trans *= 0.65f;
                transform.localPosition += trans;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!uponGround)
            {
                transform.localPosition += new Vector3(0, gravityAccelerate * 0.01f, 0);
                dropVelocity = -gravityAccelerate * 0.45f;
                uponGround = true;
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
        gravityAccelerate = transform.localScale.y * 10.0f;
        return;
    }

    // 初始化切片范围限制和当前所在层次
    private void initClips()
    {
        DimensionControl.setLevelRange(new Vector2(0, 15));
        DimensionControl.setLevel(1);
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
            checkPos = GameMap.getIntPos(checkPos);
            checkPos += new Vector3(1, 0, 0);

            // 检测是否有墙面
            if (checkPos.x < 0) return true;
            if (checkPos.y < GameMap.gameMap[DimensionControl.getLevel()][(int)checkPos.x]) return true;
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

    /// <summary>
    /// 由世界坐标获取当前地块的地面世界坐标高度
    /// </summary>
    /// <returns></returns>
    private float getGroundHeight(RectTransform _rt)
    {
        Vector3 toCenter = new Vector3(_rt.pivot.x, 0, 0) * _rt.localScale.x;
        Vector3 curPos = _rt.transform.localPosition + toCenter;
        // 将世界坐标转换为地图晶格坐标
        Vector3 intPos = GameMap.getIntPos(curPos);
        float intHeight = GameMap.gameMap[(int)intPos.z][(int)intPos.x];
        return (intHeight - 2.5f) * GameMap.gameScale.y;
    }

    void Awake()
    {
        initScale();
        initClips();
        gameMapPanel = GameObject.Find("GameMapPanel");
    }

    void Start()
    {
        GameMap.gameLevel = 2;
        GameMap.controllable = true;
    }

    // Update is called once per frame
    void Update()
    {
        keyboardControl();
        gravityBehaviours();
    }
}
