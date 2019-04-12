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

        }
    }
})(window.PureMVC || (window.PureMVC = {}));
