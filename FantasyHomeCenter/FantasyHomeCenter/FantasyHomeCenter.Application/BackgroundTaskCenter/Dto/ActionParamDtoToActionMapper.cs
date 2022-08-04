using System.Collections.Generic;
using FantasyHomeCenter.Core.Entities;
using Mapster;

namespace FantasyHomeCenter.Application.BackgroundTaskCenter.Dto;

public class ActionParamDtoToActionMapper:IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<ActionParams, AutomationActionInputParam>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Value, src => src.Value)
            .Map(dest => dest.Type,src=> ActionValueType.Set);

      
    }
}