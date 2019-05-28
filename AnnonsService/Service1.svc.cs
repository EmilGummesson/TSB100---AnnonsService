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
        //Hämtar alla annonser som inte är arkiverade och lägger till i en lista.
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

        //Skapar ny annons
        public string SkapaAnnons(Annonser annons)
        {
            using (AnnonsModel db = new AnnonsModel())
            {
                try
                {
                    //Loggar varje gång en annons skall skapas.
                    System.Diagnostics.Trace.Write("Skapar annons: " + annons.annonsNamn);
                    // You must close or flush the trace to empty the output buffer. 
                    System.Diagnostics.Trace.Flush();

                    db.Annonser.Add(annons);
                    db.SaveChanges();
                    return ("Allt är bra! Tack!");
                }
                catch (Exception e)
                {
                    //Loggar när en annons inte kan skapas.
                    System.Diagnostics.Trace.Write(annons.annonsNamn + " kunde inte skapas. Fel: " + e);
                    // You must close or flush the trace to empty the output buffer. 
                    System.Diagnostics.Trace.Flush();
                    return ("Allt är inte bra! Adjö!");
                }
            }           
        }
        //Hämtar alla annonser av en säljare som inte är arkiverade.
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

        //Hämtar alla annonser av en köpare som inte är arkiverade.
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

        //Hämtar alla annonser oavsett om dessa är arkiverade eller ej. Admin funktionallitet.
        public List<Annonser> HamtaAdminAnnonser()
        {
            using (AnnonsModel db = new AnnonsModel())
            { 
                return db.Annonser.ToList();
            }
        }

        //Hämtar en specifik annons med ID som inparameter.
        public Annonser HamtaAnnons(int annonsID)
        {
            using (AnnonsModel db = new AnnonsModel())
            {
                return db.Annonser.Find(annonsID);
            }
        }

        //Testfunktion
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

        //Uppdaterar tabeller i databasen
        public string UppdateraAnnons(Annonser annons)
        {
            using (AnnonsModel db = new AnnonsModel())
            {
                //Hittar rätt inlägg i databasen
                var result = db.Annonser.Find(annons.annonsID);
                try
                {
                    if (result != null)
                    {
                        //Loggar varje gång en annons skall uppdateras.
                        System.Diagnostics.Trace.Write("Updaterar annons: " + annons.annonsNamn);
                        // You must close or flush the trace to empty the output buffer. 
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
                    //Loggar varje gång en annons inte lyckas med uppdateringen.
                    System.Diagnostics.Trace.Write(annons.annonsNamn + " kunde inte uppdateras. Fel: " + e);
                    // You must close or flush the trace to empty the output buffer. 
                    System.Diagnostics.Trace.Flush();
                    //return "ASP-FISK";
                }
            }                               
        }            
    }
}

