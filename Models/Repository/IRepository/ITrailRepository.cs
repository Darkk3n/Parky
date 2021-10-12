using System.Collections.Generic;

namespace Parky.Models.Repository.IRepository
{
    public interface ITrailRepository
    {
        ICollection<Trail> GetTrails();
        ICollection<Trail> GetTrailsInNationalPark(int parkId);
        Trail GetTrail(int TrailId);
        bool TrailExists(string parkName);
        bool TrailExists(int parkId);
        bool CreateTrail(Trail park);
        bool UpdateTrail(Trail park);
        bool DeleteTrail(int id);
        bool Save();
    }
}