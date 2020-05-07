using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * @Author: l hy 
 * @Date: 2020-05-07 21:57:16 
 * @Description: 残影效果 
 * @Last Modified by: l hy
 * @Last Modified time: 2020-05-07 22:50:56
 */
public class Shadow : MonoBehaviour {

    [Header ("图片渲染器")]
    public SpriteRenderer spriteRenderer = null;

    private float shadowTimer = 0;

    private float disapperaSpeed = 0;

    public void init (Sprite targetSprite, Vector3 pos) {
        this.transform.position = pos;
        this.spriteRenderer.sprite = targetSprite;
        this.shadowTimer = 0;
        this.disapperaSpeed = this.spriteRenderer.color.a / ConstValue.shadowExitTime;
    }

    private void Update () {
        // TODO: 临时解决
        this.selfUpdate ();
    }

    public void selfUpdate () {
        this.shadowTimer += Time.deltaTime;
        if (this.shadowTimer > ConstValue.shadowExitTime) {
            this.recycleSelf ();
            return;
        }

        Color spriteColor = this.spriteRenderer.color;
        float targetAlpha = spriteColor.a - Time.deltaTime * this.disapperaSpeed;
        if (targetAlpha <= 0) {
            targetAlpha = 0;
        }
        this.spriteRenderer.color = new Color (spriteColor.r, spriteColor.g, spriteColor.b, targetAlpha);
    }

    private void recycleSelf () {
        ObjectPool.getInstance ().returnInstance (this.gameObject);
    }
}