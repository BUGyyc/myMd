using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Slider : MonoBehaviour
{
    private FSMSystem fsmSystem { get; set; }
 
	void Start ()
    {
        fsmSystem = new FSMSystem();
 
        //巡逻状态，在构造参数传一个系统参数，确定该状态是在哪个状态系统中管理的，状态转换的时候调用
        FSMBaseState patrolState = new FSMPatrolState(fsmSystem);
        patrolState.AddTransition(FSMTransition.SeePlayer, FSMStateID.ChaseFSMStateID);//巡逻状态转化条件
 
        //追逐状态
        FSMBaseState chaseState = new FSMChaseState(fsmSystem);
        chaseState.AddTransition(FSMTransition.LeavePlayer, FSMStateID.PatrolFSMStateID);
        
        fsmSystem.AddFSMSate(patrolState);
        fsmSystem.AddFSMSate(chaseState);
	}
	
	void Update ()
    {
        fsmSystem.UpdateSystem();	
	}
}