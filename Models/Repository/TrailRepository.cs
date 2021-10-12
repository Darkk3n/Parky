using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Parky.Data;
using Parky.Models.Repository.IRepository;

namespace Parky.Models.Repository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly DataContext context;

        public TrailRepository(DataContext context)
        {
            this.context = context;
        }

        public bool CreateTrail(Trail trail)
        {
            context.Trails.Add(trail);
            return Save();
        }

        public bool DeleteTrail(int id)
        {
            context.Remove(context.Trails.FirstOrDefault(r => r.Id == id));
            return Save();
        }

        public Trail GetTrail(int trailId) => context.Trails.Include(r => r.NationalPark).FirstOrDefault(r => r.Id == trailId);

        public ICollection<Trail> GetTrails() => context.Trails.Include(r => r.NationalPark).OrderBy(r => r.Name).ToList();

        public ICollection<Trail> GetTrailsInNationalPark(int parkId)
        {
            return context
            .Trails
            .Include(r => r.NationalPark)
            .Where(r => r.NationalParkId == parkId)
            .OrderBy(r => r.Name)
            .ToList();
        }

        public bool Save() => context.SaveChanges() > 0;

        public bool TrailExists(string trailName) => context.Trails.Any(r => r.Name == trailName);

        public bool TrailExists(int trailId) => context.Trails.Any(r => r.Id == trailId);

        public bool UpdateTrail(Trail trail)
        {
            context.Trails.Update(trail);
            return Save();
        }
    }
}