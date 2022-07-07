#include <Arduino.h>
#include "registFacade.h"

RegistFacade registFacade;
void setup()
{

  Serial.begin(9600);
  registFacade.Init("B5A97CB8-59C6-2F14-C2B4-C60E49E82E4A", "fantasyhome", "1234567890");
}

void loop()
{
  // 开启http服务监听
  registFacade.HttpServerHandleClient();
  //开始健康检查
  bool checkRes = registFacade.HealthCheck(5000);
  if (checkRes)
  {
    Serial.println("health check ok!!!");
    // todo you logic
  }
  else
  {
    Serial.println("health check error!!!");
    //健康检查失败
  }

  // put your main code here, to run repeatedly:
}