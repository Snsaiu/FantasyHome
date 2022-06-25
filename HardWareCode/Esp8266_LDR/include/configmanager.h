#include <FS.h>
#include "ArduinoJson.hpp"

struct WifiInfo
{
    // wifi名称
    const char *name;
    // wifi密码
    const char *pwd;
};

// 配置信息模型
struct ConfigInfo
{
    // wifi 名称
    const char *wifiName;
    // wifi 密码
    const char *wifiPwd;
    // 服务器地址
    const char *serviceHost;

    //当前设备昵称
    const char *myName;
};

class ConfigManager
{

private:
    const char *wifiFileName = "/wifi.json";
    // 唯一的编号
    const char *guid = "B5A97CB8-59C6-2F14-C2B4-C60E49E82E4A";

public:
    ConfigManager();
    ~ConfigManager();
    /*
    判断是否存在wifi信息
    */
    bool Exist();

    /*
    获得wifi信息
    */
    const WifiInfo GetWifiInformation();

    /*
    获得guid
    */
    const char *GetGuid() const;

    // 保存配置信息
    bool SaveConfig(const ConfigInfo &config);
};

// wifi结构体
