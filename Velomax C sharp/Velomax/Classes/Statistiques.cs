using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections;
using MathNet.Numerics.Statistics;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Serialization;


namespace Velomax
{
    internal class Stat
    {
        public Stat()
        {
        }
        public static Tuple<List<string>, List<int>, List<string>> FaiblestockFournisseur()
        {
            List<string> id = new List<string>();
            List<int> quantite = new List<int>();
            List<string> siret = new List<string>();
            ArrayList arr = Program.Requete(string.Format("select idPiece,quantiteStock,siret from Piece natural join fournisseur  where quantiteStock < 3;"));
            string[] temp = new string[2];
            for (int i = 0; i < arr.Count; i++)
            {
                temp = arr[i].ToString().Split(',');
                id.Add(temp[0]);
                quantite.Add(Convert.ToInt16(temp[1]));
                siret.Add(temp[2]);
            }
            return Tuple.Create(id, quantite, siret);

        }
        public static void faiblestockXML()
        {
            MyTuple obj = new MyTuple();
            Tuple<List<string>, List<int>, List<string>> a = FaiblestockFournisseur();
            obj.id = a.Item1;
            obj.quantite = a.Item2;
            obj.siret = a.Item3;
            string xml = "";
            XmlSerializer serializer = new XmlSerializer(typeof(MyTuple));
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    serializer.Serialize(writer, obj);
                    xml = sww.ToString(); // Your XML
                }
            }
            using (StreamWriter writer = new StreamWriter("xml/faiblestock.xml"))
            {
                writer.WriteLine(xml);

            }
        }
        // Question 1
        public Tuple<List<string>, List<string>, List<int>> quantiteVendu()
        {
            ArrayList arr = Program.Requete(string.Format("Select idvelo, sum(nbItemVelo) from ContenuCommande group by idvelo"));
            List<string> type = new List<string>();
            List<string> id = new List<string>();
            List<int> nb = new List<int>();

            string[] temp = new string[2];
            for (int i = 0; i < arr.Count; i++)
            {
                temp = arr[i].ToString().Split(',');
                type.Add("Velo");
                id.Add(temp[0]);
                nb.Add(Convert.ToInt32(temp[1]));
            }

            arr = Program.Requete(string.Format("Select idPiece, sum(nbItemPiece) from Supplement group by idpiece"));
            for (int i = 0; i < arr.Count; i++)
            {
                temp = arr[i].ToString().Split(',');
                type.Add("Piece");
                id.Add(temp[0]);
                nb.Add(Convert.ToInt32(temp[1]));
            }
            return Tuple.Create(type, id, nb);
        }
        //Question 2,3
        public Tuple<List<string>, List<string>, List<string>, List<DateTime>> membre() // A ameliorer 
        {
            ArrayList arr = Program.Requete(string.Format("Select typeProg, nom, dateAdhesion prenom from ClientP natural join ProgrammeFidelite order by idProg"));
            List<string> type = new List<string>();
            List<string> nom = new List<string>();
            List<string> prenom = new List<string>();
            List<DateTime> Datefin = new List<DateTime>();
            DateTime date;

            string[] temp = new string[4];
            for (int i = 0; i < arr.Count; i++)
            {
                temp = arr[i].ToString().Split(',');
                type.Add(temp[0]);
                nom.Add(temp[1]);
                prenom.Add(temp[2]);
                date = Convert.ToDateTime(temp[3]);
                date.AddYears(date.Year + 1);
                Datefin.Add(date);
            }
            return Tuple.Create(type, nom, prenom, Datefin);
        }
        //Question 4
        public Tuple<List<string>, List<string>, List<double>> MVP()
        {
            List<double> prix = new List<double>();
            List<string> nom = new List<string>();
            List<string> prenom = new List<string>();
            ArrayList arr = Program.Requete(string.Format("select nom, prenom, sum(prixUnitVelo * nbItemVelo + prixUnitPiece * nbItemPiece) from ClientP natural join Commande natural join Piece natural join Velo natural join contenuCommande natural join supplement group by idC order by sum(prixUnitVelo * nbItemVelo + prixUnitPiece * nbItemPiece); "));
            string[] temp = new string[3];
            for (int i = 0; i < arr.Count; i++)
            {
                temp = arr[i].ToString().Split(',');
                nom.Add(temp[0]);
                prenom.Add(temp[1]);
                prix.Add(Convert.ToDouble(temp[2]));
            }

            return Tuple.Create(nom, prenom, prix);
        }
        //question 5 
        public Tuple<double, double, double> Commande()
        {
            double moypiece = 0;
            double moyprix = 0;
            double moyvelo = 0;
            ArrayList arr = Program.Requete(string.Format("select avg(prixUnitVelo * nbItemVelo + prixUnitPiece * nbItemPiece) from Commande natural join Piece natural join Velo natural join contenuCommande natural join supplement; "));
            moyprix = Convert.ToDouble(arr[0].ToString().Split(',')[0]);
            arr = Program.Requete(string.Format("select avg(nbItemPiece) from Commande natural join Piece natural join supplement;"));
            moypiece = Convert.ToDouble(arr[0].ToString().Split(',')[0]);
            arr = Program.Requete(string.Format(" select avg(nbItemVelo) from Commande natural join Velo natural join contenuCommande;"));
            moyvelo = Convert.ToDouble(arr[0].ToString().Split(',')[0]);
            return Tuple.Create(moyprix, moypiece, moyvelo);
        }
        public int NombredeClient()
        {
            int nombreP = 0;
            int nombreB = 0;
            int sum = 0;
            ArrayList arr = Program.Requete(string.Format("select count(distinct idC) from clientP  where idC != '0';"));
            nombreP = Convert.ToInt32(arr[0].ToString().Split(',')[0]);
            arr = Program.Requete(string.Format("select count(distinct idB) from clientB  where idB != '0';"));
            nombreB = Convert.ToInt32(arr[0].ToString().Split(',')[0]);
            sum = nombreP + nombreB;
            return sum;
        }

        public Tuple<List<string>, List<int>> Faiblestock()
        {
            List<string> id = new List<string>();
            List<int> quantite = new List<int>();
            ArrayList arr = Program.Requete(string.Format("select idPiece,quantiteStock from Piece  where quantiteStock < 3;"));
            string[] temp = new string[2];
            for (int i = 0; i < arr.Count; i++)
            {
                temp = arr[i].ToString().Split(',');
                id.Add(temp[0]);
                quantite.Add(Convert.ToInt16(temp[1]));
            }
            return Tuple.Create(id, quantite);

        }

        public Tuple<List<string>, List<string>, List<int>> Fournisseur()
        {
            List<string> siret = new List<string>();
            List<string> id = new List<string>();
            List<int> quantite = new List<int>();

            ArrayList arr = Program.Requete(string.Format("select siret,idpiece,nbPieceLivraison from Approvisionnement order by siret;"));
            string[] temp = new string[3];
            for (int i = 0; i < arr.Count; i++)
            {
                temp = arr[i].ToString().Split(',');
                siret.Add(temp[0]);
                id.Add(temp[1]);
                quantite.Add(Convert.ToInt16(temp[2]));
            }
            return Tuple.Create(siret, id, quantite);

        }
    }

}