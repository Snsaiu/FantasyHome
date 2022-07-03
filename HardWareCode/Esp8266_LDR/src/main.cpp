#include <Arduino.h>
#include "httpcontent.h"
#include "configmanager.h"
#include <ESP8266WiFi.h>
#include <ESP8266WebServer.h>
#include "config.h"
#include "wifiConnector.h"
// wifi仓储类
ConfigManager configManager;

// http内容
HttpContent httpnContent(configManager);

WifiConnector wifiConnector;

//开启ap模式
void startAp()
{
  const char *wifiName = "FantasyHome";
  const char *pwd = "1234567890";
  WiFi.softAP(wifiName, pwd);
  Serial.println("start ap mode");
  Serial.println(WiFi.softAPIP());
}

///注册新的wifi信息
void registNewWifiInfomation()
{
  //开启热点模式
  startAp();
}

void setup()
{

  Serial.begin(9600);

  bool wifiExist = configManager.Exist();
  //如果wifi不存在，表示需要用户注册新的wifi信息
  if (wifiExist == false)
  {
    registNewWifiInfomation();
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
    }
    else
    {

      Serial.println("connect wifi " + String(config.wifiName) + " error");
      Serial.println("try to clear config and make user reset config");
      // configManager.Clear();
      //尝试重启
      ESP.restart();
      return;
    }
    // 尝试连接wifi
  }
}

void loop()
{
  // 开启http服务监听
  httpnContent.HttpServerHandleClient();
  //开始健康检查
  bool checkRes = wifiConnector.HealthCheck(5000);
  if (checkRes)
  {
    Serial.println("health check ok!!!");
  }
  else
  {
    Serial.println("health check error!!!");
    //健康检查失败
  }

  // put your main code here, to run repeatedly:
}