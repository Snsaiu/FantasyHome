#include "configmanager.h"

ConfigManager::ConfigManager()
{
}
ConfigManager::~ConfigManager()
{
}

const WifiInfo ConfigManager::GetWifiInformation()
{

    WifiInfo info{"", ""};
    return info;
}

bool ConfigManager::SaveWifiInfomation(const WifiInfo info)
{
    return true;
}
