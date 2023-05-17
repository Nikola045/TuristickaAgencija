using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;

namespace TravelAgency.Services
{

    public class CheckPointService
    {
        private CheckPointRepository checkPointRepository { get; }
        public CheckPointService() 
        {
            checkPointRepository = new(InjectorService.CreateInstance<IStorage<CheckPoint>>());
        }

        public CheckPoint GetByName(string name)
        {
            List<CheckPoint> checkPoints = checkPointRepository.GetAll();
            foreach(CheckPoint point in checkPoints)
            {
                if(point.Name == name)
                    return point;
            }
            return null;
        }

    }
}
