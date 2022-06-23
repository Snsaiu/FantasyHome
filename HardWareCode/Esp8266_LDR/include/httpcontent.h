#include <ESP8266WebServer.h>
class HttpContent
{
public:
    HttpContent();
    ~HttpContent();
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


    bool ValidateForm(ESP8266WebServer& server);
};
