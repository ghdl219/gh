using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraman : MonoBehaviour {
    public Transform hero;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        cameramoving();
	}

    public void cameramoving()
    {
        Vector3 gap = hero.position - transform.position;
        gap /= 8f;
        transform.position += gap;
    }
}
