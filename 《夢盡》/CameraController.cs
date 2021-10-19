using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class CameraController : MonoBehaviour {
	[SerializeField] SmoothFollow m_smooth;
	float originDistance;

	// Use this for initialization
	void Start () {
		originDistance = m_smooth.Distance;	
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (m_smooth.Target.position, transform.position - m_smooth.Target.position);
		RaycastHit hit;

		float newDistance = originDistance;
		if (Physics.Raycast (ray, out hit)) {
			Vector3 hit_XZPoint = new Vector3 (hit.point.x, m_smooth.Target.position.y, hit.point.z);

			float distance = Vector3.Distance (m_smooth.Target.position, hit_XZPoint);
			if (distance < m_smooth.Distance) {
				newDistance = distance;
			} else {
				newDistance = originDistance;
			}
		}
		m_smooth.Distance = Mathf.Lerp (m_smooth.Distance, newDistance, 0.1f);
	}
}
