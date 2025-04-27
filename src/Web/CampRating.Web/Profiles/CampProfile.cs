using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using CampRating.Data.Models;
using CampRating.Web.ViewModels.Camp;

namespace CampRating.Web.Profiles;

public class CampProfile : Profile
{
    public CampProfile()
    {
        this.CreateMap<CampFormModel, Camp>();
        this.CreateMap<Camp, CampFormModel>();
        
        this.CreateMap<Camp, CampInListViewModel>();
        this.CreateMap<CampInListViewModel, Camp>();
        
        this.CreateMap<IEnumerable<Camp>, AllCampsViewModel>();
        this.CreateMap< AllCampsViewModel,IEnumerable<Camp>>();
        
        this.CreateMap<Camp, CampDetailsViewModel>();
        this.CreateMap<CampDetailsViewModel, Camp>();

    }
}