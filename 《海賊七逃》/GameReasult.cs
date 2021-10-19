using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReasult : MonoBehaviour
{
    public GameData_Tennis gameData_Tennis;
    public GameObject[] p_b,Wall;
    public GameObject lose;
    public Animator[] pbA;
    // Start is called before the first frame update

    void Update()
    {
        //if (!entetWin) { return; }
        
        //if (Vector3.Distance(player1_tra.position, player1EndPos) > 0.2f && moveOver[0])
        //    player1_tra.position = Vector3.MoveTowards(player1_tra.position, player1EndPos, MoveEndSpeed);
        //else
        //    moveOver[0] = true;
        //if (Vector3.Distance(player2_tra.position, player2EndPos) > 0.2f && moveOver[1])
        //    player2_tra.position = Vector3.MoveTowards(player1_tra.position, player2EndPos, MoveEndSpeed);
        //else
        //    moveOver[1] = true;
    }
    public void Win()
    {
        //entetWin = true;
        foreach (var g in p_b) {
            g.GetComponent<Animator>().applyRootMotion = false;
        }
        foreach (var w in Wall)
        {
            w.SetActive(false);
        }
        foreach (var a in pbA)
        {
            a.applyRootMotion = false;
            a.SetTrigger("Win");
        }
        gameData_Tennis.Common_scr.ShowSettlement(true);
    }
    public void Lose(int n)
    {
        p_b[n].SetActive(false);
    }
    public void Result_Text()
    {
        gameData_Tennis.Common_scr.ShowSettlement(false);
        //lose.SetActive(true);
    }
}
