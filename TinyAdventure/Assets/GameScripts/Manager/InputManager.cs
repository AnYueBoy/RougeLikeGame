using System.Net.Mime;
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

    [Header ("右侧移动节点")]
    public GameObject rightMoveNode = null;

    [Header ("左侧移动节点")]
    public GameObject leftMoveNode = null;

    [Header ("上侧移动节点")]
    public GameObject upMoveNode = null;

    [Header ("下侧移动节点")]
    public GameObject downMoveNode = null;

    private Vector3 moveDirection = Vector3.zero;

    public void updateSelf () { }

    public void pressLeftDown () {
        this.moveDirection = Vector3.left;
        this.leftMoveNode.SetActive (true);
    }

    public void pressLeftUp () {
        this.moveDirection = Vector3.zero;
        this.leftMoveNode.SetActive (false);
    }

    public void pressRightDown () {
        this.moveDirection = Vector3.right;
        this.rightMoveNode.SetActive (true);
    }

    public void pressRightUp () {
        this.moveDirection = Vector3.zero;
        this.rightMoveNode.SetActive (false);
    }

    public void pressUpDown () {
        this.moveDirection = Vector3.up;
        this.upMoveNode.SetActive (true);
    }

    public void pressUpUp () {
        this.moveDirection = Vector3.zero;
        this.upMoveNode.SetActive (false);
    }

    public void pressDownDown () {
        this.moveDirection = Vector3.down;
        this.downMoveNode.SetActive (true);
    }

    public void pressDownUp () {
        this.moveDirection = Vector3.zero;
        this.downMoveNode.SetActive (false);
    }

    private Vector3 touchStartPos = Vector3.zero;

    public Vector3 getMoveDirection () {
        return this.moveDirection;
    }
}