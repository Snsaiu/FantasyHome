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
    public int PageSize { get;  set; }
    public int  PageIndex { get; set; }
    
    public int? deviceType { get; set; }
    
}