using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections;
using MathNet.Numerics.Statistics;
using System.Web.Script.Serialization;

namespace Velomax
{
    public class Velo
    {
        public string idVelo;
        public string nomV;
        public string grandeur;
        public float prixUnitVelo;
        public string ligneProduit;
        public int stockVelo;
        public DateTime dateImVelo;
        public DateTime dateDpVelo;
        public List<Piece> compo;
        public List<int> nbPieces;

        public Velo(string idVelo, string nomV = null, string grandeur = null, float prixUnitVelo = 1, string ligneProduit = null, int stockVelo = 1, DateTime dateImVelo = new DateTime(), DateTime dateDpVelo = new DateTime())
        {
            this.idVelo = idVelo;
            this.nomV = nomV;  
            this.grandeur = grandeur;
            this.prixUnitVelo = prixUnitVelo;
            this.ligneProduit = ligneProduit;
            this.dateImVelo = dateImVelo;
            this.dateDpVelo = dateDpVelo;
            this.stockVelo = stockVelo;
            this.compo = new List<Piece>();
            this.nbPieces = new List<int>();
        }
        public static List<Velo> LoadAllVelos()
        {
            string scriptVelo = "SELECT idVelo, nomV, grandeur, prixUnitVelo, ligneProduit, stockVelo, dateImVelo, dateDpVelo FROM Velo";

            ArrayList preVelo;
            preVelo = Program.Requete(scriptVelo);
            List<string> idVelo = new List<string>();
            List<string> nomVelo = new List<string>();
            List<string> grandeur = new List<string>();
            List<float> prixUnitVelo = new List<float>();
            List<string> ligneProduit = new List<string>();
            List<int> stockVelo = new List<int>();
            List<DateTime> dateImVelo = new List<DateTime>();
            List<DateTime> dateDpVelo = new List<DateTime>();

            string[] temp = new string[8];
            string conv;

            for (int i = 0; i < preVelo.Count; i++)
            {
                conv = preVelo[i].ToString().Trim();
                temp = conv.Split(',');
                for (int m = 0; m < temp.Length; m++)
                {
                    temp[m] = temp[m].Trim();
                }
                if (String.IsNullOrWhiteSpace(temp[1]))
                {
                    temp[1] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[2]))
                {
                    temp[2] = "null";
                }
                if (float.TryParse(temp[3], out var n1) == false)
                {
                    temp[3] = "0";
                }
                if (String.IsNullOrWhiteSpace(temp[4]))
                {
                    temp[4] = "null";
                }
                if (int.TryParse(temp[5], out var n2) == false)
                {
                    temp[5] = "0";
                }
                if (DateTime.TryParse(temp[6], out var n3) == false)
                {
                    temp[6] = "0000-00-00";
                }
                if (DateTime.TryParse(temp[7], out var n4) == false)
                {
                    temp[7] = "0000-00-00";
                }
                idVelo.Add(temp[0]);
                nomVelo.Add(temp[1]);
                grandeur.Add(temp[2]);
                prixUnitVelo.Add(float.Parse(temp[3])); 
                ligneProduit.Add(temp[4]);
                stockVelo.Add(Convert.ToInt32(temp[5]));
                DateTime tempDateDp;
                DateTime.TryParse(temp[7], out tempDateDp);
                dateDpVelo.Add(tempDateDp);
                DateTime tempDateIm;
                DateTime.TryParse(temp[6], out tempDateIm);
                dateImVelo.Add(tempDateIm);
            }

            List<Velo> veloList = new List<Velo>();

            for (int j = 0; j < idVelo.Count; j++)
            {
                veloList.Add(new Velo(idVelo[j], nomVelo[j], grandeur[j], prixUnitVelo[j], ligneProduit[j], stockVelo[j], dateImVelo[j], dateDpVelo[j]));
            }

            /*---------------------------------*/
            string scriptCompo;
            ArrayList curCompo;

            string[] tempCompo = new string[4];
            string convCompo;
            for (int k = 0; k < veloList.Count; k++)
            {
                scriptCompo = String.Format("SELECT * FROM CompositionVelo WHERE idVelo = '{0}'", veloList[k].idVelo);
                curCompo = Program.Requete(scriptCompo);
                for (int l = 0; l < curCompo.Count; l++)
                {
                    convCompo = curCompo[l].ToString().Trim();
                    tempCompo = convCompo.Split(',');
                    for (int m = 0; m < tempCompo.Length; m++)
                    {
                        tempCompo[m] = tempCompo[m].Trim();
                    }
                    if (int.TryParse(tempCompo[2], out var n2) == false)
                    {
                        tempCompo[2] = "0";
                    }
                    Piece tempP = new Piece(tempCompo[0]);
                    Piece curP = tempP.loadPiece();
                    veloList[k].compo.Add(curP);
                    veloList[k].nbPieces.Add(Convert.ToInt32(tempCompo[2]));
                }
            }

            return veloList;
        }
        public Velo LoadVelo()
        {
            string scriptVelo = String.Format("SELECT idVelo, nomV, grandeur, prixUnitVelo, ligneProduit, stockVelo, dateImVelo, dateDpVelo FROM Velo WHERE idVelo = '{0}'", this.idVelo);

            ArrayList preVelo;
            preVelo = Program.Requete(scriptVelo);
            List<string> idVelo = new List<string>();
            List<string> nomVelo = new List<string>();
            List<string> grandeur = new List<string>();
            List<float> prixUnitVelo = new List<float>();
            List<string> ligneProduit = new List<string>();
            List<int> stockVelo = new List<int>();
            List<DateTime> dateImVelo = new List<DateTime>();
            List<DateTime> dateDpVelo = new List<DateTime>();

            string[] temp = new string[8];
            string conv;
            for (int i = 0; i < preVelo.Count; i++)
            {
                conv = preVelo[i].ToString().Trim();
                temp = conv.Split(',');
                for (int m = 0; m < temp.Length; m++)
                {
                    temp[m] = temp[m].Trim();
                }
                if (String.IsNullOrWhiteSpace(temp[1]))
                {
                    temp[1] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[2]))
                {
                    temp[2] = "null";
                }
                if (float.TryParse(temp[3], out var n1) == false)
                {
                    temp[3] = "0";
                }
                if (String.IsNullOrWhiteSpace(temp[4]))
                {
                    temp[4] = "null";
                }
                if (int.TryParse(temp[5], out var n2) == false)
                {
                    temp[5] = "0";
                }
                if (DateTime.TryParse(temp[6], out var n3) == false)
                {
                    temp[6] = "0000-00-00";
                }
                if (DateTime.TryParse(temp[7], out var n4) == false)
                {
                    temp[7] = "0000-00-00";
                }
                idVelo.Add(temp[0]);
                nomVelo.Add(temp[1]);
                grandeur.Add(temp[2]);
                prixUnitVelo.Add(float.Parse(temp[3]));
                ligneProduit.Add(temp[4]);
                stockVelo.Add(Convert.ToInt32(temp[5]));
                DateTime tempDateDp;
                DateTime.TryParse(temp[7], out tempDateDp);
                dateDpVelo.Add(tempDateDp);
                DateTime tempDateIm;
                DateTime.TryParse(temp[6], out tempDateIm);
                dateImVelo.Add(tempDateIm);
            }

            List<Velo> veloList = new List<Velo>();

            for (int j = 0; j < idVelo.Count; j++)
            {
                veloList.Add(new Velo(idVelo[j], nomVelo[j], grandeur[j], prixUnitVelo[j], ligneProduit[j], stockVelo[j], dateImVelo[j], dateDpVelo[j]));
            }

            /*---------------------------------*/
            string scriptCompo;
            ArrayList curCompo;

            string[] tempCompo;
            string convCompo;
            for (int k = 0; k < veloList.Count; k++)
            {
                scriptCompo = String.Format("SELECT * FROM CompositionVelo WHERE idVelo = '{0}'", veloList[k].idVelo);
                curCompo = Program.Requete(scriptCompo);
                for (int l = 0; l < curCompo.Count; l++)
                {
                    convCompo = curCompo[l].ToString().Trim();
                    tempCompo = convCompo.Split(',');
                    for (int m = 0; m < tempCompo.Length; m++)
                    {
                        tempCompo[m] = tempCompo[m].Trim();
                    }
                    if (int.TryParse(tempCompo[2], out var n2) == false)
                    {
                        tempCompo[2] = "0";
                    }
                    Piece tempP = new Piece(tempCompo[0]);
                    Piece curP = tempP.loadPiece();
                    veloList[k].compo.Add(curP);
                    veloList[k].nbPieces.Add(Convert.ToInt32(tempCompo[2]));
                }
            }
            return veloList[0];
        }
        public string Json()
        {
            var jsonString = new JavaScriptSerializer();
            //Use of Serialize() method
            var jsonStringResult = jsonString.Serialize(this);
            string res = jsonStringResult;
            return res;

        }
        public static void ToJson()
        {
            string json = "";
            List<Velo> pieceList = Velo.LoadAllVelos();
            for (int i = 0; i < pieceList.Count; i++)
            {
                json = json + pieceList[i].Json() + ",";
            }
            json = "[" + json + "]";
            using (StreamWriter writer = new StreamWriter("json/velo.json"))
            {
                writer.WriteLine(json);
            }
        }
        public void addVelo()
        {
            if (this != null)
            {
                string formatDate = "yyyy-MM-dd";
                string script = string.Format("INSERT INTO `Velomax`.`Velo` (`idVelo`,`nomV`,`grandeur`,`PrixUnitVelo`,`ligneProduit`,`dateImvelo`,`dateDpvelo`,`stockVelo`) " +
                                "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', DATE('{5}'), DATE('{6}'), '{7}')", this.idVelo, this.nomV, this.grandeur, this.prixUnitVelo, this.ligneProduit, this.dateImVelo.ToString(formatDate), this.dateDpVelo.ToString(formatDate), this.stockVelo);
                Program.Execute(script);
            }
            if (this != null && this.compo != null)
            {
                string scriptComp = string.Empty;
                for (int i = 0; i < this.compo.Count; i++)
                {
                    scriptComp = string.Format("INSERT INTO `Velomax`.`CompositionVelo` (`idPiece`, `idVelo`, `nbPieceVelo`) " +
                                           "VALUES('{0}', '{1}', '{2}')", this.compo[i].idPiece, this.idVelo, this.nbPieces[i]);
                    Program.Execute(scriptComp);
                }
            }
        }
        public void delVelo()
        {
            if (this != null && this.compo != null)
            {
                string scriptCompo = string.Empty;
                for (int i = 0; i < this.compo.Count; i++)
                {
                    scriptCompo = string.Format("DELETE FROM CompositionVelo WHERE idPiece = '{0}' and idVelo = '{1}'", this.compo[i].idPiece, this.idVelo);
                    Program.Execute(scriptCompo);
                }
            }
            if (this != null)
            {
                string script = string.Format("DELETE FROM Velo WHERE idVelo = '{0}'", this.idVelo);
                Program.Execute(script);   
            }
        }
        public void editVelo()
        {
            if (this != null)
            {
                string formatDate = "yyyy-MM-dd";
                string script = string.Format("UPDATE Velo " +
                                              "SET nomV = '{0}', grandeur = '{1}', prixUnitVelo = '{2}', ligneProduit = '{3}', dateImVelo = DATE('{4}'), dateDpVelo = DATE('{5}'), stockVelo = '{6}'" +
                                              "WHERE idVelo = '{7}'", this.nomV, this.grandeur, this.prixUnitVelo, this.ligneProduit, this.dateImVelo.ToString(formatDate), this.dateDpVelo.ToString(formatDate), this.stockVelo, this.idVelo);
                Program.Execute(script);
            }
            if (this != null && this.compo != null)
            {
                string scriptCompo = string.Empty;
                for (int i = 0; i < this.compo.Count; i++)
                {
                    scriptCompo = string.Format("UPDATE CompositionVelo " +
                                                "SET nbPieceVelo = '{0}' " +
                                                "WHERE idPiece = '{1}' and idVelo = '{2}'", this.nbPieces[i], this.compo[i].idPiece, this.idVelo);
                    Program.Execute(scriptCompo);
                }
            }
        }
        public int stock()
        {
            ArrayList arr = Program.Requete(string.Format("select stockVelo from velo where idVelo = '{0}'", this.idVelo));
            int nb = 0;
            nb = Convert.ToInt32(arr[0].ToString().Split(',')[0]);
            return nb;
        }
        public int stockCategorie() //A verif
        {
            ArrayList arr = Program.Requete(string.Format("select sum(stockVelo) from velo where ligneProduit = {0} ", this.ligneProduit));
            int nb = 0;
            nb = Convert.ToInt32(arr[0].ToString().Split(',')[0]);
            return nb;
        }
        public int stockMarque() //A verif
        {
            ArrayList arr = Program.Requete(string.Format("select sum(stockVelo) from velo where nomV = {0} ", this.nomV));
            int nb = 0;
            nb = Convert.ToInt32(arr[0].ToString().Split(',')[0]);
            return nb;
        }
    }
}

