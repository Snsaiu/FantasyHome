#include <FS.h>
#include "ArduinoJson.hpp"

struct WifiInfo
{
    // wifi名称
    const char *name;
    // wifi密码
    const char *pwd;
};
class ConfigManager
{

private:
    const char *wifiFileName = "/wifi.json";

public:
    ConfigManager();
    ~ConfigManager();
    /*
    判断是否存在wifi信息
    */
    bool Exist();
    /*
    保存wifi信息
    */
    bool SaveWifiInfomation(WifiInfo info);

    /*
    获得wifi信息
    */
    const WifiInfo GetWifiInformation();
};

// wifi结构体
