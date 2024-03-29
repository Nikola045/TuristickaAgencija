﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.RepositoryInterfaces
{
    public interface IStorage<T>
    {
        public List<T> Load();
        public void Save(List<T> list);
    }
}
