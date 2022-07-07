#ifndef REGISTFACADE_H
#define REGISTFACADE_H
#include "httpcontent.h"
#include "configmanager.h"
#include <ESP8266WiFi.h>
#include <ESP8266WebServer.h>
#include "config.h"
#include "wifiConnector.h"
#include <ESP8266WebServer.h>
class RegistFacade
{
private:
    // wifi仓储类
    ConfigManager configManager;
    // http内容
    HttpContent httpContent;
    WifiConnector wifiConnector;

public:
public:
    RegistFacade();
    ~RegistFacade();
    void Init(String guid, String ssid, String ssid_pwd);
    void HttpServerHandleClient();
    bool HealthCheck(long millionSecond);
};

RegistFacade::RegistFacade()
{
}

RegistFacade::~RegistFacade()
{
}
void RegistFacade::Init(String guid, String ssid, String ssid_pwd)
{
    ConfigManager cm(guid);
    this->configManager = cm;
    this->httpContent.SetConfig(this->configManager);

    bool wifiExist = configManager.Exist();
    //如果wifi不存在，表示需要用户注册新的wifi信息
    if (wifiExist == false)
    {
        this->wifiConnector.StartApMode(ssid, ssid_pwd);
    }
    else
    {
        Serial.println("try login wifi");
        //获得wifi信息并尝试连接
        const Config config = configManager.GetConfig();

        wifiConnector.Init(config);

        bool connectResult = wifiConnector.StartConnect();
        if (connectResult)
        {
            Serial.println("connect wifi successfully");
        }
        else
        {

            Serial.println("connect wifi " + String(config.wifiName) + " error");
            Serial.println("try to clear config and make user reset config");
            configManager.Clear();
            //尝试重启
            ESP.restart();
            return;
        }
        // 尝试连接wifi
    }
}

void RegistFacade::HttpServerHandleClient()
{
    this->httpContent.HttpServerHandleClient();
}

bool RegistFacade::HealthCheck(long millionSecond)
{
    return this->wifiConnector.HealthCheck(millionSecond);
}
#endif // !REGISTFACADE_H