using Appzr.Domain.Contracts;
using Appzr.Domain.Entities;
using LiteDB;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Appzr.Infrastructure.Repositories
{
    public abstract class MainRepository<T> : IRepository<T> where T : EntityBase
    {
        public MainRepository(IConfiguration configuration, string collection)
        {
            Collection = collection;
            ConnectionString = configuration.GetConnectionString("MainDB");
        }

        private string Collection { get; }
        private string ConnectionString { get; }        

        protected LiteDatabase GetDB() =>
            new LiteDatabase(ConnectionString);

        public async Task<T> FindByIdAsync(Guid id)
        {
            using (var db = GetDB())
            {
                return await Task.Run(() =>
                {
                    var entities = db.GetCollection<T>(Collection);
                    return entities.FindById(id);
                });
            }
        }

        public async Task SaveAsync(T entity)
        {
            using (var db = GetDB())
            {
                await Task.Run(() =>
                {
                    var entities = db.GetCollection<T>(Collection);
                    entities.Insert(entity);
                });
            }
        }
    }
}
