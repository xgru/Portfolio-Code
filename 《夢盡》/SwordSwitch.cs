using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwordSwitch : MonoBehaviour {

	[SerializeField] Animator anim;
	[SerializeField]Transform rightHand,back;

	[SerializeField] GameObject Sword_back,Sword_hand;


	void Start () {
		anim=GetComponent<Animator>();

	}
		

	void Update () {
		rightHand=anim.GetBoneTransform(HumanBodyBones.RightHand);// 獲取右手關節位置，可以查HumanBodyBones 的文檔獲取其他骨骼的Transform
		Sword_hand.transform.parent=rightHand;
		back = anim.GetBoneTransform (HumanBodyBones.Chest);
		Sword_back.transform.parent=back;



		if (Input.GetMouseButtonDown (2)) {
			anim.SetBool ("GetSword", !(anim.GetBool ("GetSword")));
		}
			

	}

	public void GetSword()
	{
		Sword_hand.SetActive (true);
		Sword_back.SetActive (false);
	}

	public void PutSword()
	{
		Sword_hand.SetActive (false);
		Sword_back.SetActive (true);
	}
}
