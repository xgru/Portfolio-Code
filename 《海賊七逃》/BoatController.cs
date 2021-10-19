using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [Header("目前控制器")]
    public PlayerControl_ScriptObject playerControl_scrObj;
    public static BoatController instance;

    GameObject[] Boat,Boat2;
    public GameObject win;
    //public GameObject All_player;
    ObjectPool OP;
    ObjectCreate OC;
    public float  time1, time2;
    // Start is called before the first frame update

    public GameCommon gameCommon_scr;
    [SerializeField]
    private Transform nowBoat;//目前角色的船

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        OP = ObjectPool.instance;
        OC = ObjectCreate.instance;

        //Boat = GameObject.FindGameObjectsWithTag("Boat");
        //Boat2 = GameObject.FindGameObjectsWithTag("Boat2");
        Transform[] _obj_array = gameObject.GetComponentsInChildren<Transform>();
        for (int i = 0; i < _obj_array.Length; i++)
        {
            Transform _obj = _obj_array[i];
            if (_obj.tag == "Boat" || _obj.tag == "Boat2")
            {
                nowBoat = _obj;
                break;
            }
        }
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (OP == null) { Debug.LogWarning(gameObject.name + "\n" + "OP為空"); return; }
        if (OC == null) { Debug.LogWarning(gameObject.name + "\n" + "OC為空"); return; }
        if (!OC.isChange)
        {
            if (OC.game_time != 0)
            {
                /*
                if (gameObject.name == "P1_1" || gameObject.name == "P1_2" || gameObject.name == "P1_3")
                {
                    Move1();
                }
                else if (gameObject.name == "P2_1" || gameObject.name == "P2_2" || gameObject.name == "P2_3")
                {
                    Move2();
                }*/
                if (gameObject.name.Contains("P"))
                {
                    Move();
                }
            }
            else
            {
                //Boat[0].transform.rotation = Quaternion.Euler(0, 0, 0);
                //Boat[1].transform.rotation = Quaternion.Euler(0, 0, 0);
                nowBoat.rotation = Quaternion.Euler(0, 0, 0);
            }
            
        }

        time1 += .02f;
        time2 += .02f;
        time1 = Mathf.Clamp(time1, 0f, 3f);
        time2 = Mathf.Clamp(time1, 0f, 3f);
        if (time1 == 3f)
        {
            OP.speed1 = 17f;
            time1 = 0;
        }
        if (time2 == 3f)
        {
            OP.speed2 = 17f;
            time2 = 0;
        }
    }

    private float _angle = 0;//旋轉角度

    void Move()
    {
        float _moveXpos = 0;
        float _moveZpos = 0;
        float _distance = 0;//方向
        float _value = 1.8f;//移動值

        //通用事件
        System.Action _action = () =>
        {
            if (_moveZpos > 0)
                _angle = 45;
            else if (_moveZpos < 0)
                _angle = 135;
            else
                _angle = 90;
        };

        if (PlayerControl_koroshi.Key(playerControl_scrObj.keyUp))
        {
            _moveZpos = Vector3.forward.z * OP.speed1 * Time.deltaTime;
            TwiceKeyDown(playerControl_scrObj.keyUp);
            _angle = 0;
        }
        else if (PlayerControl_koroshi.Key(playerControl_scrObj.keyDown))
        {
            _moveZpos = Vector3.back.z * OP.speed1 * Time.deltaTime;
            _angle = 180;
            TwiceKeyDown(playerControl_scrObj.keyDown);
        }
        if (PlayerControl_koroshi.Key(playerControl_scrObj.keyLeft))
        {
            _moveXpos = Vector3.left.x * OP.speed1 * Time.deltaTime;
            _distance = -1;
            _action();
            _angle = _angle * _distance;
            TwiceKeyDown(playerControl_scrObj.keyLeft);
        }
        else if (PlayerControl_koroshi.Key(playerControl_scrObj.keyRight))
        {
            _moveXpos = Vector3.right.x * OP.speed1 * Time.deltaTime;
            _distance = 1;
            _action();
            _angle = _angle * _distance;
            TwiceKeyDown(playerControl_scrObj.keyRight);
        }


        //旋轉
        nowBoat.rotation = Quaternion.Lerp(nowBoat.rotation, Quaternion.Euler(0, _angle, 0), 0.3f);

        transform.Translate(_moveXpos * _value, 0, _moveZpos * _value);

    }

    public void Result_Text()
    {
       //win.SetActive(true);
        gameObject.SetActive(false);
    }
}
