using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackmove : MonoBehaviour {
    public ingamemanager im;
    private float fMoveSpeed = 5f;  // 총알 이동 속도
    public float timer; // 공격 할떄 시간 체크

	// Use this for initialization
	void Start () {
        im = ingamemanager.Call();
	}
	
	// Update is called once per frame
	void Update () {
        MoveTarget();
        factive();
	}

    private void MoveTarget()
    {
        // 앞으로 이동
        Vector3 vDir = Vector3.forward * fMoveSpeed * Time.deltaTime;
        transform.Translate(vDir);
    }

    public void factive()
    {
        if (gameObject.activeSelf && timer > 1.5f)  // 총알이 hero 빗나 갈때 자동으로 사라짐
        {
            timer = 0.0f;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider _col)
    {
        if (_col.tag == "hero")
        {
            timer = 0.0f;
            im.hpdown(30);   // 포탑 데미지 30
            gameObject.SetActive(false);
        }
    }
}
