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
                List<Annonser> returAnnonser = new List<Annonser>();
                foreach (Annonser annons in db.Annonser)
                {
                    if (annons.status != "Arkiverad")
                    {
                        returAnnonser.Add(annons);
                    }
                }
                return returAnnonser;
            }
        }

        public string SkapaAnnons(Annonser annons)
        {
            using (AnnonsModel db = new AnnonsModel())
            {
                try
                {
                    System.Diagnostics.Trace.Write("Skapar annons: " + annons.annonsNamn);
                    // You must close or flush the trace to empty the output buffer. 
                    System.Diagnostics.Trace.Flush();

                    db.Annonser.Add(annons);
                    db.SaveChanges();
                    return ("Allt är bra! Tack!");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.Write(annons.annonsNamn + " kunde inte skapas. Fel: " + e);
                    // You must close or flush the trace to empty the output buffer. 
                    System.Diagnostics.Trace.Flush();
                    return ("Allt är inte bra! Adjö!");
                }
            }           
        }

        public List<Annonser> HamtaSaljAnnonser(int profilID)
        {
            using (AnnonsModel db = new AnnonsModel())
            {
                List<Annonser> returAnnonser = new List<Annonser>();
                foreach (Annonser annons in db.Annonser)
                {
                    if (annons.saljarID == profilID && annons.status != "Arkiverad")
                    {
                        returAnnonser.Add(annons);
                    }
                }
                return returAnnonser;
            }
        }

        public List<Annonser> HamtaKopAnnonser(int profilID)
        {
            using (AnnonsModel db = new AnnonsModel())
            {
                List<Annonser> returAnnonser = new List<Annonser>();
                foreach (Annonser annons in db.Annonser)
                {
                    if (annons.koparID == profilID && annons.status != "Arkiverad")
                    {
                        returAnnonser.Add(annons);
                    }
                }
                return returAnnonser;
            }
        }

        public List<Annonser> HamtaAdminAnnonser()
        {
            using (AnnonsModel db = new AnnonsModel())
            { 
                return db.Annonser.ToList();
            }
        }

        public Annonser HamtaAnnons(int annonsID)
        {
            using (AnnonsModel db = new AnnonsModel())
            {
                return db.Annonser.Find(annonsID);
            }
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
                try
                {
                    if (result != null)
                    {
                        System.Diagnostics.Trace.Write("Updaterar annons: " + annons.annonsNamn);
                        System.Diagnostics.Trace.Flush();

                        db.Entry(result).CurrentValues.SetValues(annons);
                        db.SaveChanges();
                    }
                    else
                    {
                    return ("Uppdatering misslyckades");
                    }
                    
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.Write(annons.annonsNamn + " kunde inte uppdateras. Fel: " + e);
                    System.Diagnostics.Trace.Flush();
                    //return "ASP-FISK";
                }
            }                               
        }            
    }
}

