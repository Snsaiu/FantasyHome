using System.Collections.Generic;
using FantasyHomeCenter.Application.RoomCenter.Dto;

namespace FantasyHomeCenter.Application.DeviceCenter.Dto;

public class DeviceTypesAndRoomsOutput
{
    public DeviceTypesAndRoomsOutput()
    {
        this.DeviceTypes = new List<DeviceTypeOutput>();
        this.Rooms = new List<RoomOutput>();
    }
    public List<DeviceTypeOutput> DeviceTypes { get; set; }
    public List<RoomOutput> Rooms { get; set; }
}