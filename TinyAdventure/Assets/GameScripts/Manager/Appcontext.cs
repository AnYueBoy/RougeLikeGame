using UnityEngine;
/*
 * @Author: l hy 
 * @Date: 2020-05-04 19:19:39 
 * @Description: 全局appcontext
 * @Last Modified by: l hy
 * @Last Modified time: 2020-05-04 19:29:50
 */

public class Appcontext : MonoBehaviour {

    private static Appcontext m_Instance = null;
    public static Appcontext getInstance () {
        return m_Instance;
    }

    public InputManager m_inputManager = null;
    public InputManager inputManager {
        get {
            return this.m_inputManager;
        }
    }

    public PlayerManager m_playerManager = null;
    public PlayerManager playerManager {
        get {

            return this.m_playerManager;
        }
    }

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

}