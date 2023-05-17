using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;
using TravelAgency.View.Guest2;

namespace TravelAgency.Repository
{
    public class VoucherRepository
    {
        private IStorage<Voucher> _storage;
        private List<Voucher> _vouchers;

        public VoucherRepository(IStorage<Voucher> storage)
        {
            _storage = storage;
            _vouchers = _storage.Load();
        }

        public Voucher Save(Voucher entity)
        {
            _vouchers.Add(entity);
            _storage.Save(_vouchers);
            return entity;
        }

        public int NextId()
        {
            if (_vouchers.Count < 1)
            {
                return 1;
            }
            return _vouchers.Max(h => h.Id) + 1;
        }

        public List<Voucher> GetAll()
        {
            return _vouchers;
        }


    }
}
