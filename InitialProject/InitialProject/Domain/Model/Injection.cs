using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;

namespace TravelAgency.Domain.Model
{
    class Injection<T> where T : IStorage<T>, ISerializable, new()
    {
        private readonly Serializer<T> _serializer;
        private readonly string _filePath;

        public Injection(Serializer<T> serializer, string filePath)
        {
            _serializer = serializer;
            _filePath = filePath;
        }

        public T Save(T entity)
        {
            var entities = _serializer.FromCSV(_filePath);
            entities.Add(entity);
            _serializer.ToCSV(_filePath, entities);
            return entity;
        }

        public List<T> GetAll()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void UpdateAll(List<T> entities)
        {
            _serializer.ToCSV(_filePath, entities);
        }

        public T Update(T entity, Func<T, bool> predicate)
        {
            var entities = _serializer.FromCSV(_filePath);
            var currentEntity = entities.FirstOrDefault(predicate);
            if (currentEntity != null)
            {
                var index = entities.IndexOf(currentEntity);
                entities.RemoveAt(index);
                entities.Insert(index, entity);
                _serializer.ToCSV(_filePath, entities);
            }
            return entity;
        }

        public void Delete(T entity, Func<T, bool> predicate)
        {
            var entities = _serializer.FromCSV(_filePath);
            var foundEntity = entities.FirstOrDefault(predicate);
            if (foundEntity != null)
            {
                entities.Remove(foundEntity);
                _serializer.ToCSV(_filePath, entities);
            }
        }

        public int NextId(Func<T, int> idSelector)
        {
            var entities = _serializer.FromCSV(_filePath);
            if (entities.Count() < 1)
            {
                return 1;
            }
            return entities.Max(idSelector) + 1;
        }


    }

}
