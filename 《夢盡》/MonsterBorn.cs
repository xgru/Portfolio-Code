using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class MonsterBorn : MonoBehaviour {

    public GameObject[] monsters; //要生的怪物
    public float Timer = 2.0f; //生成間隔
	public float Timers = 10.0f; //生成間隔
    public Transform[] Tran_CreatPoints;//物件要生成的位置
    public Vector3 V3_Random;//隨機生成位置
    int x; //要生幾隻
	public int r=1,r1=0,rr;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Timers -= Time.deltaTime;  
		if (Timers <= 0) {
			x = 10;
			while (x > 0)
			{
				switch (r) {
				case 1:
					rr = Random.Range (0, 3);
					break;
				case 2:
					rr = Random.Range (4, 7);
					break;
				case 3:
					rr = Random.Range (8, 11);
					break;
				case 4:
					rr = Random.Range (12, 15);
					break;
				}
				V3_Random = Tran_CreatPoints[rr].position + new Vector3(Random.Range(0f, 5.5f), Random.Range(0, 0), Random.Range(0f, 5.5f));
				Timer -= Time.deltaTime;
				if (Timer <= 0)
				{
					r1 = Random.Range (0, 4);
					GameObject Obj_Clone = Instantiate(monsters[r1], V3_Random, Quaternion.identity) as GameObject;
					x = x - 1;
					Timer = 2;
				}

			}
			Timers = 10;
		}

    }

    

    

}
