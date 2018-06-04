using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horsemove : MonoBehaviour {
    public ingamemanager im;
    public Animator ani;
    public float timer; // 트리거 2번 발생 해서 시간으로 조절
    public int monsternum;  // 몬스터 순번

	// Use this for initialization
	void Start () {
        im = ingamemanager.Call();
        ani = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (timer < 3)
        {
            timer += Time.deltaTime; // 시간체크
        }
	}

    // 트리거 지역 진입시
    void OnTriggerEnter(Collider _col)
    {   // im.attackbool == true heroattack - 공격 모션 실행시 true (몬스터 붙을때 트리거 실행 방지)
        // timer >= im.attackspeed 한번에 트리거 2번 발생 해서 시간으로 조절 (공격 속도 빨라지면 시간 줄여야 된다)
        if (_col.tag == "sword" && im.attackbool == true && timer >= im.attackspeed && im.binmovetarget == null && monsternum == im.monsternum) // im.binmovetarget == null 캐릭터 지나가는데 트리거 발생 방지, 현재 몬스터 순번 우클릭 몬스터 번호 비교
        {
            horsehp(im.attackskill()); // 데미지 스킬 (공격횟수3 1번공격1)
            im.sattack();
            timer = 0f; // 공격 하면 0으로 초기화
        }

        if (_col.tag == "fire") // 스킬 불
        {
            horsehp(20);
        }
    }

    public void horsehp(int i)
    {
        im.amonsterhp[monsternum] = im.amonsterhp[monsternum] - im.fcritical(i);  // 크리티컬 im.fcritical(i)
        im.fmonsterhpbar(im.amonsterhp[monsternum], 11, false); // 몬스터 공격할때 정보 표시 (현재체력, 기본체력, 죽는거 체크)
        move(); // 맞을때 몬스터 반응 실제 이동은 안함
        if (im.amonsterhp[monsternum] <= 0)
        {
            im.deathlife[monsternum] = false; // 죽는모션 공격 방지
            ingamemanager.Call().diemonster();  // 공격 중지
            hpreset();
            gameObject.SetActive(false);    // 몬스터 사라짐
        }
    }

    public void hpreset()   // 죽으면 다시 hp 복구 안하면 hp줄어든 체로 부활
    {
        im.deathlife[monsternum] = true; // 죽는모션 공격 방지
        im.amonsterhp[monsternum] = 11; // 체력 리셋
        im.fmonsterhpbar(0, 11, true);  // 몬스터 죽을때 정보 사라짐
        im.gring.SetActive(false);
    }

    public void move()
    {
        StopCoroutine("Triggermove");
        StartCoroutine("Triggermove");
    }

    private IEnumerator Triggermove() // IEnumerator 업데이트 처럼 반복 하지만 예약을 반복적으로 걸어서 반복 실행 한다
    {
        while (true)
        {
            ani.SetBool("move", true);

            yield return new WaitForEndOfFrame();
        }
    }
}
