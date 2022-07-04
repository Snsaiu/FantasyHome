#include "configmanager.h"
#include <ArduinoJson.h>

ConfigManager::ConfigManager(String guid)
{
    this->guid = guid;
    if (SPIFFS.begin())
    {
        Serial.println("spiffs start successfully");
    }
    else
    {
        Serial.println("spiffs start error");
    }
}
ConfigManager::ConfigManager()
{
}
ConfigManager::~ConfigManager()
{
}

bool ConfigManager::Exist()
{
    //判断文件是否存在
    if (SPIFFS.exists(this->configFile))
    {

        return true;
    }

    return false;
}

const Config ConfigManager::GetConfig()
{
    if (this->Exist() == false)
        return Config{};

    File file = SPIFFS.open(this->configFile, "r");
    String content = file.readString();
    Serial.println("find config json" + content);
    const size_t capacity = JSON_OBJECT_SIZE(4) + 120;
    DynamicJsonDocument doc(capacity);
    deserializeJson(doc, content);
    const char *wifiName = doc["wifiName"];
    const char *wifiPwd = doc["wifiPwd"];
    const char *service = doc["serviceHost"];
    const char *servicePort = doc["servicePort"];
    const char *myName = doc["myName"];
    Config info{wifiName, wifiPwd, service, servicePort, myName, this->guid};
    Serial.println("print config infomation:");
    Serial.println(info.myName);
    Serial.println(info.wifiName);
    Serial.println(info.wifiPwd);
    Serial.println(info.serviceHost);
    Serial.println(info.servicePort);
    Serial.println(info.guid);
    file.close();
    return info;
}

String ConfigManager::GetGuid() const
{
    return this->guid;
}

bool ConfigManager::SaveConfig(const Config &config)
{

    if (SPIFFS.exists(this->configFile))
    {
        Serial.println("find config file ,need delete");

        SPIFFS.remove(this->configFile);
        Serial.println("config file delete successfully");
    }

    File f = SPIFFS.open(this->configFile, "w");
    StaticJsonDocument<200> doc;
    doc["wifiName"] = config.wifiName;
    doc["wifiPwd"] = config.wifiPwd;
    doc["serviceHost"] = config.serviceHost;
    doc["servicePort"] = config.servicePort;
    doc["myName"] = config.myName;

    Serial.println("will be save");

    serializeJson(doc, f);
    doc.clear();
    f.close();
    Serial.println("save successfully");
    return true;
}

void ConfigManager::Clear()
{
    if (SPIFFS.exists(this->configFile))
    {
        Serial.println("find config file ,need delete");

        SPIFFS.remove(this->configFile);
        Serial.println("config file delete successfully");
    }
}