using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {
	 Animator anim;
	int a1=0,a2=0;
	// Use this for initialization
	void Start () {
		anim = transform.root.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (anim.GetBool ("GetSword") && anim.GetLayerWeight(1)<0.1f) {
			
			if (Input.GetKeyDown (KeyCode.F1)) {
				a1++;
				anim.SetTrigger ("Attack");
				switch ((a1 % 4)) {
				case 1:
					anim.SetFloat ("attack", 0);
					break;
				case 2:
					anim.SetFloat ("attack", 0.5f);
					break;
				case 3:
					anim.SetFloat ("attack",1);
					break;
				case 0:
					anim.SetFloat ("attack",1.5f);
					break;
				}
			}
			else if (Input.GetKeyDown (KeyCode.F2)) {
				a2++;
				anim.SetTrigger ("Attack");
				switch ((a2 % 3)) {
				case 1:
					anim.SetFloat ("attack",-0.5f);
					break;
				case 2:
					anim.SetFloat ("attack", -1);
					break;
				case 0:
					anim.SetFloat ("attack",-1.5f);
					break;
				}
			}
		}

	}
	void OnTriggerEnter(Collider other){
		if (other.tag == "Enemy") {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack") ) {
				if (anim.GetCurrentAnimatorStateInfo (0).normalizedTime <= 0.5f) {
					other.SendMessage ("GetDamege");
				}
			}
		}
	}
}

