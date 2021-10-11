using System.Collections.Generic;
using Parky.Models.Repository.IRepository;

namespace Parky.Models.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        public bool CreateNationalPark(NationalPark park)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteNationalPark(int id)
        {
            throw new System.NotImplementedException();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            throw new System.NotImplementedException();
        }

        public bool NationalParkExists(string parkName)
        {
            throw new System.NotImplementedException();
        }

        public bool NationalParkExists(int parkId)
        {
            throw new System.NotImplementedException();
        }

        public bool Save()
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateNationalPark(NationalPark park)
        {
            throw new System.NotImplementedException();
        }
    }
}