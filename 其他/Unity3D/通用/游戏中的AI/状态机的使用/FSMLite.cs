/*
 * @Author: delevin.ying 
 * @Date: 2019-05-30 15:33:54 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2019-05-30 15:51:41
 */
using System.Collections.Generic;

public class Test {
    public Test () {
        FSMLite fsmLite = new FSMLite ();
        //设置多个状态
        fsmLite.AddState ("IDLE");
        fsmLite.AddState ("WALK");
        fsmLite.AddState ("RUN");
        fsmLite.AddState ("FLY");
        //加入转换条件，以及触发回调
        fsmLite.AddTranslation ("IDLE", "CLICK", "WALK", onWalk);
        fsmLite.AddTranslation ("WALK", "CLICK", "RUN", this.onRun);
        //启动状态机，默认第一个状态
        fsmLite.Start ("IDLE");
    }

    private void onWalk () {

    }

    private void onRun () {

    }
}
/// <summary>
/// 轻量级的状态机
/// </summary>
public class FSMLite {
    public delegate void FSMCallFunction (params object[] param);
    /// <summary>
    /// 状态
    /// </summary>
    /// <value></value>
    public string State { get; private set; }
    /// <summary>
    /// 存储状态的字典
    /// </summary>
    /// <typeparam name="string"></typeparam>
    /// <typeparam name="FSMState"></typeparam>
    /// <returns></returns>
    private readonly Dictionary<string, FSMState> mStateDict = new Dictionary<string, FSMState> ();
    /// <summary>
    /// 添加状态
    /// </summary>
    /// <param name="name"></param>
    public void AddState (string name) {
        mStateDict[name] = new FSMState (name);
    }
    /// <summary>
    /// 添加转换条件
    /// </summary>
    /// <param name="fromState"></param>
    /// <param name="name"></param>
    /// <param name="toState"></param>
    /// <param name="callFunc"></param>
    public void AddTranslation (string fromState, string name, string toState, FSMCallFunction callFunc) {
        mStateDict[fromState].TranslationDict[name] = new FSMTranslation (fromState, name, toState, callFunc);
    }
    /// <summary>
    /// 开始状态机，并设置起始状态
    /// </summary>
    /// <param name="name"></param>
    public void Start (string name) {
        State = name;
    }

    public void HandleEvent (string name, params object[] param) {
        if (State != null && mStateDict[State].TranslationDict.ContainsKey (name)) {
            FSMTranslation tempTranslation = mStateDict[State].TranslationDict[name];
            tempTranslation.OnTranslationCallback (param);
            State = tempTranslation.toState;
        }
    }
    /// <summary>
    /// 清除
    /// </summary>
    public void Clear () {
        mStateDict.Clear ();
    }

    /// <summary>
    /// 转换条件
    /// </summary> 
    class FSMTranslation {
        public string fromState;
        public string name;
        public string toState;
        public FSMCallFunction OnTranslationCallback;
        public FSMTranslation (string fromState, string name, string toState, FSMCallFunction callFunc) {
            this.fromState = fromState;
            this.name = name;
            this.toState = toState;
            this.OnTranslationCallback = callFunc;
        }
    }

    /// <summary>
    /// 状态
    /// </summary>
    class FSMState {
        private string mName;
        public FSMState (string name) {
            mName = name;
        }
        /// <summary>
        /// 转换条件字典
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <typeparam name="FSMTranslation"></typeparam>
        /// <returns></returns>
        public readonly Dictionary<string, FSMTranslation> TranslationDict = new Dictionary<string, FSMTranslation> ();
    }
}