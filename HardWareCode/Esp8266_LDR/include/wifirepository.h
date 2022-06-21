#include <FS.h>
#include "ArduinoJson.hpp"

class WifiRepository
{

private:
    const char *wifiFileName = "/wifi.json";

public:
    WifiRepository();
    ~WifiRepository();
    /*
    判断是否存在wifi信息
    */
    bool Exist();
};