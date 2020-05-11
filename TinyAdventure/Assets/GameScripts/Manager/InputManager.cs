/*
 * @Author: l hy 
 * @Date: 2020-05-01 22:11:13 
 * @Description: 输入管理
 * @Last Modified by: l hy
 * @Last Modified time: 2020-05-11 19:23:25
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    [Header ("是否开启多点触控")]
    public bool isOpenMultiTouch = false;

    [Header ("dot移动点")]
    public Image moveDot = null;

    [Header ("触发半径")]
    public float triggerRadius = 0;

    private Vector3 moveDirection = Vector3.zero;

    private void Start () {
        Input.multiTouchEnabled = this.isOpenMultiTouch;
    }

    public void updateSelf () {
        this.touchStart ();
        this.touchMove ();
        this.touchEnd ();
        this.keyBoardInput ();
    }

    private Vector3 touchStartPos = Vector3.zero;

    private void touchStart () {
        if (Input.touchCount <= 0) {
            return;
        }

        Touch touchData = Input.GetTouch (0);
        if (touchData.phase == TouchPhase.Began) {
            this.gameObject.transform.position = touchData.position;
            this.touchStartPos = touchData.position;
        }
    }

    private void touchMove () {
        if (Input.touchCount <= 0) {
            return;
        }

        Touch touchData = Input.GetTouch (0);
        Vector3 movePos = Vector3.zero;
        if (touchData.phase == TouchPhase.Moved) {
            movePos = touchData.position;
            Vector3 subVec = movePos - this.touchStartPos;
            float dis = subVec.magnitude;

            Vector3 moveDir = subVec.normalized;

            if (dis > ConstValue.moveMaxRadius) {
                dis = ConstValue.moveMaxRadius;
            }

            this.moveDot.transform.localPosition = moveDir * dis;

            if (dis < this.triggerRadius) {
                // 不触发移动逻辑
                this.moveDirection = Vector3.zero;
                return;
            }

            this.moveDirection = moveDir * dis / ConstValue.moveMaxRadius;
        }
    }

    private void touchEnd () {
        if (Input.touchCount <= 0) {
            return;
        }

        Touch touchData = Input.GetTouch (0);
        if (touchData.phase == TouchPhase.Ended) {
            this.moveDot.transform.localPosition = Vector3.zero;
            this.touchStartPos = Vector3.zero;
            this.moveDirection = Vector3.zero;
        }
    }

    private void keyBoardInput () {
        if (!Application.isEditor) {
            return;
        }
        float horizontalValue = Input.GetAxis ("Horizontal");
        if (Mathf.Abs (horizontalValue) < 0.01) {
            this.moveDirection = Vector3.zero;
            return;
        }

        if (horizontalValue < 0) {
            this.moveDirection = Vector3.left;
            return;
        }

        if (horizontalValue > 0) {
            this.moveDirection = Vector3.right;
        }

    }

    public Vector3 getMoveDirection () {
        return this.moveDirection;
    }
}