using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoattack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void keydown()   // 클릭
    {
        ingamemanager.Call().keydownup(true);   // 이미지 작아짐
    }

    public void keyup()
    {
        ingamemanager.Call().keydownup(false);  // 이미지 커짐
    }
}
