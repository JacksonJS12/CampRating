using System.Collections.Generic;
using System.Threading.Tasks;
using CampRating.Web.ViewModels.Camp;

namespace CampRating.Services.Interfaces;

public interface ICampService
{
    public T GetById<T>(string campId);
    public Task CreateAsync(CampFormModel model);
    public IEnumerable<T> GetAll<T>();
    public Task DeleteAsync(string campId);
    public Task UpdateAsync(CampFormModel model, string campId);
}