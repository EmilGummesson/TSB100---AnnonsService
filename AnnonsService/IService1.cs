using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AnnonsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        List<Annonser> HamtaAllaAnnonser();

        [OperationContract]
        string SkapaAnnons(Annonser annons);

        [OperationContract]
        string Test();

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        List<Annonser> HamtaSaljAnnonser(int profilID);
        // TODO: Add your service operations here

        [OperationContract]
        List<Annonser> HamtaKopAnnonser(int profilID);

        [OperationContract]
        List<Annonser> HamtaAdminAnnonser();
        
        [OperationContract]
        string UppdateraAnnons(Annonser annons);

        [OperationContract]
        Annonser HamtaAnnons(int annonsID);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
