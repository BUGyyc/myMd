(function (PureMVC) {
    const proxyMap = {};//保存代理
    class Model {

        registerProxy(proxy) {
            const name = proxy.getProxyName();
            if (proxyMap[name]) {
                throw Error("已存在proxy " + proxy);
            }
            proxyMap[name] = proxy;
            proxy.onRegister();
        }

        removeProxy(proxyName) {
            if(proxyName == null){
                throw Error("proxyName 为空");
            }
            const proxy = proxyMap[proxyName];
            if(proxy){
                delete proxyMap[proxyName];
                proxy.onRemove();
            }
            return proxy;
        }

        hasProxy(proxyName) {
            const proxy = this.retrieveProxy(proxyName);
            return (proxy!=null);
        }
        
        retrieveProxy(proxyName) {
            return proxyMap[proxyName] || null;
        }
    }
    PureMVC.Model = Model;
})(window.PureMVC || (window.PureMVC = {}));