public class BaseState<entity_type> {
    public entity_type type;
    /// <summary>
    /// 执行某个状态
    /// </summary>
    /// <param name="e"></param>
    public virtual void Execute (entity_type e) {

    }
    /// <summary>
    /// 进入某个状态
    /// </summary>
    /// <param name="e"></param>
    public virtual void Enter (entity_type e) {

    }
    /// <summary>
    /// 退出某个状态
    /// </summary>
    /// <param name="e"></param>
    public virtual void Exit (entity_type e) {

    }
}