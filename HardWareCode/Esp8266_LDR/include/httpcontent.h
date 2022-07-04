#include <ESP8266WebServer.h>
#include "configmanager.h"
#include <ESP8266WiFi.h>

class HttpContent
{
private:
    // http服务
    ESP8266WebServer httpServer;
    ConfigManager configManager;
    /*
进入配置页面
*/
    const char *getContent();
    /*
    配置成功页面
    */
    const char *configOkContent();

    /*
    配置错误页面
    */
    const char *configErrorContent();

    // 验证表单结果并保存
    bool validateFormAndSave();

public:
    HttpContent();
    HttpContent(ConfigManager &configManager);
    ~HttpContent();

    void SetConfig(ConfigManager &configManager);

    /*
    开启http服务监听
    */
    void HttpServerHandleClient();
};
