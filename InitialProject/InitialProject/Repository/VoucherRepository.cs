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

namespace TravelAgency.Repository
{
    public class VoucherRepository
    {

        private const string FilePath = "../../../Resources/Data/vouchers.csv";
        private const string FilePathGuest = "../../../Resources/Data/guestOnTour.csv";


        private readonly Serializer<Voucher> _serializer;

        private readonly TourRepository _tourRepository;    

        private List<Voucher> _vouchers;

        public VoucherRepository()
        {
            _serializer = new Serializer<Voucher>();
            _vouchers = _serializer.FromCSV(FilePath);
        }

        public Voucher Save(Voucher voucher)
        {
            voucher.Id = NextId();
            _vouchers = _serializer.FromCSV(FilePath);
            _vouchers.Add(voucher);
            _serializer.ToCSV(FilePath, _vouchers);
            return voucher;
        }

        public int NextId()
        {
            _vouchers = _serializer.FromCSV(FilePath);
            if (_vouchers.Count < 1)
            {
                return 1;
            }
            return _vouchers.Max(t => t.Id) + 1;
        }

        public void CreateVouchersForCancelling(Tour tour, int guestId)
        {
            List<GuestOnTour> guestOnTours = _tourRepository.ReadFromGuestOnTour(FilePathGuest);
            for(int i = 0; i< guestOnTours.Count; i++)
            {
                if (guestOnTours[i].TourId == tour.Id) 
                { 
                    Voucher voucher = new Voucher();
                    voucher.Id = NextId();
                    voucher.ExpirationDate = DateTime.Now;
                    voucher.Name = "Voucher for canceling";
                    voucher.GuestId = guestId;
                    Save(voucher);
                }
            }
        }

        public List<Voucher> ReadFromVouchersCsv(string FileName)
        {
            List<Voucher> vouchers = new List<Voucher>();

            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    Voucher voucher = new Voucher();

                    voucher.Id = Convert.ToInt32(fields[0]);
                    voucher.Name = fields[1];
                    voucher.ExpirationDate = Convert.ToDateTime(fields[2]);
                    vouchers.Add(voucher);
                }
            }
            return vouchers;
        }


    }
}
