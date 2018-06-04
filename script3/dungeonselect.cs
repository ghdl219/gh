using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dungeonselect : MonoBehaviour, IPointerClickHandler {
    public int dungeonnum;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()   // 던전 선택
    {

    }

    public void OnPointerClick(PointerEventData data)   // 던전 선택
    {
        if (data.button == 0)   // 0 왼쪽
        {
            ingamemanager.Call().dungeonmove(dungeonnum);
        }
    }
}
