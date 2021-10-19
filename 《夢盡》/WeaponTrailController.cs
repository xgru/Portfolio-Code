using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xft;

public class WeaponTrailController : MonoBehaviour {
	[SerializeField] XWeaponTrail m_Xweapon;
	// Use this for initialization
	void Start () {
		m_Xweapon.Deactivate ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void StartTrail()
	{
		m_Xweapon.Activate();
	}
	public void StopTrail()
	{
		m_Xweapon.StopSmoothly (0.2f);
	}
}
