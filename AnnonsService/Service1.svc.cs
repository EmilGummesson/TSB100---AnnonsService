using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AnnonsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        public List<Annonser> HamtaAllaAnnonser()
        {
            using (AnnonsModel db = new AnnonsModel())
            {
                return db.Annonser.ToList();
            }
        }

        public string SkapaAnnons(Annonser annons)
        {
            using (AnnonsModel db = new AnnonsModel())
            {
                db.Annonser.Add(annons);
                db.SaveChanges();
                return ("Allt är bra! Tack!");
            }
            
        }

        public List<Annonser> HamtaSaljAnnonser(int profilID)
        {
            using (AnnonsModel db = new AnnonsModel())
            {
                List<Annonser> returAnnonser = new List<Annonser>();
                foreach (Annonser annons in db.Annonser)
                {
                    if (annons.profilID == profilID)
                    {
                        returAnnonser.Add(annons);
                    }
                }
                return returAnnonser;
            }
        }

        public Annonser HamtaAnnons(int annonsID)
        {
            using (AnnonsModel db = new AnnonsModel())
            {
                return db.Annonser.Find(annonsID);
            }
        }



        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public string Test()
        {
            using (AnnonsModel db = new AnnonsModel())
            {
                var dbAnnonsLista = db.Annonser.ToList();
                return (dbAnnonsLista[0].betalningsmetod);
            }
            

            //return "ASP-FISK";
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string UppdateraAnnons(Annonser annons)
        {
            using (AnnonsModel db = new AnnonsModel())
            {
                var result = db.Annonser.Find(annons.annonsID);
                if (result != null)
                {
                    //result = annons;
                    db.Entry(result).CurrentValues.SetValues(annons);
                    db.SaveChanges();
                    return ("Annons uppdaterad!");

                }
                else
                    return ("Uppdatering misslyckades");

            }


            //return "ASP-FISK";
        }
    }
}
