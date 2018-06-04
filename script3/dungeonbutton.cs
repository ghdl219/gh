using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dungeonbutton : MonoBehaviour, IPointerClickHandler{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData data)   // 던전 선택
    {
        if (data.button == 0)   // 0 왼쪽
        {
            ingamemanager.Call().dungeonbutton();
        }
    }
}
