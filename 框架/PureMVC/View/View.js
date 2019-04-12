(function (PureMVC) {
    const mediatorMap = {};
    class View {

        registerMediator(mediator){
            const name = mediator.getMediatorName();
            if(mediatorMap[name]){
                throw Error("已存在 --- error");
            }
            mediatorMap[name] = mediator;
            mediator.onRegister();
        }

        removeMediator(mediatorName){
            if(mediatorName == null){
                throw Error("Error 不能为空");
            }
            const mediator = this.retrieveMediator(mediatorName);
            if(mediator){
                delete mediatorMap[mediatorName];
                mediator.onRemove();
            }
            return mediator;
        }

        hasMediator(mediatorName){
            const mediator = mediatorMap.retrieveMediator(mediatorName);
            return (mediator!=null);
        }

        retrieveMediator(mediatorName){
            return mediatorMap[mediatorName] || null;
        }
    }
})(window.PureMVC || (window.PureMVC = {}));