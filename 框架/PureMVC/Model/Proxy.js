require("Notifier");
(function (PureMVC) {
    class Proxy extends PureMVC.Notifier {
        constructor(proxyName) {
            super();
            if (proxyName == null) throw Error("错误 --- proxyName " + proxyName);
            this.proxyName = proxyName;
        }
        
        getProxyName(){
            return this.proxyName;
        }

        onRegister(){}

        onRemove(){}
    }
    PureMVC.Proxy = Proxy;
})(window.PureMVC || (window.PureMVC = {}));