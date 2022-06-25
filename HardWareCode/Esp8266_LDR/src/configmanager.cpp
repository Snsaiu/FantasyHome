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



const char *ConfigManager::GetGuid() const
{
    return this->guid;
}

bool ConfigManager::SaveConfig(const ConfigInfo &config)
{
    return false;
}
