namespace FantasyRoomDisplayDevice.Services
{
    public interface IDeviceCodeBuilder
    {
        /// <summary>
        /// 通过设备获得唯一编号
        /// </summary>
        /// <returns></returns>
        string CreateGuidFromDevice();
    }
}