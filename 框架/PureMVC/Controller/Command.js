require("Notifier")
    (function (PureMVC) {
        class Command extends PureMVC.Notifier {
            excute(...agrs) {

            }
        }
        PureMVC.Command = Command;
    })(window.PureMVC || (window.PureMVC = {}));