{
    let wxLogin = function () {
        //是否在微信里面
        if (Laya.Browser.onMiniGame) {
            //微信登录
            Laya.Browser.window.wx.login({
                success: (res) => {
                    if (res.code) {
                        this.m_wxCode = res.code;
                    }
                    else {
                        MessageBoxManager.getInstance().ShowMessageBox(MBType.OK, "拉取微信用户代码失败");
                        return;
                    }
                    //去微信查询是否已经授权
                    Laya.Browser.window.wx.getSetting(
                        {
                            success: (res) => {
                                var bAuthUserInfo = res.authSetting['scope.userInfo'];
                                //如果没有授权过，要弹出授权按钮
                                if (!bAuthUserInfo) {
                                    let startBtn = Laya.Browser.window.wx.createImage();
                                    startBtn.src = "StartScene/startGameButton.png";
                                    var button = Laya.Browser.window.wx.createUserInfoButton(
                                        {
                                            type: 'image',
                                            image: startBtn.src,
                                            style:
                                            {
                                                left: wx.getSystemInfoSync().windowWidth / 2 - 277 / 4,
                                                top: wx.getSystemInfoSync().windowHeight - 100,
                                                width: 277 / 2,
                                                height: 77 / 2,
                                            }
                                        })
                                    button.onTap((res) => {
                                        if (res.errMsg == "getUserInfo:ok") {
                                            Debug.log("获得授权用户信息")
                                            //获取到用户信息
                                            this.m_wxNickName = res.userInfo.nickName
                                            this.m_wxAvatarUrl = res.userInfo.avatarUrl
                                            this.m_hasGetUserInfo = true;
                                            //清除微信授权按钮
                                            button.destroy();
                                            //得到了微信用户信息就直接登录
                                            this.login();
                                        }
                                        else {
                                            TipManager.instance.ShowTip("开始游戏前，请先允许授权！");
                                        }
                                    })
                                    button.show()
                                }
                                else {
                                    Laya.Browser.window.wx.getUserInfo(
                                        {
                                            success: (res) => {
                                                //获取到用户信息
                                                this.m_wxNickName = res.userInfo.nickName
                                                this.m_wxAvatarUrl = res.userInfo.avatarUrl
                                                this.m_hasGetUserInfo = true;
                                                this.login();
                                            }
                                            ,
                                            fail: () => {
                                                TipManager.instance.ShowTip("获取用户信息失败");
                                                //尝试直接登录
                                                this.m_hasGetUserInfo = true;
                                                this.login();
                                            }
                                            ,
                                            complete: () => {

                                            }
                                        });
                                }
                            }
                            ,
                            fail: () => {
                                MessageBoxManager.getInstance().ShowMessageBox(MBType.OK, "获取微信授权信息失败!");
                                //尝试直接登录
                                this.m_hasGetUserInfo = true;
                                this.login();
                            }
                            ,
                            complete: () => {

                            }
                        }
                    );
                }
                ,
                fail: () => {
                    MessageBoxManager.getInstance().ShowMessageBox(MBType.OK, "获取用户微信信息失败!");
                }
            });
        }
        else {
            this.m_wxNickName = this.m_wxCode;
            this.m_wxAvatarUrl = "";
            this.m_hasGetUserInfo = true;
            GlobalData.isWeixinEnv = false;
            //不是微信环境，准备好了就直接登录
            this.login();
        }
    }

    let login = function () {
        if (GlobalData.isServerConnected == false || this.m_hasGetUserInfo == false) {
            return;
        }
        var req = new LoginReq();
        req.code = this.m_wxCode;
        req.nickName = this.m_wxNickName;
        req.headUrl = this.m_wxAvatarUrl;
        req.connectType = this.m_connecntType;
        req.channel = GlobalData.Channel;
        req.launcher = GlobalData.Launcher;
        req.version = GlobalData.Version;
        NetManager.getInstance().Send(OpCode.ACCOUNT, AccountCode.LOGIN_REQ, req, true);
    }
}