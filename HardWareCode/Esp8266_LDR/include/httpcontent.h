#include <ESP8266WebServer.h>
#include "configmanager.h"
#include <ESP8266WiFi.h>

class HttpContent
{
private:
    // http服务
    ESP8266WebServer httpServer;
    /*
进入配置页面
*/
    const char *GetContent();
    /*
    配置成功页面
    */
    const char *ConfigOkContent();

    /*
    配置错误页面
    */
    const char *ConfigErrorContent();

public:
    HttpContent();
    ~HttpContent();

    // 验证表单结果并保存
    bool ValidateFormAndSave(ESP8266WebServer &server, ConfigManager &configManager);
};
