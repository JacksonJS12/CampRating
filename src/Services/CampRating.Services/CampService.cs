using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CampRating.Data.Common.Repositories;
using CampRating.Data.Models;
using CampRating.Services.Interfaces;
using CampRating.Web.ViewModels.Camp;

namespace CampRating.Services;

public class CampService : ICampService
{
    private readonly IDeletableEntityRepository<Camp> _campRepository;
    private readonly IMapper _mapper;

    public CampService(IDeletableEntityRepository<Camp> campRepository, IMapper mapper)
    {
        this._campRepository = campRepository;
        this._mapper = mapper;
    }

    public T GetById<T>(string propertyId)
    {
        var camp = this._campRepository
            .AllAsNoTracking()
            .FirstOrDefault(x => x.Id == propertyId && x.IsDeleted == false);

        return this._mapper.Map<T>(camp);
    }

    public async Task CreateAsync(CampFormModel model)
    {
        var camp = this._mapper.Map<Camp>(model);
        camp.Id = Guid.NewGuid().ToString();

        await this._campRepository.AddAsync(camp);
        await this._campRepository.SaveChangesAsync();
    }

    public IEnumerable<T> GetAll<T>()
    {
        var query = this._campRepository
            .All()
            .Where(x => x.IsDeleted == false)
            .OrderBy(x => x.Name);

        return query.ProjectTo<T>(this._mapper.ConfigurationProvider).ToList();
    }

    public async Task DeleteAsync(string campId)
    {
        var camp = this._campRepository.All()
            .FirstOrDefault(c => c.Id == campId && c.IsDeleted == false);
        
        if (camp == null)
        {
            throw new InvalidOperationException($"Camp with ID {campId} not found.");
        }

        this._campRepository.Delete(camp);
        await this._campRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(CampFormModel model, string campId)
    {
        var camp = this._campRepository.All()
            .FirstOrDefault(c => c.Id == campId && c.IsDeleted == false);

        if (camp == null)
        {
            throw new InvalidOperationException($"Camp with ID {campId} not found.");
        }

        camp.Name = model.Name;
        camp.Description = model.Description;
        camp.Latitude = model.Latitude;
        camp.Longitude = model.Longitude;
        camp.ImgUrl = model.ImgUrl;

        this._campRepository.Update(camp);
        await this._campRepository.SaveChangesAsync();
    }
}