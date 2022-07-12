using System.Collections.Generic;
using FantasyHomeCenter.Application.FamilyCenter.Dto;

namespace FantasyHomeCenter.Application.FamilyCenter;

public interface IFamilyService
{
    /// <summary>
    /// 创建新家庭成员
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    RESTfulResult<CreateFamilyOutput> CreateNewFamily(CreateFamilyInput input);

    /// <summary>
    /// 获得分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    RESTfulResult<PagedList<CreateFamilyOutput>> GetFamilys(FamilyPageInput input);

    RESTfulResult<PagedList<FamilyWithDeivesOuput>> GetFamilysWithDevices(FamilyPageInput input);
}