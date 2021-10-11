using System.Collections.Generic;

namespace Parky.Models.Repository.IRepository
{
    public interface INationalParkRepository
    {
        ICollection<NationalPark> GetNationalParks();
        NationalPark GetNationalPark(int nationalParkId);
        bool NationalParkExists(string parkName);
        bool NationalParkExists(int parkId);
        bool CreateNationalPark(NationalPark park);
        bool UpdateNationalPark(NationalPark park);
        bool DeleteNationalPark(int id);
        bool Save();
    }
}