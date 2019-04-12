require("Notifier");
(function (PureMVC) {
    class Mediator extends PureMVC.Notifier {
        constructor(mediatorName, viewComponent) {
            super();
            if (mediatorName == null) {
                throw Error("error ");
            }
            if (viewComponent == null) {
                throw Error("error ");
            }
            this.mediatorName = mediatorName;
            this.viewComponent = viewComponent;
        }

        getMediatorName() {
            return this.mediatorName;
        }

        getViewComponent() {
            return this.viewComponent;
        }

        setViewComponent(viewComponent) {
            this.viewComponent = viewComponent;
        }

        onRegister() { }

        onRemove() { }
    }
})(window.PureMVC || (window.PureMVC = {}));