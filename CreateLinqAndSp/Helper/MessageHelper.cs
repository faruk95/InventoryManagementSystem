using CreateLinqAndSp.DTO;
using System.Runtime.Serialization;


namespace PurcheseWork.Helper
{
    public class MessageHelper
    {
        [DataMember]
        public string? Message { get; set; }
        public int statuscode { get; set; }
        public long Key { get; set; }
        public  List<CrateItemList> duplicate { get; set; }
        
    }
}
