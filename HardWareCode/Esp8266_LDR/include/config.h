
#ifndef CONFIG_H
#define CONFIG_H

struct Config
{
    // wifi 名称
    String wifiName;
    // wifi 密码
    String wifiPwd;
    // 服务器地址
    String serviceHost;
    //服务器端口
    String servicePort;

    //当前设备昵称
    String myName;

    String guid;

    String mqttServer;

    String mqttPort;

    String mqttTopic;
};

#endif // !CONFIG_H
