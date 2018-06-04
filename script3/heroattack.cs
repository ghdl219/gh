using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroattack : MonoBehaviour {
    public monstermanager monsterM;
    public ingamemanager im;
    public Animator ani;
    public float timer;
    public bool diebool;

	// Use this for initialization
	void Start () {
        diebool = true;
        ani = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime; // 시간체크
        fdie();
	}

    public void fdie()
    {
        if (im.hp <= 0)
        {
            fherodie();
        }

        if (im.hp <= 0 && diebool == true)
        {
            StopCoroutine("diedie");
            StartCoroutine("diedie");
        }
    }

    public void attack(int i)   // heromove 에서 보내준다 몬스터 우클릭한거 -> i
    {
        timer = 0.0f;
        StopCoroutine("TriggerAttack");
        StartCoroutine("TriggerAttack", i);
    }

    private IEnumerator TriggerAttack(int i) // IEnumerator 업데이트 처럼 반복 하지만 예약을 반복적으로 걸어서 반복 실행 한다
    {
        while (true)
        {
            if (im.binmovetarget == null && Vector3.Distance(monsterM.tmonster[i].transform.position, transform.position) < 0.75f && timer > 0.2f && im.hp > 0) // 몬스터 캐릭터 거리
            {
                ani.SetFloat("heroRun", 0.0f);    // 달리기 그만
                ani.SetFloat("Attackspeed", 0.7f);  // 공격 속도 0.5
                ani.SetBool("attack", true);    // 공격 시작
                im.attackbool = ani.GetBool("attack");  // monstermove에서 공격모션 전 트리거 실행방지 (공격 true)
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void bossattack(int i)   // heromove 에서 보내준다 몬스터 우클릭한거 -> i
    {
        timer = 0.0f;
        StopCoroutine("bossTriggerAttack");
        StartCoroutine("bossTriggerAttack", i);
    }

    private IEnumerator bossTriggerAttack(int i) // IEnumerator 업데이트 처럼 반복 하지만 예약을 반복적으로 걸어서 반복 실행 한다
    {
        while (true)
        {
            if (im.binmovetarget == null && Vector3.Distance(monsterM.tboos[i].transform.position, transform.position) < 0.75f && timer > 0.2f && im.hp > 0) // 보스 몬스터 캐릭터 거리
            {
                ani.SetFloat("heroRun", 0.0f);    // 달리기 그만
                ani.SetFloat("Attackspeed", 0.7f);  // 공격 속도 0.5
                ani.SetBool("attack", true);    // 공격 시작
                im.attackbool = ani.GetBool("attack");  // monstermove에서 공격모션 전 트리거 실행방지 (공격 true)
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void stopattack()    // 공격 중지
    {
        StopCoroutine("TriggerAttack");
        StopCoroutine("bossTriggerAttack");
        StartCoroutine("stpoTriggerAttack");
    }

    public void stpoTriggerAttack()
    {
        ani.SetBool("attack", false);   // 공격 중지
        im.attackbool = ani.GetBool("attack");  // monstermove에서 공격모션 전 트리거 실행방지 (공격 false)
    }

    public void fherodie()
    {
        ani.SetFloat("heroRun", 0.0f);    // 달리기 그만
        ani.SetBool("attack", false);
        StopCoroutine("TriggerAttack");
        StopCoroutine("bossTriggerAttack");
        StartCoroutine("stpoTriggerAttack");
    }

    private IEnumerator diedie() // IEnumerator 업데이트 처럼 반복 하지만 예약을 반복적으로 걸어서 반복 실행 한다
    {
        while (true)
        {
            if (im.hp <= 0) // 몬스터 캐릭터 거리
            {
                diebool = false;
                ani.SetBool("die", true);    // 사망
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void resurrection()
    {
        ani.SetBool("die", false);
        StopCoroutine("bossTriggerAttack");
    }
}
