#include <Arduino.h>
#include "httpcontent.h"
#include "configmanager.h"
#include <ESP8266WiFi.h>
#include <ESP8266WebServer.h>

// wifi仓储类
ConfigManager configManager;
// http内容
HttpContent httpnContent;

// http服务
ESP8266WebServer httpServer(80);

//开启ap模式
void startAp()
{
  const char *wifiName = "FantasyHome";
  const char *pwd = "1234567890";
  WiFi.softAP(wifiName, pwd);
  Serial.println("start ap mode");
  Serial.println(WiFi.softAPIP());
}

//热点模式下的进入根目录
void handleRoot()
{
  httpServer.send(200, "text/html", httpnContent.GetContent());
  Serial.println("handle root request ok");
}

//处理配置
void handleConfig()
{
  const String wifiName = httpServer.arg("wifiName");
  Serial.println(wifiName);
}

///注册新的wifi信息
void registNewWifiInfomation()
{

  //开启热点模式
  startAp();
  //启动httpserver服务
  httpServer.begin();
  httpServer.on("/", handleRoot);
  httpServer.on("/config", HTTP_POST, handleConfig);
}

//连接wifi信息
bool connectWifi(const WifiInfo &wifiInfo)
{
  return true;
}

void setup()
{

  Serial.begin(9600);

  registNewWifiInfomation();

  return;

  bool wifiExist = configManager.Exist();
  //如果wifi不存在，表示需要用户注册新的wifi信息
  if (wifiExist == false)
  {
    registNewWifiInfomation();
  }
  else
  {
    //获得wifi信息并尝试连接
    const WifiInfo wifiInfo = configManager.GetWifiInformation();
    bool connectResult = connectWifi(wifiInfo);
    if (connectResult)
    {
      //连接成功
    }
    else
    {
      //如果连接不上去。。todo 后面应该怎么办?
    }
  }
}

void loop()
{

  httpServer.handleClient();

  // put your main code here, to run repeatedly:
}