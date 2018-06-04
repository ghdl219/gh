using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ingamemanager : MonoBehaviour {
    private static ingamemanager instance = null; // instance 변수로 선언
    public static ingamemanager Call() { return instance; } // .instance 으로 쓸수 있지만 .Call 으로 쓰기 위해 사용
    void Awake() { instance = this; } // Awake 가장 먼저 instance 나자신을 넣는다
    void onDestroy() { instance = null; } // 다시 null로 초기화

    public itemmanager itemM;
    public inventory inventory;
    public uimanager uiM;
    public heroattack heroattack;
    public heromove heromove;
    public monstermanager monsterM;
    public chomonstermake chomake;
    public itemcharacter itemcharac;
    public skillattack skilla;
    public dealitem dealitem;
    public soundmanager soundM;
    public skill sk;
    public message tmessage;

    public GameObject[] npc;    // npc들
    public GameObject hero; // 주인공
    public GameObject movetarget;   // movetarget 들어 있는 변수
    public GameObject binmovetarget;    // 비어있는 변수 (movetarget 이게 들어간다)
    public int[] infomation;    // 캐릭터 정보(숫자)
    public int hp = 100;    // 현재 체력
    public int mp = 100;    // 현재 마력
    public int attack = 10;
    public int def = 0;
    public int critical = 0;
    public int[] amonsterhp;
    public int[] abosshp;

    public Transform cho;   // 첫번째 던전
    public Transform chopo; // 첫번째 던전 위치
    public Transform cho1;  // 두번째 던전
    public Transform chopo1;    // 두전째 던전 위치

    public int dungeonnum;
    public float attackspeed = 0.6f;    // 한번에 트리거 2번 발생 해서 시간으로 조절 공격 속도 빨라지면 시간 줄여야 된다
    public int monsternum;  // 우클릭 한 몬스터 번호
    public bool attackbool; // 공격모션전 트리거 실행 방지
    public int[] wearingflag;
    public Sprite[] imequipment;    // 아이템 이미지 교체 임지 저장 
    public int[] imcharacteritem;   // 장착 아이템 번호 교체 임지 저장
    public int a = 0;   // skill1 번 공격 횟수 체크
    public GameObject[] sword;  // 스킬에 따른 검 색깔  0 평상, 1 스킬
    public Transform tfireskill;    // 스킬2
    public ParticleSystem fireskill;
    public Transform theelskill;    // 스킬3
    public ParticleSystem heelskill;
    public Text tmoney;
    public int money;
    public bool[] deathlife;    // 죽는모션 공격 방지
    public GameObject black;    // 타워 총알
    public Transform[] tblack;
    public GameObject spiderball;   // 거미 총알
    public Transform[] tspiderball;
    public GameObject ring;
    public GameObject gring;    // 몬스터 클릭시 링
    public GameObject ssssss;   // 데미지 표시 하는중 주인공 검에 붙어 있다

	// Use this for initialization
	void Start () {
        money = 0;
        fmoney();
        infomation[0] = hp; // hp 최대값 표시
        infomation[1] = mp; // mp 최대값 표시
        infomation[2] = attack;
        infomation[3] = def;
        infomation[4] = critical;
        for (int i = 0; i < wearingflag.Length; i++)
        {
            wearingflag[i] = 99;    // 아이템 장작 안하면 99
        }

        for (int i = 0; i < tblack.Length; i++) // 포탑 총알
        {
            tblack[i] = Instantiate(black).transform;
            tblack[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < tspiderball.Length; i++)    // 거미 총알
        {
            tspiderball[i] = Instantiate(spiderball).transform;
            tspiderball[i].gameObject.SetActive(false);
        }

        gring = Instantiate(ring);
        gring.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void fmoney()
    {
        tmoney.text = "    " + money; // 돈
    }

    public void skill(int i)
    {
        switch (i)
        {
            case 0: skilla.fskil11();    // 데미지 스킬 (공격횟수3 1번공격0)
                break;
            case 1: skilla.fskillfire();    // 스킬 불
                break;
            case 2: skilla.fheel(); // 힐
                break;
            case 3: fhphp(); // 체력 포션
                break;
            case 4: fmpmp(); // 마나 포션
                break;
        }
    }
    
    public void sattack()   // 평타 공격 후 실행됨
    {
        if (sword[1].gameObject.activeSelf)
        {
            a = a + 1;  // 횟수 체크
        }

        if (a == 3)
        {
            a = 0;
            swordactive(true); // 일반 검
        }
    }

    public int attackskill()    // 평타 공격
    {
        soundM.fswordsound();
        int infomationattack = 0;

        if (sword[0].gameObject.activeSelf) // 일반 검
        {
            infomationattack = infomation[2];
        }

        if (sword[1].gameObject.activeSelf) // 스킬 검
        {
            infomationattack = infomation[2] + (infomation[2] / 10);
        }

        return infomationattack;
    }

    public int fcritical(int i) // 크리티컬
    {
        /*
        int roro = 0;

        if (heromove.transform.rotation.y > -0.5f)  // 데미지 표시 하는중
        {
            roro = 130;
        }
        else
        {
            roro = 0;
        }*/

        //Vector3 v = new Vector3(ssssss.transform.localPosition.x + roro, ssssss.transform.localPosition.y + 130, ssssss.transform.localPosition.z); // 데미지 표시 하는중

        int att = 0;
        if (critical != 0)
        {
            if (Random.Range(0, 100) <= infomation[4])   // 랜덤 0 ~ 100 <= 확률
            {
                att = i * 2;    // 데미지 2배
            }
            else
            {
                att = i;    // 안뜰때 기본 데미지
                //uiM.tdamage.rectTransform.localPosition = v;    // 데미지 표시 하는중
                //uiM.tdamage.text = "" + att;
            }
        }

        if (critical == 0)  // critical 0일때
        {
            att = i;
            //uiM.tdamage.rectTransform.localPosition = v;    // 데미지 표시 하는중
            //uiM.tdamage.text = "" + att;
        }

        return att;
    }

    public void swordactive(bool swsw)  // 스킬에 따른 검의 색 표현
    {
        bool sw = swsw;
        if (sw)
        {
            sword[0].gameObject.SetActive(true);    // 기본 칼
            sword[1].gameObject.SetActive(false);
        }
        else if(!sw)
        {
            sword[0].gameObject.SetActive(false);
            sword[1].gameObject.SetActive(true);    // 스킬 사용 칼
        }
    }

    public void characteritem(int i, int z, int itemsale) // 아이템 교체 아이템순번, 인벤토리 순번
    {
        if (uiM.ilv >= uiM.filv)    // 장비 교체 레벨 비교
        {
            itemcharac.itemdecreasenum(i);  // 능력치 다운
            imequipment[i] = itemM.Iitemimageslotdeal[itemcharac.characteritem[i]]; // itemcharac.characteritem[i] -> 캐릭터 창에 있는 아이템 순번
            imcharacteritem[i] = itemcharac.characteritem[i];
            itemcharac.equipment[i].sprite = null;  // 이미지 제거
            item(itemsale);
            inventory.iminventoryslot(i, z); // 인벤토리로 넣어 준다
        }
        else
        {
            tmessage.fmessage(1);
            Debug.Log("레벨 부족 합니다");
        }
    }

    public void item(int z)
    {
        if (uiM.gitemcharacter.activeSelf)
        {
            uiM.itemwearing(z);   // 인벤토리 순번
        }
        inventory.inventoryitemsale(z); // 인벤토리 순번
    }

    public int inventoryitemsale(int i) // 아이템 착용 할때 sale에서 사용
    {
        int a=0;
        a = inventory.inventorynum[i];  // 아이템 전체 순번
        return a;
    }

    public void inventoryslot(int i)    // 아이템 장착 해제
    {
        wearingflag[i] = 99;
        itemcharac.itemdecreasenum(i);  // 능력치 다운
        itemcharac.equipment[i].sprite = null;  // 이미지 제거
        inventory.inventoryslot(i); // 인벤토리로 넣어 준다
    }
    
    public void itembuy(int i)  // buy.cs에서 보내준다 ,아이템 사기
    {
        dealitem.fitemtext(i);  // 이거 실행해서 itemint여기에 값 넣어 준다
        if (dealitem.itemint <= money)
        {
            invenitem(i, itemM.pageindex);  // 페이지 번호 넘겨 주기 위해
            money = money - dealitem.itemint;
            tmoney.text = "    " + money;
        }
        else
        {
            tmessage.fmessage(0);
            Debug.Log("돈이 부족 합니다");
        }
    }

    public void invenitem(int itemnum, int pagenum) // itemnum 아이템 번호, pagenum 페이지 번호
    {
        inventory.inventoryitembuy(itemnum, pagenum);
    }

    public void fitemsale(int i)
    {
        if (uiM.gdeal.activeSelf)
        {
            int itemindex = i;
            int z = 1;
            if (i > 5)
            {
                itemindex = i - 5;
                z = 2;
            }
            if (i > 11)
            {
                itemindex = i - 11;
                z = 3;
            }
            dealitem.saleitemtext(itemindex, z);  // 이거 실행해서 itemint여기에 값 넣어 준다
            money = money + dealitem.itemint;
            tmoney.text = "    " + money;
        }
    }

    public void itemsale(int i)  // sale.cs에서 보내준다, 아이템 판매
    {
        inventory.inventoryitemsale(i); // 인벤토리 순번
    }

    public void itemwearing(int i)  // 아이템 착용
    {
        if (uiM.gitemcharacter.activeSelf)
        {
            if (uiM.ilv >= uiM.filv)    // 장비 착용 레벨 비교
            {
                uiM.itemwearing(i);   // 인벤토리 순번
            }
            else
            {
                tmessage.fmessage(1);
                Debug.Log("레벨 부족");
            }
        }
    }

    public void tooltip(int i, bool flag)  // buy.cs에서 보내준다, 마우스오버 아이템 정보보기, 아이템 착용 안햇는데 수치 올라가는거 방지 false
    {
        if (i >= 3 && itemM.pageindex == 3)
        {
            
        }
        else
        {
            uiM.uitooltip(i, itemM.pageindex, flag);  // 상점 아이템의 순번과 페이지 번호
        }
    }

    public void tooltipout()    // buy.cs에서 보내준다, 마우스오버아웃
    {
        uiM.itooltip.gameObject.SetActive(false);   // 아이템 툴립 끄기
    }

    public void inventooltip(int i)
    {
        uiM.uitooltip(i, 99, false);   // 상점 아이템의 순번과 99 플레그 값 아이템 착용 안햇는데 수치 올라가는거 방지 false
    }

    public void charactertooltip(int i) // 캐릭터창 아이템 툴팁
    {
        if (itemcharac.itemability[i] != "")    // 아이템 장착 햇을때만 툴팁 뜨게
        {
            int a = 0;
            a = itemcharac.characteritem[i];    // 아이템 순번을 가져온다
            uiM.uitooltip(a, 100, false);
        }
    }

    public void inventooltipout()
    {
        uiM.itooltip.gameObject.SetActive(false);   // 아이템 툴립 끄기
    }

    public void dungeonmove(int i)  // 던전 선택
    {
        switch (i)
        {
            case 0: dungeonnum = i;
                uiM.idungeonimage[i].color = new Color(10, 60 / 100, 5 / 10);   // 선택 빨강
                uiM.idungeonimage[i+1].color = new Color(255, 255 / 255, 255 / 255);    // 선택 해제 흰색
                break;
            case 1: dungeonnum = i;
                uiM.idungeonimage[i].color = new Color(10, 60 / 100, 5 / 10);
                uiM.idungeonimage[i-1].color = new Color(255, 255 / 255, 255 / 255);
                break;
        }
    }

    public void dungeonbutton() // 던전 이동 버튼
    {
        soundM.fbuttonsound();
        bool a = false;
        switch (dungeonnum)
        {
            case 0: hero.transform.position = chopo.transform.position; // 초원 던전
                    chomake.monstermake(0);
                    uiM.idungeonimage[0].color = new Color(255, 255 / 255, 255 / 255);    // 선택 해제 흰색
                    a = true;
                break;
            case 1: if (uiM.ilv >= 3)   // 렙제 3
                {
                    hero.transform.position = chopo1.transform.position;    // ??? 던전
                    chomake.monstermake(10);
                    uiM.idungeonimage[1].color = new Color(255, 255 / 255, 255 / 255);
                    a = true;
                }
                else
                {
                    tmessage.fmessage(3);
                    Debug.Log("갈수 없습니다");
                    a = false;
                }
                break;
        }

        if (a == true)  // 던전 매뉴창이 무조건 사라지는것 방지
        {
            uiM.gdungeon.SetActive(false);  // 던전 선택 창
            uiM.gnpcmenu.SetActive(false);  // npc메뉴 창
        }
    }

    public void diemonster()    // 공격 중지
    {
        heroattack.stopattack();
    }

    public void resetcho(int i)
    {
        monsterM.resetmon(i);
    }

    public void heropo()    // 보스 잡았을때 돌아가기
    {
        Vector3 zore = new Vector3(0,0,0);
        hero.transform.position = zore;
    }

    public void hpdown(int i)   // 몬스터 한테 맞을때
    {
        i = i - def;
        if (i <= 0)
        {
            i = 0;
        }
        hp = hp - i;    // 현재 체력

        itemcharac.hpmp(hp,99); // 99는 실행 안됨
        uiM.hpbar.fillAmount -= (float)i / (float)infomation[1];  // 체력 바 (현재 체력, 기본 체력)
    }

    public bool mpdown(int i)
    {
        int mpmp = mp;  // mp 딱맞게 0까지 사용할때 실행이 안됨
        bool b = false;
        mpmp = mp - i;

        if (0 > mpmp) // 최대값 현재 마나 비교
        {
            tmessage.fmessage(2);
            Debug.Log("마나 없습니다");
            b = false;
        }
        else if (0 <= mpmp)   // mp 딱맞게 0까지 -> || mp == 10 && i == 10
        {
            mp = mp - i;
            b = true;
            itemcharac.hpmp(99, mp); // 99는 실행 안됨
            uiM.mpbar.fillAmount -= (float)i / (float)infomation[1];  // 마나 바 (현재 마나, 기본 마나)
        }

        return b;
    }

    public void fhphp()  // 체력 포션
    {
        hp = hp + 50;   // 힐량 50
        if (infomation[0] < hp) // 최대값 현재 체력 비교
        {
            hp = infomation[0];
        }
        itemcharac.hpmp(hp, 99); // 99는 실행 안됨
        uiM.hpbar.fillAmount = (float)hp / (float)infomation[1]; // 체력 바 (현재 체력, 기본 체력)
    }

    public void fmpmp()  // 마나 포션
    {
        mp = mp + 50;   // 마나 50
        if (infomation[1] < mp) // 최대값 현재 체력 비교
        {
            mp = infomation[1];
        }
        itemcharac.hpmp(99, mp); // 99는 실행 안됨
        uiM.mpbar.fillAmount = (float)mp / (float)infomation[1]; // 마나 바 (현재 마나, 기본 마나)
    }

    public void keydownup(bool bkey)    // 마우스 클릭
    {
        Vector3 vscale = new Vector3(0.8f, 0.8f, 0);    // 작아 질때 크기
        if (bkey)
        {
            uiM.tautoattack.transform.localScale = vscale;  // 작아 진다 (클릭)
            if (hp > 0)
            {
                heromove.fattack(); // 자동 공격
            }
        }

        if (bkey == false)
        {
            vscale.x = 1f;
            vscale.y = 1f;
            uiM.tautoattack.transform.localScale = vscale;  // 커진다 (업)
        }
    }

    public void ftmonsterhpbar(int i, int a)    // 몬스터 클릭시 체력 표시 (현재 체력, 기본체력)
    {
        uiM.tmonsterhpbar.text = i + " / " + a;
        uiM.monsterhpbar.fillAmount = (float)i / (float)a; // 몬스터 체력 바 (현재 체력, 기본체력)
    }

    public void fmonsterhpbar(int i, int a, bool z) // 몬스터 공격할때
    {
        if (z == true)  // 몬스터 죽으면 몬스터 정보 사라짐
        {
            uiM.topimage.gameObject.SetActive(false);
        }
        uiM.monsterhpbar.fillAmount = (float)i / (float)a; // 몬스터 체력 바 (현재 체력, 기본체력)
        uiM.tmonsterhpbar.text = i + " / " + a;
    }

    public void fclear(int clearnum)
    {
        soundM.fbuttonsound();
        if (clearnum == 0)
        {
            uiM.clear.SetActive(false);
            heropo();      // 1보스 죽으면 초기 위치로이동
        }
        if (clearnum == 1)
        {
            uiM.clear1.SetActive(false);
            heropo();      // 2보스 죽으면 초기 위치로이동
        }
    }

    public void lvup()  // 레벨 업
    {
        infomation[0] = infomation[0] + 50; // hp 최대값 표시
        infomation[1] = infomation[1] + 50; // mp 최대값 표시
        infomation[2] = infomation[2] + 5;  // 공
        infomation[3] = infomation[3] + 5;  // 방
        infomation[4] = infomation[4] + 5;  // 크리티컬

        uiM.ilv = uiM.ilv + 1;
        uiM.lvup.text = "LV UP " + uiM.ilv;  // 던전1 종료시 렙업 하면
        uiM.lvup2.text = "LV UP " + uiM.ilv;  // 던전2 종료시 렙업 하면
        uiM.herolv.text = "레벨 : " + uiM.ilv;
        itemcharac.infomation[0].text = "체력 : " + infomation[0] + " / " + hp;    // 캐릭터 창에 나타나는 정보
        itemcharac.infomation[1].text = "마력 : " + infomation[1] + " / " + mp;
        itemcharac.infomation[2].text = "공격력 : " + infomation[2];
        itemcharac.infomation[3].text = "방어력 : " + infomation[3];
        itemcharac.infomation[4].text = "크리티컬 : " + infomation[4];

        uiM.hpbar.fillAmount = (float)hp / (float)infomation[0];
        uiM.mpbar.fillAmount = (float)mp / (float)infomation[1];
    }

    public void ftskill(string t, int i, Transform tran)    // 스킬 설명, 스킬 번호, 스킬 위치
    {
        Vector3 y = new Vector3(tran.transform.localPosition.x - 60.0f, tran.transform.localPosition.y + 150.0f, tran.transform.localPosition.z);   // 일반 스킬

        if (i >= 3)
        {
            y = new Vector3(tran.transform.localPosition.x, tran.transform.localPosition.y + 80.0f, tran.transform.localPosition.z);    // 물약 위치 다름
        }

        uiM.tskill.rectTransform.localPosition = y; // 툴팁 위치
        uiM.tskill.text = t;    // 툴팁 내용
    }

    public void fdropout()
    {
        uiM.tskill.text = "";   // 마우스 벗어 날때 초기화
    }

    public void fdierestart()   // 캐릭터 사망
    {
        resetcho(2);
        heropo();
        fhpmpreset();
    }

    public void fhpmpreset()
    {
        Debug.Log(infomation[0]);
        Debug.Log(infomation[1]);
        hp = infomation[0]; // hp 최대값 표시
        mp = infomation[1]; // mp 최대값 표시

        itemcharac.infomation[0].text = "체력 : " + infomation[0] + " / " + hp;    // 캐릭터 창에 나타나는 정보
        itemcharac.infomation[1].text = "마력 : " + infomation[1] + " / " + mp;

        uiM.hpbar.fillAmount = (float)hp / (float)infomation[0];
        uiM.mpbar.fillAmount = (float)mp / (float)infomation[1];

        uiM.herodie.SetActive(false);
        heroattack.diebool = false;
        heroattack.resurrection();
    }
}
