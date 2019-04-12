(function (PureMVC) {
    class Notifier {
        constructor() {
            this.facade = PureMVC.Facade.getInstance();
        }

        sendNotification(key, args) {
            this.facade.sendNotification(key, args);
        }
    }
    SoulGame.Notifier = Notifier;
})(window.PureMVC || (window.PureMVC = {}));