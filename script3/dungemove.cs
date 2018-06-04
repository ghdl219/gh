using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class dungemove : MonoBehaviour {
    public uimanager uiM;
    public dungeonselect dungeonselect;

    public Transform dungeonmove1;    // 아이템 부모
    public Text[] dungeonname;    // 아이템 이름 배열
    public Text[] dungeonprice;   // 아이템 가격 배열
    public Vector3 x;   // 위치
    public Vector3 yy;  // 밑으로 증가 및 처음으로 초기화
    public Vector3 s;   // 크기

	// Use this for initialization
	void Start () {
        x = new Vector3(-0.1f, 7, 0);
        yy = new Vector3(0, -4.5f, 0);
        s = new Vector3(1.5f, 0.035f, 1);

        for (int i = 0; i < uiM.idungeonimage.Length; i++)
        {
            uiM.idungeonimage[i] = Instantiate(uiM.dungeonimage);
            uiM.idungeonimage[i].transform.SetParent(dungeonmove1);
            uiM.idungeonimage[i].rectTransform.localPosition = x; // 글로벌로 되면서안된다 로컬로 해야됨 -> localPosition
            uiM.idungeonimage[i].rectTransform.localScale = s;
            uiM.idungeonimage[i].color = new Color(255, 255 / 255, 255 / 255);  // 힌색
            dungeonname[i] = uiM.idungeonimage[i].GetComponentInChildren<Text>();   // 던전 이름
            dungeonprice[i] = uiM.idungeonimage[i].transform.FindChild("dungeonlv").GetComponent<Text>();   // 입장 레벨
            dungeontext(i);
            x = x + yy; // 증가
            dungeonselect.dungeonnum = i + 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void dungeontext(int i)
    {
        switch (i)
        {
            case 0: dungeonname[i].text = "초원초원";
                dungeonprice[i].text = "Lv : 1";
                break;
            case 1: dungeonname[i].text = "???";
                dungeonprice[i].text = "Lv : 3";
                break;
        } 
    }
}
