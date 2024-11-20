using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_PlayerController3 : MonoBehaviour
{
    private const float HALF_BODY_HEIGHT = 0.256f;
    private const float HORIZEN_VELOCITY = 1.2f;
    private const float GRAVITY_ACCELERATE = 3.0f;

    private float dropVelocity = 0.0f;

    public GameObject tempObj_audioPlayer;
    public AudioClip audio_jump, audio_down;

    /// <summary>
    /// 检测人物是否离开地面
    /// </summary>
    private bool uponGround() {
        // 利用射线检测检测地面高度
        Vector3 hitBody = getColliderPos(new Vector3(0, -1, 0));
        if (hitBody == new Vector3(-1, -1, -1)) return false;
        if (transform.localPosition.y - hitBody.y > HALF_BODY_HEIGHT + 1e-4f) return true;
        return false;
    }

    /// <summary>
    /// 以主角为射线端点，进行给定方向上的射线检测
    /// </summary>
    /// <param name="_dir">指定的射线方向</param>
    /// <returns>射线碰撞点坐标</returns>
    private Vector3 getColliderPos(Vector3 _dir) {
        // 允许检测背面
        Physics.queriesHitBackfaces = true;
        RaycastHit rtn;
        Ray ray = new Ray(transform.localPosition, _dir.normalized);
        if (Physics.Raycast(ray, out rtn)) return rtn.point;
        return new Vector3(-1, -1, -1);
    }

    private void gravityBehavour() {
        transform.localPosition -= new Vector3(0, dropVelocity, 0) * Time.deltaTime;

        if (uponGround()) {
            dropVelocity += GRAVITY_ACCELERATE * Time.deltaTime;
        }
        else 
        {
            Vector3 groundPos = getColliderPos(new Vector3(0, -1, 0));
            if (groundPos == new Vector3(-1, -1, -1)) {
                groundPos = getColliderPos(new Vector3(0, 1, 0));
                transform.localPosition = groundPos + new Vector3(0, HALF_BODY_HEIGHT, 0);
            }
            else {
                transform.localPosition = groundPos + new Vector3(0, HALF_BODY_HEIGHT, 0);
            }
            dropVelocity = 0;
        }
        return;
    }

    private void keyboardControl() {
        if (GameMap.controllable == false) return;

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!uponGround()) {
                dropVelocity = -1.4f;
                // 播放跳跃音效
                GameObject adp = Instantiate(tempObj_audioPlayer);
                adp.GetComponent<S_audioPlayer>().adc = audio_jump;
                adp.GetComponent<S_audioPlayer>().life = 1.0f;
            }
        }

        Vector3 transVec = Vector3.zero;

        if (Input.GetKey(KeyCode.A)) {
            transVec += new Vector3(-1, 0, -1);
        }
        if (Input.GetKey(KeyCode.D)) {
            transVec += new Vector3(+1, 0, +1);
        }
        if (Input.GetKey(KeyCode.W)) {
            transVec += new Vector3(-1, 0, +1);
        }
        if (Input.GetKey(KeyCode.S)) {
            transVec += new Vector3(+1, 0, -1);
        }

        if (transVec != Vector3.zero) {
            transVec /= transVec.magnitude;
            transVec += new Vector3(0, -dropVelocity, 0);
            Vector3 colliderPos = getColliderPos(transVec);
            if ((colliderPos - transform.localPosition).magnitude > HALF_BODY_HEIGHT) {
                transVec -= new Vector3(0, -dropVelocity, 0);
                transform.localPosition += transVec * HORIZEN_VELOCITY * Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            DimensionControl.levelIncrease();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            DimensionControl.levelDecrease();
        }

        return;
    }    

    void Awake() {
        GameMap.gameScale = Vector3.one * 0.5f;
        GameMap.gameLevel = 3;
        DimensionControl.setLevelRange(new Vector2(0, 5));
        DimensionControl.setLevel(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameMap.controllable = true;
    }

    // Update is called once per frame
    void Update()
    {
        keyboardControl();
        gravityBehavour();
    }
}
