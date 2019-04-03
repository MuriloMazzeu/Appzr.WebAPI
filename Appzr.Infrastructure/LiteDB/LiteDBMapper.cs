using Appzr.Domain.Entities;
using LiteDB;

namespace Appzr.Infrastructure.LiteDB
{
    public static class LiteDBMapper
    {
        public static void RegisterMaps()
        {
            BsonMapper.Global.RegisterType
            (
                serialize: (entity) => {
                    var doc = new BsonDocument();
                    doc["_id"] = entity.Id;
                    doc["Name"] = entity.Name;
                    doc["Link"] = entity.Link;
                    doc["CreatedAt"] = entity.CreatedAt;

                    return doc;
                },
                deserialize: (bson) =>
                {
                    var doc = bson.AsDocument;

                    var id = doc["_id"].AsGuid;
                    var name = doc["Name"].AsString;
                    var link = doc["Link"].AsString;
                    var date = doc["CreatedAt"].AsDateTime;

                    var entity = new MyAppEntity(id, name, link, date);
                    return entity;
                }
            );
        }
    }
}
