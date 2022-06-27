
#ifndef CONFIG_H
#define CONFIG_H

struct Config
{
    // wifi 名称
    const char *wifiName;
    // wifi 密码
    const char *wifiPwd;
    // 服务器地址
    const char *serviceHost;

    //当前设备昵称
    const char *myName;

    const char *guid;
};

#endif // !CONFIG_H
