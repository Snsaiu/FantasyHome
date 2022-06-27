#ifndef WIFICONNECTOR_H
#define WIFICONNECTOR_H
#include "config.h"
#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <WiFiClient.h>
#include <ArduinoJson.h>

class WifiConnector
{
private:
    Config config;
    bool connectedSuccessfully = false;
    WiFiClient client;
    HTTPClient http;

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
    if (WiFi.status() == WL_CONNECTED)
    {
        String router = "http://" + String(this->config.serviceHost) + "/health";
        this->http.begin(client, router);
        StaticJsonDocument<200> doc;
        doc["guid"] = this->config.guid;
        doc["ip"] = client.localIP().toString();
        doc["name"] = this->config.myName;
        String json;
        serializeJson(doc, json);
        http.addHeader("Content-Type", "application/json");
        int httpResponseCode = http.POST(json);
        Serial.println(httpResponseCode);
    }
    else
    {
        Serial.println("can not connect wifi! please check you wifi work statues!");
    }
}

#endif // !WIFICONNECTOR_H
