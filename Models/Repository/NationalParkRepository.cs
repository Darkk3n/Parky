using System.Collections.Generic;
using System.Linq;
using Parky.Data;
using Parky.Models.Repository.IRepository;

namespace Parky.Models.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly DataContext context;

        public NationalParkRepository(DataContext context)
        {
            this.context = context;
        }

        public bool CreateNationalPark(NationalPark park)
        {
            context.NationalPaks.Add(park);
            return Save();
        }

        public bool DeleteNationalPark(int id)
        {
            var parkToDelete = context.NationalPaks.FirstOrDefault(r => r.Id == id);
            context.Remove(parkToDelete);
            return Save();
        }

        public bool UpdateNationalPark(NationalPark park)
        {
            context.NationalPaks.Update(park);
            return Save();
        }
        public NationalPark GetNationalPark(int nationalParkId) => context.NationalPaks.FirstOrDefault(r => r.Id == nationalParkId);

        public ICollection<NationalPark> GetNationalParks() => context.NationalPaks.OrderBy(r => r.Name).ToList();

        public bool NationalParkExists(string parkName) => context.NationalPaks.Any(r => r.Name.ToLower().Trim() == parkName.ToLower().Trim());

        public bool NationalParkExists(int parkId) => context.NationalPaks.Any(r => r.Id == parkId);

        public bool Save() => context.SaveChanges() > 0;
    }
}