/*
 * @Author: l hy 
 * @Date: 2020-05-01 21:55:13 
 * @Description: 玩家管理
 * @Last Modified by: l hy
 * @Last Modified time: 2020-05-07 22:43:20
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    [Header ("动画状态机")]
    public Animator m_Animator = null;

    [Header ("残影预制")]
    public GameObject shadowPrefab = null;

    [Header ("预制父节点")]
    public Transform shadowParent = null;

    [Header ("图片渲染器")]
    public SpriteRenderer spriteRenderer = null;

    public void updateSelf () {
        this.run ();
        this.sprint ();
    }

    private void run () {
        Vector3 moveDir = Appcontext.getInstance ().inputManager.getMoveDirection ();
        if (moveDir == Vector3.zero) {
            if (this.m_Animator.GetBool ("Run")) {
                this.m_Animator.SetBool ("Run", false);
            }
            return;
        }

        this.transform.localScale = new Vector3 (moveDir.x, 1, 1);

        this.transform.Translate (moveDir * ConstValue.moveSpeed * Time.deltaTime);

        if (!this.m_Animator.GetBool ("Run")) {
            this.m_Animator.SetBool ("Run", true);
        }
    }

    private bool isSprint = false;

    private float sprintTimer = 0;

    public void pressSprint () {
        if (!this.isSprint) {
            this.isSprint = true;
        }
    }

    private float shadowInterval = 0;

    private void sprint () {
        if (!this.isSprint) {
            return;
        }

        this.sprintTimer += Time.deltaTime;
        if (this.sprintTimer > ConstValue.sprintTime) {
            this.isSprint = false;
            this.shadowInterval = 0;
            this.sprintTimer = 0;
        }

        float dir = this.transform.localScale.x;

        this.transform.Translate (new Vector3 (dir, 0, 0) * ConstValue.sprintSpeed * Time.deltaTime);

        if (!this.m_Animator.GetBool ("Run")) {
            this.m_Animator.SetBool ("Run", true);
        }

        this.shadowInterval += Time.deltaTime * ConstValue.sprintSpeed;
        if (this.shadowInterval < ConstValue.bodyLength) {
            return;
        }
        this.shadowInterval = 0;

        GameObject shadowNode = ObjectPool.getInstance ().requestInstance (this.shadowPrefab);
        shadowNode.transform.SetParent (this.shadowParent);
        Shadow shadow = shadowNode.GetComponent<Shadow> ();
        // TODO: 数组管理shadow

        Sprite targetSprite = this.spriteRenderer.sprite;
        shadow.init (targetSprite, this.transform.position, this.transform.localScale.x);

    }
}