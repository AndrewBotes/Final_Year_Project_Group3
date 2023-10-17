using CarrierPidgeon.Types;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarrierPidgeon.Models
{
    public class Drone : IDrone
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public ObjectId userId {get; set;}
        public string status { get; set; }
        public ObjectId routeId {get; set;}

        public Drone(){

        }

        public Drone(ObjectId droneId, ObjectId userId, string status, ObjectId routeId){
            this._id = droneId;
            this.userId = userId;
            this.status = status;
            this.routeId = routeId;
        }
    }
}