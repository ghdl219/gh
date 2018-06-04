using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class dealitem : MonoBehaviour {
    public itemmanager itemM;
    public buy buy;
    public Transform deal1; // Iitemslotdeal 의 부모
    public Text[] titemname;    // 아이템 이름 배열
    public Text[] titemprice;   // 아이템 가격 배열
    public Text tpageindex; // 상점 페이지 번호
    public Vector3 x;   // 위치
    public Vector3 xx;  // 오른쪽 증가
    public Vector3 yy;  // 밑으로 증가 및 처음으로 초기화
    public Vector3 s;   // 크기
    public int itemint;

	// Use this for initialization
	void Start () {
        x = new Vector3(-65, 7.5f, 0);  // 위치
        xx = new Vector3(78, 0, 0); // 오른쪽 증가
        yy = new Vector3(-156, -5, 0);   // 밑으로 증가 및 처음으로 초기화
        s = new Vector3(0.25f, 0.035f, 1);  // 크기

        for (int i = 0; i < 6; i++)
        {
            itemM.Iitemslotdeal[i] = Instantiate(itemM.itemdealbase);
            itemM.Iitemslotdeal[i].transform.SetParent(deal1);    // 하이러키창 deal1 자식으로
            itemM.Iitemslotdeal[i].rectTransform.localPosition = x; // 글로벌로 되면서안된다 로컬로 해야됨 -> localPosition
            itemM.Iitemslotdeal[i].rectTransform.localScale = s;    // 크기
            itemM.Iitemslotdeal[i].sprite = itemM.Iitemimageslotdeal[i];    // 아이템 이미지를 넣어준다
            titemname[i] = itemM.Iitemslotdeal[i].GetComponentInChildren<Text>();   // 아이템 이름
            titemprice[i] = itemM.Iitemslotdeal[i].transform.FindChild("itemprice").GetComponent<Text>();   // 아이템 가격
            itemtext(i);    // 아이템 이름 가격 표시
            buy.itemindex = i+1;    // 상점 아이템 순번
            x = x + xx; // 위치 증가
            
            if (i % 2 != 0)
            {
                x = x + yy; // 다음 줄
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        tpageindex.text = itemM.pageindex.ToString();   // 상점 페이지 번호 표시
	}

    public void itemtext(int a) // rightbutton.cs 에서 보네준다
    {
        if (itemM.pageindex == 1)   // 1페이지
        {
            switch (a)
            {
                case 0:
                    titemname[a].text = "초보자 무기";
                    titemprice[a].text = "0원";
                    itemint = 0;
                    break;
                case 1:
                    titemname[a].text = "합금 검";
                    titemprice[a].text = "1000원";
                    itemint = 1000;
                    break;
                case 2:
                    titemname[a].text = "강력한 검";
                    titemprice[a].text = "5000원";
                    itemint = 5000;
                    break;
                case 3:
                    titemname[a].text = "초보자 갑옷";
                    titemprice[a].text = "0원";
                    itemint = 0;
                    break;
                case 4:
                    titemname[a].text = "강철 갑옷";
                    titemprice[a].text = "2000원";
                    itemint = 2000;
                    break;
                case 5:
                    titemname[a].text = "황금 갑옷";
                    titemprice[a].text = "6000원";
                    itemint = 6000;
                    break;
            }
        }
        else if (itemM.pageindex == 2)  // 2페이지
        {
            switch (a)
            {
                case 0:
                    titemname[a].text = "초보자 모자";
                    titemprice[a].text = "0원";
                    itemint = 0;
                    break;
                case 1:
                    titemname[a].text = "마법사 모자";
                    titemprice[a].text = "1000원";
                    itemint = 1000;
                    break;
                case 2:
                    titemname[a].text = "강철 모자";
                    titemprice[a].text = "5000원";
                    itemint = 5000;
                    break;
                case 3:
                    titemname[a].text = "신발";
                    titemprice[a].text = "100원";
                    itemint = 100;
                    break;
                case 4:
                    titemname[a].text = "강화된 신발";
                    titemprice[a].text = "2000원";
                    itemint = 2000;
                    break;
                case 5:
                    titemname[a].text = "마법사 신발";
                    titemprice[a].text = "3000원";
                    itemint = 3000;
                    break;
            }
        }
        else if (itemM.pageindex == 3)  // 3페이지
        {
            switch (a)
            {
                case 0:
                    titemname[a].text = "기본 장갑";
                    titemprice[a].text = "1000원";
                    itemint = 1000;
                    break;
                case 1:
                    titemname[a].text = "장갑";
                    titemprice[a].text = "2000원";
                    itemint = 2000;
                    break;
                case 2:
                    titemname[a].text = "마법사 장갑";
                    titemprice[a].text = "3000원";
                    itemint = 3000;
                    break;
                case 3:
                    titemname[a].text = null;   // 3페이지 남는곳
                    titemprice[a].text = null;
                    break;
                case 4:
                    titemname[a].text = null;
                    titemprice[a].text = null;
                    break;
                case 5:
                    titemname[a].text = null;
                    titemprice[a].text = null;
                    break;
            }
        }
    }

    public void fitemtext(int i)
    {
        itemtext(i);    // 가격 비교하기 위해 ingamemanager에서 쓴다
    }

    public void saleitemtext(int i, int z)
    {
        if (z == 1)   // 1페이지
        {
            switch (i)
            {
                case 0:
                    itemint = 0;
                    break;
                case 1:
                    itemint = 100;
                    break;
                case 2:
                    itemint = 500;
                    break;
                case 3:
                    itemint = 0;
                    break;
                case 4:
                    itemint = 200;
                    break;
                case 5:
                    itemint = 600;
                    break;
            }
        }
        else if (z == 2)  // 2페이지
        {
            switch (i)
            {
                case 0:
                    itemint = 0;
                    break;
                case 1:
                    itemint = 100;
                    break;
                case 2:
                    itemint = 500;
                    break;
                case 3:
                    itemint = 10;
                    break;
                case 4:
                    itemint = 200;
                    break;
                case 5:
                    itemint = 300;
                    break;
            }
        }
        else if (z == 3)  // 3페이지
        {
            switch (i)
            {
                case 0:
                    itemint = 100;
                    break;
                case 1:
                    itemint = 200;
                    break;
                case 2:
                    itemint = 300;
                    break;
            }
        }
    }
}
