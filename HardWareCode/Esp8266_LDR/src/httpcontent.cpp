#include "httpcontent.h"

HttpContent::HttpContent(ConfigManager &configManager)
{
    this->configManager = configManager;
    this->httpServer.begin(80);
    Serial.println("start http server");

    this->httpServer.on("/", [this]
                        { 
                            Serial.println("request home page start");
                            this->httpServer.send(200, "text/html", this->getContent()); 
                            Serial.println("request home page end;"); });

    this->httpServer.on("/config", HTTP_POST, [this]
                        { 
                            Serial.println("request config page start");
                           bool validateRes= this->validateFormAndSave();
                           if(validateRes)
                           {
                             this->httpServer.send(200,"text/html",this->configOkContent());
                           }else{
                            this->httpServer.send(200,"text/html",this->configErrorContent());
                           }
                           Serial.println("request config page end"); });
}
HttpContent::~HttpContent()
{
}
HttpContent::HttpContent()
{
}
const char *HttpContent::getContent()
{
    if (this->configManager.Exist())
    {
        Config c = this->configManager.GetConfig();
        String info = "<!DOCTYPE html>\n<html lang=\"en\">\n\n<head>\n    <meta charset=\"UTF-8\">\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n    <title>Fantasy Home</title>\n</head>\n\n<body>\n    <div style=\"text-align:center;\">\n        <div>\n            <h1>fantasy home 设备配置</h1>\n        </div>\n        <div>\n            <form action=\"/config\" method=\"post\">\n                Wi-FI名称:<br>\n                <input type=\"text\" name=\"wifiName\" value=\"" + c.wifiName + "\">\n                <br>\n                Wi-Fi密码:<br>\n                <input type=\"text\" name=\"wifiPwd\" value=\"" + c.wifiPwd + "\">\n                <br><br>\n                服务器地址:<br>\n                <input type=\"text\" name=\"serviceAddress\" value=\"" + c.serviceHost + "\">\n   <br><br>\n       服务器端口:<br>\n                <input type=\"text\" name=\"servicePort\" value=\"" + c.servicePort + "\">\n               <br><br>\n                设备命名:<br>\n                <input type=\"text\" name=\"deviceName\" value=\"" + c.myName + "\">\n                <br><br>\n                <input type=\"submit\" value=\"Submit\">\n            </form>\n\n        </div>\n    </div>\n\n</body>\n\n</html>";
        return info.c_str();
    }
    else
    {
        const char *info = "<!DOCTYPE html>\n<html lang=\"en\">\n\n<head>\n    <meta charset=\"UTF-8\">\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n    <title>Fantasy Home</title>\n</head>\n\n<body>\n    <div style=\"text-align:center;\">\n        <div>\n            <h1>fantasy home 设备配置</h1>\n        </div>\n        <div>\n            <form action=\"/config\" method=\"post\">\n                Wi-FI名称:<br>\n                <input type=\"text\" name=\"wifiName\" value=\"\">\n                <br>\n                Wi-Fi密码:<br>\n                <input type=\"text\" name=\"wifiPwd\" value=\"\">\n                <br><br>\n                服务器地址:<br>\n                <input type=\"text\" name=\"serviceAddress\" value=\"\">\n   <br><br>\n       服务器端口:<br>\n                <input type=\"text\" name=\"servicePort\" value=\"\">\n               <br><br>\n                设备命名:<br>\n                <input type=\"text\" name=\"deviceName\" value=\"\">\n                <br><br>\n                <input type=\"submit\" value=\"Submit\">\n            </form>\n\n        </div>\n    </div>\n\n</body>\n\n</html>";
        return info;
    }
}

const char *HttpContent::configOkContent()
{
    const char *info = "ok";
    return info;
}

const char *HttpContent::configErrorContent()
{
    const char *info = "erroe";
    return info;
}

bool HttpContent::validateFormAndSave()
{
    // todo 验证每个字段
    const String wifiName = this->httpServer.arg("wifiName");
    if (wifiName == "")
        return false;

    Serial.println("wifi name:" + wifiName);

    const String wifiPwd = this->httpServer.arg("wifiPwd");
    if (wifiPwd == "")
        return false;

    Serial.println("wifi pwd:" + wifiPwd);

    const String serviceHost = this->httpServer.arg("serviceAddress");
    if (serviceHost == "")
        return false;

    Serial.println("service host:" + serviceHost);
    const String myName = this->httpServer.arg("deviceName");
    if (myName == "")
        return false;
    Serial.println("device name:" + myName);
    const String port = this->httpServer.arg("servicePort");
    if (port == "")
        return false;

    Serial.println("service port" + port);

    // 验证结束

    Config info{wifiName, wifiPwd, serviceHost, port, myName};
    bool saveRes = configManager.SaveConfig(info);
    return saveRes;
}

void HttpContent::HttpServerHandleClient()
{
    this->httpServer.handleClient();
}

void HttpContent::SetConfig(ConfigManager &configManager)
{
    this->configManager = configManager;
}