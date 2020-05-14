using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    // Start is called before the first frame update
    void Start () {

    }

    private bool isStartLoad = false;

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            if (isStartLoad) {
                return;
            }
            this.isStartLoad = true;
            Appcontext.getInstance ().loadNextScene (SceneSwitchEnum.CIRCLE);
        }
    }
}