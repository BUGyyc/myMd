(function (PureMVC) {
    const pool = {};
    class Pool {

        /**
         * 获取某个名称的对象池子
         * @param {*} name 
         */
        static getItem(name) {
            const array = pool[name];
            if (array && array.length) {
                return array.pop();
            }
            return null;
        }

        /**
         * 通过名称取对象池中取对象，如果不存在对象的话，直接新建一个
         * @param {*} name 
         * @param {*} cls 
         */
        static getItemByClass(name, cls) {
            return Pool.getItem(name) || new cls();
        }

        /**
         * 回收对象
         * @param {*} name 
         * @param {*} item 
         */
        static recoverItem(name,item){
            if(pool[name]){
                pool[name].push(item);
            }else{
                pool[name] = [item];
            }
        }

        /**
         * 清理某个对象池
         * @param {*} name 
         */
        static clear(name){
            delete pool[name];
        }
    }
    PureMVC.Pool = Pool;
})(window.PureMVC || (window.PureMVC = {}));