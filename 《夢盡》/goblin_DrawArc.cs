using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class goblin_DrawArc : MonoBehaviour {
	public GameObject target;
	public goblinAI goblin;
	//视野角度
	[Range(0,360)]
	public int viewAngle;
	//视野半径
	public float viewRadius = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		SeeOther ();
	}
	void OnDrawGizmos()
	{
		#if UNITY_EDITOR
		Color color = Handles.color;
		#endif
		//设置画笔颜色
		#if UNITY_EDITOR
		Handles.color = Color.red;
		#endif
		//求起使边
		int angle = viewAngle/2;
		Vector3 startLine = Quaternion.Euler(0,-angle,0) * this.transform.forward;
		//画扇形
		#if UNITY_EDITOR
		Handles.DrawSolidArc(this.transform.position,this.transform.up,startLine,viewAngle,viewRadius);
		#endif
		//恢复颜色
		#if UNITY_EDITOR
		Handles.color = color;
		#endif
	}
		
	void SeeOther()
	{
		//计算距离
		float distance = Vector3.Distance(this.transform.position,target.transform.position);
		//求怪物指向角色的向量
		Vector3 myVector3 = target.transform.position - this.transform.position;
		//计算两个向量的角度
		float angle = Vector3.Angle(myVector3,this.transform.forward);
		//距离小于半径
		if (distance <= viewRadius && angle <=viewAngle/2) {
			//在视线范围内
			goblin.viewFlag = true;
		}else{
			//不在视线范围内
			goblin.viewFlag = false;
		}
	}

}
