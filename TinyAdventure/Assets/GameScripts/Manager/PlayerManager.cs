/*
 * @Author: l hy 
 * @Date: 2020-05-01 21:55:13 
 * @Description: 玩家管理
 * @Last Modified by: l hy
 * @Last Modified time: 2020-05-04 19:27:04
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    [Header ("动画状态机")]
    public Animator m_Animator = null;

    public void updateSelf () {
        this.run ();
    }

    private void run () {
        Vector3 moveDir = Appcontext.getInstance ().inputManager.getMoveDirection ();
        if (moveDir == Vector3.zero) {
            if (this.m_Animator.GetBool ("Run")) {
                this.m_Animator.SetBool ("Run", false);
            }
            return;
        } 

        if (!this.m_Animator.GetBool ("Run")) {
            this.m_Animator.SetBool ("Run", true);
        }
    }
}