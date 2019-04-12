require("View");
require("Model");
require("Controller");

(function(PureMVC){
    let instance = null;
    class Facade{
        //取单例
        static getInstance(){
            if(instance == null){
                instance = new Facade();
            }
            return instance;
        }

        constructor(){
            this.$view = new PureMVC.View();
            this.$model = new PureMVC.Model();
            this.$controller = new PureMVC.Controller();
        }

        registerCommand(commandName, commandClassRef) {
            this.$controller.registerCommand(commandName, commandClassRef);
        }

        removeCommand(commandName) {
            this.$controller.removeCommand(commandName);
        }

        hasCommand(commandName) {
            return this.$controller.hasCommand(commandName);
        }

        sendNotification(key, args) {
            this.$controller.sendNotification(key, args);
        }

        registerProxy(proxy) {
            this.$model.registerProxy(proxy);
        }

        removeProxy(proxyName) {
            return this.$model.removeProxy(proxyName);
        }

        hasProxy(proxyName) {
            return this.$model.hasProxy(proxyName);
        }

        retrieveProxy(proxyName) {
            return this.$model.retrieveProxy(proxyName);
        }

        registerMediator(mediator) {
            this.$view.registerMediator(mediator);
        }

        removeMediator(mediatorName) {
            return this.$view.removeMediator(mediatorName);
        }

        hasMediator(mediatorName) {
            return this.$view.hasMediator(mediatorName);
        }

        retrieveMediator(mediatorName) {
            return this.$view.retrieveMediator(mediatorName);
        }


    }
})(window.PureMVC || (window.PureMVC = {}));
