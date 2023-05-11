using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;

namespace TravelAgency.Storage.FileStorage
{
    internal class VoucherFileStorage : IStorage<Voucher>
    {
        private Serializer<Voucher> _serializer;
        private readonly string _file = "../../../Resources/Data/vouchers.csv";

        public VoucherFileStorage()
        {
            _serializer = new Serializer<Voucher>();
        }

        public List<Voucher> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Voucher> vouchers)
        {
            _serializer.ToCSV(_file, vouchers);
        }
    }
}
