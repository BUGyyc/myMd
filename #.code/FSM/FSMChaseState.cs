using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class FSMChaseState : FSMBaseState
{
    private GameObject mPlayerObj { get; set; }
    private GameObject mSliderObj { get; set; }
    private float mSliderMoveSpeed = 6.0f;
 
    public FSMChaseState(FSMSystem fsmSystem) : base(fsmSystem, FSMStateID.ChaseFSMStateID) { }
 
    public override void StateStart()
    {
        mPlayerObj = GameObject.Find("Player");
        mSliderObj = GameObject.Find("Slider");
    }
 
    public override void StateUpdate()
    {
        if (Vector3.Distance(mPlayerObj.transform.position, mSliderObj.transform.position) <= 10.0f)
        {
            //开始面向主角
            mSliderObj.transform.LookAt(mPlayerObj.transform.position);
            //开始追逐
            mSliderObj.transform.Translate(Vector3.forward * Time.deltaTime * mSliderMoveSpeed);
        }
    }
 
    public override void StateEnd()
    {
        
    }
 
    public override void TransitionReason()
    {
        //当主角远离敌人
        if (Vector3.Distance(mPlayerObj.transform.position, mSliderObj.transform.position) > 10.0f)
        {
            //转化状态
            if (this.mFSMSystem == null)
            {
                Debug.Log("目标状态机为空");
                return;
            }
            mFSMSystem.TransitionFSMState(FSMTransition.LeavePlayer);
        }
    }
}