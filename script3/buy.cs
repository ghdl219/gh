using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buy : MonoBehaviour,IPointerClickHandler {
    public bool flag;   //  아이템 착용 안햇는데 수치 올라가는거 방지
    public int itemindex;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}
    
    public void OnClick()   // 아이템 사기
    {
        //ingamemanager.Call().itembuy(itemindex);
    }

    public void OnPointerClick(PointerEventData data)   // 아이템 사기
    {
        if (data.button != 0)   // 0 왼쪽
        {
            flag = true;
            ingamemanager.Call().itembuy(itemindex);
        }
    }

    public void Ondrop()    // 마우스오버 아이템 정보보기
    {
        flag = false;
        ingamemanager.Call().tooltip(itemindex, flag);
    }

    public void Ondropout() // 마우스오버아웃
    {
        ingamemanager.Call().tooltipout();
    }
}
