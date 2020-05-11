/*
 * @Author: l hy 
 * @Date: 2020-03-15 21:00:27 
 * @Description: 游戏管理器
 * @Last Modified by: l hy
 * @Last Modified time: 2020-05-01 22:05:16
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private void Start () {
        // TODO: 临时解决办法，场景进入成功时发送事件
        Appcontext.getInstance().setScripts();
        this.inputManager = Appcontext.getInstance ().inputManager;
        this.playerManager = Appcontext.getInstance ().playerManager;
    }

    private InputManager inputManager = null;

    private PlayerManager playerManager = null;

    void Update () {
        if (this.inputManager != null) {
            this.inputManager.updateSelf ();
        }

        if (this.playerManager != null) {
            this.playerManager.updateSelf ();
        }

    }
}