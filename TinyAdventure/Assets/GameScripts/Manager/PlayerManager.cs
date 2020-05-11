/*
 * @Author: l hy 
 * @Date: 2020-05-01 21:55:13 
 * @Description: 玩家管理
 * @Last Modified by: l hy
 * @Last Modified time: 2020-05-11 19:41:48
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

    [Header ("碰撞框")]
    public Collider2D bodyCollider = null;

    [Header ("刚体")]
    public Rigidbody2D bodyRigidbody = null;

    [Header ("射线检测层")]
    public LayerMask layerMask;

    private List<Shadow> shadows = new List<Shadow> ();

    public void updateSelf () {
        this.run ();
        this.sprint ();
        this.jumpCheck ();
        this.refreshShadows ();
        this.keyBoardInput ();
    }

    /// <summary>
    /// 奔跑
    /// </summary>
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

    /// <summary>
    /// 冲刺
    /// </summary>
    private void sprint () {
        if (!this.isSprint) {
            return;
        }

        float dir = this.transform.localScale.x;
        // 撞到墙冲刺中断
        if (Util.ray2DCheck (this.transform.position, new Vector2 (dir, 0), ConstValue.sprintCheckDistance, this.layerMask)) {
            this.isSprint = false;
            this.shadowInterval = 0;
            this.sprintTimer = 0;
            return;
        }

        this.sprintTimer += Time.deltaTime;
        if (this.sprintTimer > ConstValue.sprintTime) {
            this.isSprint = false;
            this.shadowInterval = 0;
            this.sprintTimer = 0;
        }

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
        if (!this.shadows.Contains (shadow)) {
            this.shadows.Add (shadow);
        }

        Sprite targetSprite = this.spriteRenderer.sprite;
        shadow.init (targetSprite, this.transform.position, this.transform.localScale.x);
    }

    private bool isJump = false;

    public void jump () {
        if (!this.m_Animator.GetBool ("Jump")) {
            this.m_Animator.SetBool ("Jump", true);
        }

        if (!this.isJump) {
            this.isJump = true;
            this.bodyRigidbody.velocity = Vector3.up * ConstValue.jumpSpeed;
            this.bodyCollider.enabled = false;
        }

        // TODO: 二段跳
    }

    /// <summary>
    /// 跳跃检测
    /// </summary>
    private void jumpCheck () {
        if (!this.isJump) {
            return;
        }

        if (this.bodyRigidbody.velocity.y <= 0) {
            if (!this.bodyCollider.enabled) {
                this.bodyCollider.enabled = true;
            }

            if (Util.ray2DCheck (this.transform.position, Vector3.down, ConstValue.jumpCheckDistance, this.layerMask)) {
                this.isJump = false;
                if (this.m_Animator.GetBool ("Jump")) {
                    this.m_Animator.SetBool ("Jump", false);
                }
            }
        }
    }

    private void refreshShadows () {
        if (this.shadows == null || this.shadows.Count <= 0) {
            return;
        }

        for (int i = 0; i < this.shadows.Count; i++) {
            Shadow shadow = this.shadows[i];
            if (shadow == null) {
                continue;
            }

            if (!shadow.transform.gameObject.activeSelf) {
                continue;
            }

            shadow.selfUpdate ();
        }
    }

    /// <summary>
    /// 键盘输入
    /// </summary>
    private void keyBoardInput () {
        if (!Application.isEditor) {
            return;
        }

        if (Input.GetKeyDown (KeyCode.C)) {
            this.jump ();
        }

        if (Input.GetKeyDown (KeyCode.Space)) {
            this.pressSprint ();
        }
    }
}