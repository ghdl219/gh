using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour {
    public itemmanager itemM;
    public sale sale;
    public uimanager uiM;
    public itemcharacter itemcharac;
    public Transform inventory1;    // 아이템 부모
    public Vector3 x;   // 위치
    public Vector3 xx;  // 오른쪽 증가
    public Vector3 yy;  // 밑으로 증가 및 처음으로 초기화
    public Vector3 s;   // 크기
    public int[] inventorynum;  // 툴팁에서 쓸 이미지 번호

	// Use this for initialization
	void Start () {
        x = new Vector3(-64, 7, 0);
        xx = new Vector3(26, 0, 0);
        yy = new Vector3(-156, -4.5f, 0);
        s = new Vector3(0.2f, 0.037f, 1);

        for (int i = 0; i < itemM.Iitemslotinven.Length; i++)
        {
            itemM.Iitemslotinven[i] = Instantiate(itemM.iteminvenbase);
            itemM.Iitemslotinven[i].transform.SetParent(inventory1);
            itemM.Iitemslotinven[i].rectTransform.localPosition = x; // 글로벌로 되면서안된다 로컬로 해야됨 -> localPosition
            itemM.Iitemslotinven[i].rectTransform.localScale = s;
            inventorynum[i] = 99;
            sale.itemsale = i + 1;
            x = x + xx;

            if (((i + 1) % 6) == 0)   // 6번째 아이콘
            {
                if (i == 0 || i == 1)   // 0 1 제외
                {
                    continue;
                }
                x = x + yy; // 증가
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void inventoryitembuy(int itemnum, int pagenum)  // 페이지당 itemnum 아이템 번호, pagenum 페이지 번호
    {
        int itemindex = 0;
        if (pagenum == 1)
        {
            itemindex = 0;
        }
        else if (pagenum == 2)
        {
            itemindex = 6;
        }
        else if (pagenum == 3)
        {
            itemindex = 12;
        }

        for (int i = 0; i < itemM.Iitemslotinven.Length; i++)
        {
            if (itemM.Iitemimageslotinvenbase[i] == null)
            {
                itemM.Iitemslotinven[i].sprite = itemM.Iitemimageslotdeal[itemnum + itemindex];
                itemM.Iitemimageslotinvenbase[i] = itemM.Iitemimageslotdeal[itemnum + itemindex];
                inventorynum[i] = itemnum + itemindex;  // 툴팁에서 쓸 상점의 이미지 번호를 인벤토리 번호에 넣어줌
                break;
            }
        }
    }

    public void inventoryitemsale(int itemnum)  // itemnum 아이템 번호
    {
        if (uiM.gdeal.activeSelf)   // 아이템 팔기
        {
            itemM.Iitemslotinven[itemnum].sprite = null;
            itemM.Iitemimageslotinvenbase[itemnum] = null;
            inventorynum[itemnum] = 99; // 99로 초기화
        }
        
        if (uiM.gitemcharacter.activeSelf && uiM.ilv >= uiM.filv)  // 아이템 착용, 레벨 비교
        {
            itemcharac.itemnum(inventorynum[itemnum], itemM.Iitemimageslotinvenbase[itemnum]);  // 상점 아이템 순번, 상점 아이템 이미지 순번
            itemM.Iitemslotinven[itemnum].sprite = null;
            itemM.Iitemimageslotinvenbase[itemnum] = null;
            inventorynum[itemnum] = 99; // 99로 초기화
        }
        else if (uiM.gitemcharacter.activeSelf && uiM.ilv < uiM.filv)
        {
            Debug.Log("레벨 부족");
        }
    }
    
    public void inventoryslot(int a)    // 캐릭터 아이템 해제 후 인벤토리에 넣어 준다
    {
        for (int i = 0; i < itemM.Iitemslotinven.Length; i++)
        {
            if (itemM.Iitemimageslotinvenbase[i] == null)   // 비어 있는곳 찾는다
            {
                itemM.Iitemslotinven[i].sprite = itemM.Iitemimageslotdeal[itemcharac.characteritem[a]]; // itemcharac.characteritem[a] 여기에 상점아이템 순번 저장 되어 있다
                itemM.Iitemimageslotinvenbase[i] = itemM.Iitemimageslotdeal[itemcharac.characteritem[a]];
                inventorynum[i] = itemcharac.characteritem[a];
                break;
            }
        }
    }

    public void iminventoryslot(int a, int z)    // 아이템 교체 인벤토리에 넣는중 아이템순번, 인벤토리 순번
    {
        for (int i = 0; i < itemM.Iitemslotinven.Length; i++)
        {
            if (itemM.Iitemimageslotinvenbase[i] == null)   // 비어 있는곳 찾는다
            {
                itemM.Iitemslotinven[i].sprite = ingamemanager.Call().imequipment[a];
                itemM.Iitemimageslotinvenbase[i] = ingamemanager.Call().imequipment[a];
                inventorynum[i] = ingamemanager.Call().imcharacteritem[a];
                break;
            }
        }
    }
}
