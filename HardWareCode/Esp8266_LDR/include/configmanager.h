#ifndef CONFIGMANAGER_H
#define CONFIGMANAGER_H

#include <FS.h>
#include "config.h"

class ConfigManager
{

private:
    const char *configFile = "/config.json";
    // 唯一的编号
    String guid = "B5A97CB8-59C6-2F14-C2B4-C60E49E82E4A";

public:
    ConfigManager();
    ~ConfigManager();
    /*
    判断是否存在wifi信息
    */
    bool Exist();

    /*
     获得配置信息
    */
    const Config GetConfig();
    /*
    获得guid
    */
    String GetGuid() const;

    // 保存配置信息
    bool SaveConfig(const Config &config);

    void Clear();
};

// wifi结构体

#endif // !CONFIGMANAGER_H
