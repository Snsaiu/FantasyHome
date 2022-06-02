namespace FantasyHomeCenter.Application.DeviceCenter.Dto;

public class DeviceInput
{
    public DeviceInput(int pageIndex,int pageSize)
    {
        (PageIndex, PageSize) = (pageIndex, pageSize);
        
    }

    public DeviceInput(int pageIndex, int pageSize, int deviceId)
    {
        (PageIndex, PageSize, deviceType) = (pageIndex, pageSize, deviceId);
        
    }
    public int PageSize { get; private set; }
    public int  PageIndex { get;private set; }
    
    public int? deviceType { get;private set; }
    
}