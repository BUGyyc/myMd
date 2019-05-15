public enum FSMStateID {
    NullFSMStateID,
    PatrolFSMStateID, //巡逻状态
    ChaseFSMStateID, //追逐状态
}

public enum FSMTransition {
    SeePlayer,
    LeavePlayer,
}

public class FSMSystem {
    private FSMStateID mCurrentStateID;
    private FsmBaseState mCurrentState;

    /// <summary>
    /// ID对应上状态
    /// </summary>
    /// <typeparam name="FSMStateID"></typeparam>
    /// <typeparam name="FSMBaseState"></typeparam>
    /// <returns></returns>
    private Dictionary<FSMStateID, FSMBaseState> mFSMStateDic = new Dictionary<FSMStateID, FSMBaseState> ();
    /// <summary>
    /// 添加状态
    /// </summary>
    /// <param name="state"></param>
    public void AddFSMState (FSMBaseState state) {
        if (state == null) {
            return;
        }
        if (mCurrentState == null) {
            mCurrentState = state;
            mCurrentStateID = state.mStateID;
            mCurrentState.StateStart (); //启动？
        }
        if (mFSMStateDic.ContiansKey (state)) { //已经存在
            return;
        }
        mFSMStateDic.Add (state.mStateID, state); //添加
    }
    /// <summary>
    /// 删除状态
    /// </summary>
    /// <param name="state"></param>
    public void DeleteFSMSate (FSMBaseState state) {
        if (state == null) {
            return;
        }
        if (!mFSMStateDic.ContiansKey (state)) {
            return;
        }
        mFSMStateDic.Remove (state.mStateID); //移除
    }
    /// <summary>
    /// 更新
    /// </summary>
    public void UpdateSystem () {
        if (mCurrentState != null) {
            mCurrentState.StateUpdate ();
            mCurrentState.TransitionReason ();
        }
    }
    /// <summary>
    /// 转换状态
    /// </summary>
    /// <param name="transition"></param>
    public void TransitionFSMState (FSMTransition transition) {
        FSMStateID stateID = mCurrentState.GetStateIdByTransition(transition);
        if (stateID != FSMStateID.NullFSMStateID)
        {
            mCurrentStateID = stateID;
            mCurrentState.StateEnd();
            //换状态
            mCurrentState = mFSMStateDic.FirstOrDefault(q => q.Key == stateID).Value;
            mCurrentState.StateStart();
        }
    }

}