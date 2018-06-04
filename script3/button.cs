using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour {
    public uimanager um;
    public soundmanager soundM;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void deal()
    {
        soundM.fbuttonsound();
        switch (um.npcnum)
        {
            case 0: bool b = um.ginventory.activeSelf;   // inventory창 확인
                    um.ginventory.SetActive(!b);
                    bool a = um.gdeal.activeSelf;   // deal창 확인
                    um.gdeal.SetActive(!a);
                break;
            case 1: bool c = um.gdungeon.activeSelf;   // 던전 이동 창 확인
                    um.gdungeon.SetActive(!c);
                break;
            case 2: um.npctext1.text = "말걸지마";
                Debug.Log("2"); // 강화 창
                break;
        }
    }

    public void offmenu()
    {
        soundM.fbuttonsound();
        um.ginventory.SetActive(false); // inventory창 끄기
        um.gdeal.SetActive(false);  // deal창 끄기
        um.gnpcmenu.SetActive(false);   // npc대화창 끄기
        um.gdungeon.SetActive(false);   // 던전 이동 창 끄기
    }
}
