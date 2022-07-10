#ifndef MQTTSERVER
#define MQTTSERVER

#include <PubSubClient.h>
#include <ESP8266WiFi.h>
class MqttServer
{
private:
    WiFiClient wifiClient;
    PubSubClient mqttClient = PubSubClient(wifiClient);
    void healthCheck(String guid);
    String guid;
    /* data */
public:
    MqttServer(/* args */);
    ~MqttServer();
    ///开启mqtt服务
    void Begin(String server, int port, String guid);

    // 保持心跳
    void LoopConnectCheck();

    //发布内容
    bool Publish(String topic, String message);
};

MqttServer::MqttServer(/* args */)
{
}

MqttServer::~MqttServer()
{
}
bool MqttServer::Publish(String topic, String message)
{
    // 实现ESP8266向主题发布信息
    if (mqttClient.publish(topic.c_str(), message.c_str()))
    {
        Serial.println("Publish Topic:");
        Serial.println(topic);
        Serial.println("Publish message:");
        Serial.println(message);
        return true;
    }
    else
    {
        Serial.println("Message Publish Failed.");
        return false;
    }
}
void MqttServer::LoopConnectCheck()
{
    if (mqttClient.connected())
    {                      // 如果开发板成功连接服务器
        mqttClient.loop(); // 保持客户端心跳
    }
    else
    { // 如果开发板未能成功连接服务器
        this->healthCheck(this->guid);
    }
}

void MqttServer::healthCheck(String guid)
{
    if (mqttClient.connect(guid.c_str()))
    {
        Serial.println("MQTT Server Connected.");
        Serial.println("Server Address: ");
        Serial.println("ClientId:");
        Serial.println(guid);
    }
    else
    {
        Serial.print("MQTT Server Connect Failed. Client State:");
        Serial.println(mqttClient.state());
        delay(3000);
    }
}

void MqttServer::Begin(String server, int port, String guid)
{

    this->guid = guid;
    // 设置MQTT服务器和端口号
    mqttClient.setServer(server.c_str(), port);

    this->healthCheck(guid);
}
#endif // !MQTTSERVER
