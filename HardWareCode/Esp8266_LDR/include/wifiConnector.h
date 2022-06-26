#ifndef WIFICONNECTOR_H
#define WIFICONNECTOR_H
#include "config.h"
#include <ESP8266WiFi.h>

class WifiConnector
{
private:
    Config config;
    bool connectedSuccessfully = false;
    /* data */
public:
    WifiConnector(const Config &config);

    WifiConnector();
    ~WifiConnector();
    /*
    尝试连接wifi,连接成功返回true，否则返回false
    */
    bool StartConnect();

    /*
    初始化参数
    */
    void Init(const Config &config);

    /*
     健康检查
    */
    void HealthCheck();
};

WifiConnector::WifiConnector(const Config &config)
{
    this->config = config;
}

WifiConnector::WifiConnector()
{
}

WifiConnector::~WifiConnector()
{
}

bool WifiConnector::StartConnect()
{
    WiFi.begin(this->config.wifiName, this->config.wifiPwd);
    int i = 0;
    while (WiFi.status() != WL_CONNECTED)
    {
        delay(1000);

        i++;
        Serial.println("try to reconnecting ..." + i);
        if (i > 10)
        {
            Serial.println("tried 10 ,but connect still error!please validate your wifi name and wifi password!");
            return false;
        }
    }

    this->connectedSuccessfully = true;
    return true;
}

void WifiConnector::Init(const Config &config)
{
    this->config = config;
}

void WifiConnector::HealthCheck()
{
    
}

#endif // !WIFICONNECTOR_H
