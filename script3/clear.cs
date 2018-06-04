using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clear : MonoBehaviour {
    public ingamemanager im;
    public uimanager uiM;
	// Use this for initialization
	void Start () {
        im = ingamemanager.Call();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void fclear(int clearnum)
    {
        uiM.gitemcharacter.SetActive(false);    // 캐릭터창 종료
        uiM.lvup.text = ""; // 레벨 업 할때만 나오게 초기화
        uiM.lvup2.text = ""; // 레벨 업 할때만 나오게 초기화

        int expexp = 0;
        if (clearnum == 0)
        {
            im.itemcharac.exp = im.itemcharac.exp + 1000;   // 현재 경험치
            uiM.expbar.fillAmount = (float)im.itemcharac.exp / (float)im.itemcharac.fullexp; // 경험치 바 (현재, 최대)
            uiM.texpbar.text = im.itemcharac.exp + "/" + im.itemcharac.fullexp;    // 경험치 바 텍스트 표시
            im.money = im.money + 2500;   // 돈 획득
            im.tmoney.text = "    " + im.money; // 돈 획득 텍스트 표시
        }
        if (clearnum == 1)
        {
            im.itemcharac.exp = im.itemcharac.exp + 2100;   // 현재 경험치
            uiM.expbar.fillAmount = (float)im.itemcharac.exp / (float)im.itemcharac.fullexp; // 경험치 바 (현재, 최대)
            uiM.texpbar.text = im.itemcharac.exp + "/" + im.itemcharac.fullexp;
            im.money = im.money + 5000;
            im.tmoney.text = "    " + im.money;
        }

        if (im.itemcharac.exp == im.itemcharac.fullexp) // 레벨 업
        {
            im.itemcharac.exp = 0;  // 현재 경험지 초기화
            im.itemcharac.fullexp = im.itemcharac.fullexp + 1000; // 경험치 최대치 증가
            uiM.expbar.fillAmount = 0f / (float)im.itemcharac.fullexp; // 경험치 바 초기화 (현재, 최대)
            uiM.texpbar.text = im.itemcharac.exp + "/" + im.itemcharac.fullexp;    // 경험치 바 텍스트 표시
            im.lvup();
        }
        
        if (im.itemcharac.exp > im.itemcharac.fullexp)
        {
            expexp = im.itemcharac.exp - im.itemcharac.fullexp; // 경험치 최대값 넘어간 것 계산
            im.itemcharac.exp = 0;  // 현재 경험지 초기화
            im.itemcharac.fullexp = im.itemcharac.fullexp + 1000; // 경험치 최대치 증가
            uiM.expbar.fillAmount = 0 / (float)im.itemcharac.fullexp; // 경험치 바 초기화 (현재, 최대)
            uiM.expbar.fillAmount += (float)expexp / (float)im.itemcharac.fullexp; // 넘치는 경험치, 바에 추가 (현재, 최대)
            uiM.texpbar.text = expexp + "/" + im.itemcharac.fullexp;    // 경험치 바 텍스트 표시
            im.lvup();
        }
    }
}
