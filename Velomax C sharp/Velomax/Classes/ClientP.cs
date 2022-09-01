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
    
    internal class ClientP
    {
        public string idC;
        public string idProg;
        public string nom;
        public string prenom;
        public string idAdresse;
        public string codePostP;
        public string nomRueP;
        public int numRueP;
        public DateTime dateAdhesion;
        public DateTime dateFinAdhesion;

        public ClientP(string idc,string idProg = "0", string nom = null, string prenom = null, string idAdresse = null, string codePostP = "00000", string nomRueP = null, int numRueP = 0, DateTime dateAdhesion = new DateTime(), DateTime dateFinAdhesion = new DateTime())
        {
            this.idC = idc;
            this.idProg = idProg;
            this.nom = nom;
            this.prenom = prenom;
            this.idAdresse = idAdresse;
            this.codePostP = codePostP;
            this.nomRueP = nomRueP;
            this.numRueP = numRueP;
            this.dateAdhesion = dateAdhesion;
            this.dateFinAdhesion = dateFinAdhesion;
        }
        public static List<ClientP> LoadAllClientsP()
        {
            string scriptClientP = "SELECT * FROM ClientP";

            ArrayList preCp;
            preCp = Program.Requete(scriptClientP);
            List<string> idC = new List<string>();
            List<string> idProg = new List<string>();
            List<string> nom = new List<string>();
            List<string> prenom = new List<string>();
            List<string> idAdresse = new List<string>();
            List<string> codePostP = new List<string>();
            List<string> nomRueP = new List<string>();
            List<int> numRueP = new List<int>();
            List<DateTime> dateAdhesion = new List<DateTime>();
            List<DateTime> dateFinAdhesion = new List<DateTime>();

            string[] temp = new string[11];

            for (int i = 0; i < preCp.Count; i++)
            {
                temp = preCp[i].ToString().Split(',');
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
                if (String.IsNullOrWhiteSpace(temp[3]))
                {
                    temp[3] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[4]))
                {
                    temp[4] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[5]))
                {
                    temp[5] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[6]))
                {
                    temp[6] = "null";
                }
                if (int.TryParse(temp[7], out var n2) == false)
                {
                    temp[7] = "0";
                }
                if (DateTime.TryParse(temp[8], out var n3) == false)
                {
                    temp[8] = "0000-00-00";
                }
                if (DateTime.TryParse(temp[9], out var n4) == false)
                {
                    temp[9] = "0000-00-00";
                }

                idC.Add(temp[0]);
                idProg.Add(temp[1]);
                nom.Add(temp[2]);
                prenom.Add(temp[3]);
                idAdresse.Add(temp[4]);
                codePostP.Add(temp[5]);
                nomRueP.Add(temp[6]);
                numRueP.Add(Convert.ToInt32(temp[7]));
                DateTime tempDateA;
                DateTime.TryParse(temp[8], out tempDateA);
                dateAdhesion.Add(tempDateA);
                DateTime tempDateFA;
                DateTime.TryParse(temp[9], out tempDateFA);
                dateFinAdhesion.Add(tempDateFA);
            }
            List<ClientP> ClientPList = new List<ClientP>();

            for (int j = 0; j < idC.Count; j++)
            {
                ClientPList.Add(new ClientP(idC[j], idProg[j], nom[j], prenom[j], idAdresse[j], codePostP[j], nomRueP[j], numRueP[j], dateAdhesion[j], dateFinAdhesion[j]));
            }
            return ClientPList;
        }
        public ClientP LoadClientP()
        {
            string scriptClientP = String.Format("SELECT * FROM ClientP where idC = '{0}'", this.idC);

            ArrayList preCp;
            preCp = Program.Requete(scriptClientP);
            List<string> idC = new List<string>();
            List<string> idProg = new List<string>();
            List<string> nom = new List<string>();
            List<string> prenom = new List<string>();
            List<string> idAdresse = new List<string>();
            List<string> codePostP = new List<string>();
            List<string> nomRueP = new List<string>();
            List<int> numRueP = new List<int>();
            List<DateTime> dateAdhesion = new List<DateTime>();
            List<DateTime> dateFinAdhesion = new List<DateTime>();

            string[] temp = new string[11];

            for (int i = 0; i < preCp.Count; i++)
            {
                temp = preCp[i].ToString().Split(',');
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
                if (String.IsNullOrWhiteSpace(temp[3]))
                {
                    temp[3] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[4]))
                {
                    temp[4] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[5]))
                {
                    temp[5] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[6]))
                {
                    temp[6] = "null";
                }
                if (int.TryParse(temp[7], out var n2) == false)
                {
                    temp[7] = "0";
                }
                if (DateTime.TryParse(temp[8], out var n3) == false)
                {
                    temp[8] = "0000-00-00";
                }
                if (DateTime.TryParse(temp[9], out var n4) == false)
                {
                    temp[9] = "0000-00-00";
                }

                idC.Add(temp[0]);
                idProg.Add(temp[1]);
                nom.Add(temp[2]);
                prenom.Add(temp[3]);
                idAdresse.Add(temp[4]);
                codePostP.Add(temp[5]);
                nomRueP.Add(temp[6]);
                numRueP.Add(Convert.ToInt32(temp[7]));
                DateTime tempDateA;
                DateTime.TryParse(temp[8], out tempDateA);
                dateAdhesion.Add(tempDateA);
                DateTime tempDateFA;
                DateTime.TryParse(temp[9], out tempDateFA);
                dateFinAdhesion.Add(tempDateFA);

            }
            ClientP client = new ClientP(idC[0], idProg[0], nom[0], prenom[0], idAdresse[0], codePostP[0], nomRueP[0], numRueP[0], dateAdhesion[0], dateFinAdhesion[0]);
            return client;
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
            List<Piece> pieceList = Piece.LoadAllPieces();
            for (int i = 0; i < pieceList.Count; i++)
            {
                json = json + pieceList[i].Json() + ",";
            }
            json = "[" + json + "]";
            using (StreamWriter writer = new StreamWriter("json/ClienP.json"))
            {
                writer.WriteLine(json);

            }

        }
        public void addClientP()
        {
            string formatDate = "yyyy-MM-dd";
            if (this != null)
            {
                string script = string.Format("INSERT INTO `Velomax`.`ClientP` (`idC`,`idProg`,`nom`,`prenom`,`idAdresse`,`codePostP`,`nomRueP`,`numRueP`,`dateAdhesion`,`dateFinAdhesion`) " +
                                        "VALUES('{0}','{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', DATE('{8}'), DATE('{9}'))", this.idC, this.idProg, this.nom, this.prenom, this.idAdresse, this.codePostP, this.nomRueP, this.numRueP, this.dateAdhesion.ToString(formatDate), this.dateFinAdhesion.ToString(formatDate));
                Program.Execute(script);
            }
        }
        public void delClientP()
        {
            if (this != null)
            {
                string script = string.Format("DELETE FROM ClientP WHERE idC = '{0}'", this.idC);
                Program.Execute(script);
            }
        }
        public void editClientP()
        {
            string formatDate = "yyyy-MM-dd";
            if (this != null)
            {
                string script = string.Format("UPDATE ClientP " +
                                              "SET idProg = '{0}', nom = '{1}', prenom = '{2}', idAdresse = '{3}',codePostP = '{4}', nomRueP = '{5}', numRueP = '{6}', dateAdhesion = DATE('{7}'), dateFinAdhesion = DATE('{8}') " +
                                              "WHERE idC = '{9}'",this.idProg, this.nom, this.prenom, this.idAdresse, this.codePostP, this.nomRueP, this.numRueP, this.dateAdhesion.ToString(formatDate), this.dateFinAdhesion.ToString(formatDate), this.idC);
                Program.Execute(script);
            }
        }
    }
}
