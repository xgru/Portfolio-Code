using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {
	[SerializeField] Animator anim;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (anim.GetCurrentAnimatorStateInfo(0).IsName ("Fighting_Move") && Input.GetMouseButton (1)) {
			anim.SetLayerWeight (1, Mathf.Lerp(anim.GetLayerWeight(1),1,0.2f));
		} else {
			anim.SetLayerWeight (1,Mathf.Lerp(anim.GetLayerWeight(1),0,0.2f));
		}
	}
}
