using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FSMPatrolState : FSMBaseState {
    //路径点
    private List<Transform> mStargetPointTransform = new List<Transform> ();
    //路径点索引
    private int mPointIndex = 0;
    //士兵
    private GameObject mSliderObj { get; set; }
    //主角
    private GameObject mPlayerObj { get; set; }
    //士兵移动速度
    private float mMoveSpeed = 4f;

    public FSMPatrolState (FSMSystem fsmSystem) : base (fsmSystem, FSMStateID.PatrolFSMStateID) {

    }

    public override void StateStart () {
        //获取路径点
        Transform[] transforms = GameObject.Find ("Points").GetComponentsInChildren<Transform> ();
        foreach (var m_transform in transforms) {
            if (m_transform != GameObject.Find ("Points").transform) {
                mStargetPointTransform.Add (m_transform);
                Debug.Log (m_transform.position);
            }
        }

        //获取士兵对象
        mSliderObj = GameObject.Find ("Slider");
        //获取主角对象
        mPlayerObj = GameObject.Find ("Player");
    }

    public override void StateEnd () {

    }

    public override void StateUpdate () {
        //确实目标点并移动  
        mSliderObj.transform.LookAt (this.mStargetPointTransform[this.mPointIndex].position);
        mSliderObj.transform.Translate (Vector3.forward * Time.deltaTime * mMoveSpeed);

        if (Vector3.Distance (mSliderObj.transform.position, this.mStargetPointTransform[this.mPointIndex].position) < 0.5f) {
            //切换目标点
            this.mPointIndex++;
            if (this.mPointIndex >= this.mStargetPointTransform.Count) {
                this.mPointIndex = 0;
            }
        }
    }

    public override void TransitionReason () {
        if (Vector3.Distance (mSliderObj.transform.position, mPlayerObj.transform.position) <= 2.0f) {
            //转化状态
            if (this.mFSMSystem == null) {
                Debug.Log ("目标状态机为空");
                return;
            }
            mFSMSystem.TransitionFSMState (FSMTransition.SeePlayer);
        }
    }

}