using System.Collections;
using UnityEngine;
/*
 * @Author: l hy 
 * @Date: 2020-05-04 19:19:39 
 * @Description: 全局appcontext
 * @Last Modified by: l hy
 * @Last Modified time: 2020-05-05 17:12:48
 */

public class Appcontext : MonoBehaviour {

    private static Appcontext m_Instance = null;
    public static Appcontext getInstance () {
        return m_Instance;
    }

    private InputManager m_inputManager = null;
    public InputManager inputManager {
        get {
            return this.m_inputManager;
        }
    }

    private PlayerManager m_playerManager = null;
    public PlayerManager playerManager {
        get {

            return this.m_playerManager;
        }
    }

    [Header ("淡入淡出切换动画管理器")]
    public Animator fadeSceneAnimator = null;

    [Header ("圆形动画管理器")]
    public Animator circleAnimator = null;

    private void Awake () {
        m_Instance = this;
        DontDestroyOnLoad (this);
        this.setScripts ();
    }

    public void setScripts () {
        try {
            this.m_inputManager = GameObject.Find ("InputManager").GetComponent<InputManager> ();
        } catch (System.Exception) {
            Debug.Log ("input manager set fail");
        }

        try {
            this.m_playerManager = GameObject.Find ("PlayerManager").GetComponent<PlayerManager> ();
        } catch (System.Exception) {
            Debug.Log ("player manager set fail");
        }
    }

    public void loadNextScene (SceneSwitchEnum sceneType) {

        switch (sceneType) {

            case SceneSwitchEnum.FADE:
                this.fadeSceneAnimator.transform.parent.gameObject.SetActive (true);
                this.circleAnimator.transform.parent.gameObject.SetActive (false);
                // TODO: LOGO
                StartCoroutine (animationLoad (this.fadeSceneAnimator));
                break;

            case SceneSwitchEnum.CIRCLE:
                this.fadeSceneAnimator.transform.parent.gameObject.SetActive (false);
                this.circleAnimator.transform.parent.gameObject.SetActive (true);
                StartCoroutine (animationLoad (this.circleAnimator));
                break;

            case SceneSwitchEnum.LOGO:
                break;

            default:
                Debug.LogWarning ("scene switch enum is not exist" + sceneType);
                break;
        }
    }

    IEnumerator animationLoad (Animator targetAnimator) {
        targetAnimator.SetTrigger ("Start");

        yield return new WaitForSeconds (1);

        SceneLoadManager.getInstance ().loadNextScene ();
    }

}