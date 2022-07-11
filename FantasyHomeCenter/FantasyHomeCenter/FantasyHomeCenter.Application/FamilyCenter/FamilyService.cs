using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using FantasyHome.GTool;
using FantasyHomeCenter.Application.FamilyCenter.Dto;
using FantasyHomeCenter.Core.Entities;
using Furion.DatabaseAccessor;
using Mapster;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FantasyHomeCenter.Application.FamilyCenter;

public class FamilyService:IFamilyService,IDynamicApiController,ITransient
{
    private readonly IRepository<Family> familyRepository;

    public FamilyService(IRepository<Family> familyRepository)
    {
        this.familyRepository = familyRepository;
    }
    public RESTfulResult<CreateFamilyOutput> CreateNewFamily(CreateFamilyInput input)
    {
        if (this.familyRepository.Any(x=>x.UserName==input.Name))
        {
            return new RESTfulResult<CreateFamilyOutput>() { Succeeded = false, Errors = $"{input.Name} 家庭成员已经存在" };
        }

        string pwd= Tools.MD5Encrypt32(input.Pwd);

        Family family = new Family();
        family.UserName = input.Name;
        family.Password = pwd;
        family.PhoneNumber = input.Phone;
        EntityEntry<Family> entry = this.familyRepository.InsertNow(family);

       var output=  entry.Entity.Adapt<CreateFamilyOutput>();
       return new RESTfulResult<CreateFamilyOutput>() { Succeeded = true, Data = output };
    }

    public RESTfulResult<PagedList<CreateFamilyOutput>> GeFamilys(FamilyPageInput input)
    {
      var query=  this.familyRepository.AsQueryable();
      var list= query.Select(x => new CreateFamilyOutput()
      {
          UserName = x.UserName,
          PhoneNumber = x.PhoneNumber,
          Id = x.Id
      }).ToPagedList(input.PageIndex, input.PageSize);
      return new RESTfulResult<PagedList<CreateFamilyOutput>>() { Succeeded = true, Data = list };
    }
}