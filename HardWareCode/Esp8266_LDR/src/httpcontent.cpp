#include "httpcontent.h"

HttpContent::HttpContent()
{
    this->httpServer.on("/", [this]
                        { this->httpServer.send(200, "text/html", this->GetContent()); });
}
HttpContent::~HttpContent()
{
}
const char *HttpContent::GetContent()
{
    const char *info = "<!DOCTYPE html>\n<html lang=\"en\">\n\n<head>\n    <meta charset=\"UTF-8\">\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n    <title>Fantasy Home</title>\n</head>\n\n<body>\n    <div style=\"text-align:center;\">\n        <div>\n            <h1>fantasy home 设备配置</h1>\n        </div>\n        <div>\n            <form action=\"/config\" method=\"post\">\n                Wi-FI名称:<br>\n                <input type=\"text\" name=\"wifiName\" value=\"\">\n                <br>\n                Wi-Fi密码:<br>\n                <input type=\"text\" name=\"wifiPwd\" value=\"\">\n                <br><br>\n                服务器地址:<br>\n                <input type=\"text\" name=\"serviceAddress\" value=\"\">\n                <br><br>\n                设备命名:<br>\n                <input type=\"text\" name=\"deviceName\" value=\"\">\n                <br><br>\n                <input type=\"submit\" value=\"Submit\">\n            </form>\n\n        </div>\n    </div>\n\n</body>\n\n</html>";
    return info;
}

const char *HttpContent::ConfigOkContent()
{
    const char *info = "";
    return info;
}

const char *HttpContent::ConfigErrorContent()
{
    const char *info = "";
    return info;
}

bool HttpContent::ValidateFormAndSave(ESP8266WebServer &server, ConfigManager &configManager)
{
    // todo 验证每个字段

    // 验证结束

    ConfigInfo info{};
    bool saveRes = configManager.SaveConfig(info);
    return saveRes;
}