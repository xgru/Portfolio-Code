using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class goblinAI : MonoBehaviour {
	public Randommission rando;
    [SerializeField] public equidUIcontroll eqhead;
    [SerializeField] public EXPcontroll EXPctrl;
    [SerializeField] Animator m_Animator,player_Animator;
	[SerializeField] Transform m_Canvas;
	public GameObject target,m_DamegeText,my;
	[SerializeField] int goblin_hp;
	//寻路坐标点数组
	public Transform[] points;
	//坐标索引
	public int pointIndex;
	public float moveSpeed;
	//目标
	public bool viewFlag;
	public float attackDis=0.0f;
	bool goblin_dead;
	public MonsterBorn mosb;

	void Start () {
		StartCoroutine (MosterGO ());
		switch (mosb.r) {
		case 1:
			pointIndex = Random.Range (0, 3);
			break;
		case 2:
			pointIndex = Random.Range (4, 7);
			break;
		case 3:
			pointIndex = Random.Range (8, 11);
			break;
		case 4:
			pointIndex = Random.Range (12, 15);
			break;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
				
	void MoveSelf()
	{

		//看向选定坐标点
		Vector3 nextPosition = points[pointIndex].position;
		this.transform.LookAt(nextPosition);
		//前进,自身坐标系，使用Vector3.forward即可
		m_Animator.SetFloat ("Speed", Mathf.Lerp (m_Animator.GetFloat ("Speed"), 0.5f, 0.1f));
		this.transform.Translate(moveSpeed * Vector3.forward * Time.deltaTime,Space.Self);
		//判断距离
		if (Vector3.Distance(this.transform.position,nextPosition)< 0.1) {
			//更换下一个坐标点
			switch (mosb.r) {
			case 1:
				pointIndex = Random.Range (0, 3);
				break;
			case 2:
				pointIndex = Random.Range (4, 7);
				break;
			case 3:
				pointIndex = Random.Range (8, 11);
				break;
			case 4:
				pointIndex = Random.Range (12, 15);
				break;
			}//随机范围{0,1,2,3}
		}
	}

	void FollowTarget()
	{
		m_Animator.SetFloat ("Speed", Mathf.Lerp (m_Animator.GetFloat ("Speed"), 1f, 0.1f));
		//获取调用时目标坐标
		Vector3 targetPostion = target.transform.position;
		if (player_Animator.GetBool ("OnGround")) {
			this.transform.LookAt(targetPostion);
			this.transform.Translate(moveSpeed * Vector3.forward * Time.deltaTime,Space.Self);
		}

	}
	void Attack()
	{
		if (goblin_hp > 0) {
			if (player_Animator.GetBool ("OnGround")) {
				this.transform.LookAt(target.transform.position);
			}
			m_Animator.SetFloat ("Speed", Mathf.Lerp (m_Animator.GetFloat ("Speed"), 0, 0.1f));
			m_Animator.SetTrigger ("attack");
		}
			
	}

	IEnumerator MosterGO()
	{
		while (true) {
			//等待固定帧更新完成
			yield return new WaitForFixedUpdate ();
			float distance = Vector3.Distance (target.transform.position, this.transform.position);
			if (goblin_dead) {
				m_Animator.SetBool ("dead", true);
			
                if (distance > 2) {
                    rando.kill = rando.kill + 1;
                    EXPctrl.EXP = EXPctrl.EXP + (float)(10 + (eqhead.numhead * 0.01)); //每隻怪基礎經驗10  每增加一點+0.01   =往後每打一隻怪得到10.01點經驗
                    my.SetActive (false);
				}
			} else {
				if (viewFlag) {
					//求二者距离
					if (player_Animator.GetBool("Dead")) {
						m_Animator.SetFloat ("Speed", Mathf.Lerp (m_Animator.GetFloat ("Speed"), 0, 0.1f));
						MoveSelf ();
					} else {
						if (distance <= attackDis) {
							Attack ();
							continue;
						}
						FollowTarget ();
						continue;
					}

				}
				//没有看到目标，自由移动
				MoveSelf ();
			}
		}
	}
	int damege;
	void GetDamege(){
		GameObject damegeTextClone = Instantiate<GameObject> (m_DamegeText);
		m_Animator.SetTrigger ("damage");
		if (goblin_hp > 0) {
			damege = (int)(100 + (eqhead.numaxe * 0.01));
            double x;
            //暴擊
            x = Random.Range(0, 100);
            if ((x <= 0 + (eqhead.numleg * 0.01)) )
            {
                damege = 2 * damege;
            }
			goblin_hp -= damege;
		} 
		if (goblin_hp <= 0) {
			goblin_hp = 0;
			goblin_dead = true;

		} else {
			damegeTextClone.transform.parent = m_Canvas;
			damegeTextClone.transform.position = Camera.main.WorldToScreenPoint (transform.position + new Vector3 (0, 2, 0));
			damegeTextClone.GetComponent<UnityEngine.UI.Text> ().text = "-" + damege+"/"+goblin_hp;
			damegeTextClone.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 250), ForceMode2D.Impulse);
			Destroy (damegeTextClone, 1);
		}

	}
		
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			if (m_Animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack") ) {
				if (m_Animator.GetCurrentAnimatorStateInfo (0).normalizedTime <= 0.5f) {
					other.SendMessage ("player_Damege");
				}
			}
		}
	}

}
