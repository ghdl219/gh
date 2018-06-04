using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class characterslot : MonoBehaviour, IPointerClickHandler {
    public int slotnum;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void rigthclick()
    {

    }

    public void OnPointerClick(PointerEventData data)   // 캐릭터 창에 있는 아이템 해제
    {
        if (data.button != 0)   // 0 왼쪽
        {
            ingamemanager.Call().inventoryslot(slotnum);    // 0 ~ 4 까지 있다
        }
    }

    public void itemon()    // 마우스오버 아이템 정보보기
    {
        ingamemanager.Call().charactertooltip(slotnum);
    }

    public void itemoff()   // 마우스오버아웃
    {
        ingamemanager.Call().tooltipout();
    }
}
