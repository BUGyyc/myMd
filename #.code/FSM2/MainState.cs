public class MainState : BaseState<Main> {
    public static MainState instance;

    public static MainState getInstance () {
        if (instance == null) instance = new MainState ();
        return instance;
    }

    public override void Enter (Main e) {

    }

    public override void Execute (Main e) {

    }

    public override void Exit (Main e) {

    }
}