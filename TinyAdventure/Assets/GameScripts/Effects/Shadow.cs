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

    private Color selfColor;

    public void init (Sprite targetSprite, Vector3 pos, float scaleX) {
        this.transform.localScale = new Vector3 (scaleX, 1, 1);
        this.transform.position = pos;
        this.spriteRenderer.sprite = targetSprite;
        this.selfColor = this.spriteRenderer.color;
        this.disapperaSpeed = this.spriteRenderer.color.a / ConstValue.shadowExitTime;
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
        this.spriteRenderer.color = new Color (0.5f, 0.5f, 1, targetAlpha);
    }

    private void recycleSelf () {
        this.reset ();
        ObjectPool.getInstance ().returnInstance (this.gameObject);
    }

    private void reset () {
        this.shadowTimer = 0;
        this.spriteRenderer.color = this.selfColor;
    }
}