using System;
namespace DomainInterface
{
    public interface IInbound
    {
        //string Available_Branch { get; set; }
        //int? Cat_ID { get; set; }
       
        ////Infrastructure.DAL.ObjectChangeTracker ChangeTracker { get; set; }
        //string Description { get; set; }
        //string Details { get; set; }
        //string Download_File { get; set; }
        //DateTime? EndDate { get; set; }
        //string Enrollment_Rate { get; set; }
        //string Estemated_Hours { get; set; }
        //int ID { get; set; }
        //string Name { get; set; }
        void OnDeserializedMethod(System.Runtime.Serialization.StreamingContext context);
        void OnDeserializingMethod(System.Runtime.Serialization.StreamingContext context);
      
        //Infrastructure.DAL.TrackableCollection<Infrastructure.DAL.Track> Tracks { get; set; }
    }
}
