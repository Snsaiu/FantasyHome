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

    // ap 模式的热点名称
    String ssid = "FantasyHome";
    // ap 模式的热点密码
    String ssid_pwd = "1234567890";

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
     健康检查,millionSecond：多少秒进行健康检查
    */
    bool HealthCheck(long millionSecond);

    void StartApMode(String ssid, String pwd);

    void StartApMode();
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

    Serial.println("start connect wifi!");
    Serial.println("wifi:");
    Serial.println(this->config.wifiName);
    Serial.println("wifi password:");
    Serial.println(this->config.wifiPwd);
    WiFi.mode(WIFI_STA);
    WiFi.begin(this->config.wifiName, this->config.wifiPwd);
    int i = 0;
    while (WiFi.status() != WL_CONNECTED)
    {
        delay(1000);

        i++;
        Serial.println("try to reconnecting ...");
        if (i > 10)
        {
            Serial.println("tried 10 ,but connect still error!please validate your wifi name and wifi password!");
            return false;
        }
    }

    Serial.println("");                        // WiFi连接成功后
    Serial.println("Connection established!"); // NodeMCU将通过串口监视器输出"连接成功"信息。
    Serial.print("IP address:    ");           // 同时还将输出NodeMCU的IP地址。这一功能是通过调用
    Serial.println(WiFi.localIP());
    this->connectedSuccessfully = true;
    return true;
}

void WifiConnector::Init(const Config &config)
{
    this->config = config;
}

bool WifiConnector::HealthCheck(long millionSecond)
{

    if (WiFi.status() == WL_CONNECTED)
    {

        String router = "http://" + this->config.serviceHost + ":5000" + "/api/sensor-device/check-health";
        //  String router = "http://www.baidu.com";
        Serial.println("request address:");
        Serial.println(router);

        http.begin(client, router);
        StaticJsonDocument<200> doc;
        doc["guid"] = this->config.guid;
        doc["ip"] = WiFi.localIP();
        doc["name"] = this->config.myName;
        String json;
        serializeJson(doc, json);
        Serial.println("send data:");
        Serial.println(json);
        http.addHeader("Content-Type", "application/json");

        int httpResponseCode = http.POST(json);
        Serial.println(httpResponseCode);
        String responsePayload = http.getString();
        Serial.println(responsePayload);
        delay(millionSecond);
        http.end();
        return true;
    }
    else
    {
        delay(1000);
        Serial.println("can not connect wifi! please check you wifi work statues!");
        return false;
    }
}

void WifiConnector::StartApMode(String ssid, String pwd)
{

    WiFi.softAP(ssid, pwd);
    Serial.println("start ap mode");
    Serial.println(WiFi.softAPIP());
}
void WifiConnector::StartApMode()
{

    WiFi.softAP(this->ssid, this->ssid_pwd);
    Serial.println("start ap mode");
    Serial.println(WiFi.softAPIP());
}

#endif // !WIFICONNECTOR_H
