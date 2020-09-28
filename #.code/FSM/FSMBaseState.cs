public abstract class FSMBaseState {
    public FSMStateID mStateID { get; set; }
    public FSMSystem mFSMSystem { get; set; }

    public Dictionary<FSMTransition, FSMStateID> mFSMStateIdDic = new Dictionary<FSMTransition, FSMStateID> ();

    public FSMBaseState (FSMSystem fsmSystem, FSMStateID stateID) {
        this.mFSMSystem = fsmSystem;
        this.mStateID = stateID;
    }
    /// <summary>
    /// 增加转换条件
    /// </summary>
    /// <param name="fsmTransition"></param>
    /// <param name="stateID"></param>
    public void AddTransition(FSMTransition fsmTransition,FSMStateID stateID){

    }
    /// <summary>
    /// 删除转换条件
    /// </summary>
    /// <param name="fsmTransition"></param>
    /// <param name="stateID"></param>
    public void DeleteTransition(FSMTransition fsmTransition,FSMStateID stateID){

    }
    /// <summary>
    /// 根据转换条件获得状态ID
    /// </summary>
    /// <param name="fsmTransition"></param>
    /// <returns></returns>
    public FSMStateID GetStateIdByTransition(FSMTransition fsmTransition){

    }
    public abstract void StateStart();
    public abstract void StateUpdate();
    public abstract void StateEnd();
    public abstract void TransitionReason();
}