(function (PureMVC) {
    const cmdMap = {};
    class Controller {

        /**
         * 注册Command
         * @param {*} cmd 
         * @param {*} classs 
         */
        registerCommand(cmd, classs) {
            if (cmdMap[cmd]) {
                throw Error('已注册 cmd:' + cmd);
            }
            if (cmd && classs) {
                cmdMap[cmd] = classs;
            } else {
                throw Error("注册出错 " + cmd + " - " + classs);
            }
        }

        /**
         * 移除Command
         * @param {*} cmd 
         */
        removeCommand(cmd) {
            if (cmd && cmdMap[cmd]) {
                delete cmdMap[cmd];
            }
            if (cmd === null) {
                throw Error("移除错误 " + cmd);
            }
        }

        /**
         * 判断是否存在Command
         * @param {*} cmd 
         */
        hasCommand(cmd) {
            return (cmd && cmdMap[cmd]);
        }

        /**
         * 执行
         * @param {*} cmd 
         * @param {*} args 
         */
        sendNotification(cmd, args) {
            const cls = cmdMap[cmd];
            if (cls) {
                const cmd = new cls();
                if (args === void 0) {
                    cmd.execute.call(cmd);
                } else if (args instanceof Array) {
                    cmd.execute.apply(cmd, args);
                } else {
                    cmd.execute.call(cmd, args);
                }
            } else {
                throw Error("执行有错，cls " + cls);
            }
        }
    }
    PureMVC.Controller = Controller;
})(window.PureMVC || (window.PureMVC = {}));