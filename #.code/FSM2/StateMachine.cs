public class StateMachine<entity_type> {
    public entity_type type;
    private BaseState<entity_type> currState; //当前状态
    private BaseState<entity_type> lastState; //上一个状态
    private BaseState<entity_type> globalState; //全局状态
    public StateMachine (entity_type e) {
        this.type = e;
        currState = null;
        lastState = null;
        globalState = null;
    }

    /// <summary>
    /// 进入全局状态
    /// </summary>
    public void globalStateEnter () {
        globalState.Enter (type);
    }

    /// <summary>
    /// 设置全局状态
    /// </summary>
    /// <param name="e"></param>
    public void setGlobalState (BaseState<entity_type> e) {
        globalState = e;
        globalState.Target = type;
        globalState.Enter (type);
    }
    /// <summary>
    /// 设置当前状态
    /// </summary>
    /// <param name="e"></param>
    public void setCurrState (entity_type e) {
        currState = e;
        currState.Target = type;
        currState.Enter (type);
    }

    /// <summary>
    /// 状态更新
    /// </summary>
    public void update () {
        if (globalState != null) {
            globalState.Execute (type);
        }
        if (currState != null) {
            currState.Execute (type);
        }
    }
    /// <summary>
    /// 改变状态
    /// </summary>
    /// <param name="e"></param>
    public void changeState (BaseState<entity_type> e) {
        if (e == null) {
            //
        }
        currState.Exit (type); //
        lastState = currState;
        currState = e;
        currState.Target = type;
        currState.Execute (type);
    }

    /// <summary>
    /// 回退到上一个状态
    /// </summary>
    public void toLastState () {
        changeState (lastState);
    }

    public BaseState<entity_type> getLastState () {
        return lastState;
    }

    public BaseState<entity_type> getCurrState () {
        return currState;
    }

    public BaseState<entity_type> getGlobalState () {
        return globalState;
    }
}