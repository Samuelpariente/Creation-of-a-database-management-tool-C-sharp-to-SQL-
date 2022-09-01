using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using MathNet.Numerics.Statistics;
using System.Web.Script.Serialization;

namespace Velomax
{
    internal class ClientB
    {
        public string idB;
        public string idRemise;
        public string idAdresse;
        public string codePostB;
        public string nomRueB;
        public int numRueB;
        public string telephoneB;
        public string emailB;
        public string nomContactB;

        public ClientB(string idB,string idRemise = "0", string idAdresse = null, string codePostB = "00000", string nomRueB = null, int numRueB = 0, string telephoneB = null, string emailB = null,string nomContactB = null)
        {
            this.idB = idB;
            this.idRemise = idRemise;
            this.idAdresse = idAdresse;
            this.codePostB = codePostB;
            this.nomRueB = nomRueB;
            this.numRueB = numRueB;
            this.telephoneB = telephoneB;
            this.emailB = emailB;
            this.nomContactB = nomContactB;
        }
        public static List<ClientB> LoadAllClientsB()
        {
            string scriptClientB = "SELECT * FROM ClientB";

            ArrayList prePiece;
            prePiece = Program.Requete(scriptClientB);
            List<string> idB = new List<string>();
            List<string> idRemise = new List<string>();
            List<string> idAdresse = new List<string>();
            List<string> codePostB = new List<string>();
            List<string> nomRueB = new List<string>();
            List<int> numRueB = new List<int>();
            List<string> telephoneB = new List<string>();
            List<string> emailB = new List<string>();
            List<string> nomContactB = new List<string>();

            string[] temp = new string[9];

            for (int i = 0; i < prePiece.Count; i++)
            {
                temp = prePiece[i].ToString().Split(',');
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
                if (int.TryParse(temp[5], out var n2) == false)
                {
                    temp[5] = "0";
                }
                if (String.IsNullOrWhiteSpace(temp[6]))
                {
                    temp[6] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[7]))
                {
                    temp[7] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[8]))
                {
                    temp[8] = "null";
                }

                idB.Add(temp[0]);
                idRemise.Add(temp[1]);
                idAdresse.Add(temp[2]);
                codePostB.Add(temp[3]);
                nomRueB.Add(temp[4]);
                numRueB.Add(Convert.ToInt32(temp[5]));
                telephoneB.Add(temp[6]);
                emailB.Add(temp[7]);
                nomContactB.Add(temp[8]);
            }
            List<ClientB> ClientBList = new List<ClientB>();

            for (int j = 0; j < idB.Count; j++)
            {
                ClientBList.Add(new ClientB(idB[j], idRemise[j], idAdresse[j], codePostB[j], nomRueB[j], numRueB[j], telephoneB[j], emailB[j], nomContactB[j]));
            }
            return ClientBList;
        }
        public ClientB LoadClienB()
        {
            string scriptClientB = String.Format("SELECT * FROM ClientB where idB = '{0}'", this.idB);
            ArrayList prePiece;
            prePiece = Program.Requete(scriptClientB);
            List<string> idB = new List<string>();
            List<string> idRemise = new List<string>();
            List<string> idAdresse = new List<string>();
            List<string> codePostB = new List<string>();
            List<string> nomRueB = new List<string>();
            List<int> numRueB = new List<int>();
            List<string> telephoneB = new List<string>();
            List<string> emailB = new List<string>();
            List<string> nomContactB = new List<string>();

            string[] temp = new string[9];

            for (int i = 0; i < prePiece.Count; i++)
            {
                temp = prePiece[i].ToString().Split(',');
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
                if (int.TryParse(temp[5], out var n2) == false)
                {
                    temp[5] = "0";
                }
                if (String.IsNullOrWhiteSpace(temp[6]))
                {
                    temp[6] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[7]))
                {
                    temp[7] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[8]))
                {
                    temp[8] = "null";
                }

                idB.Add(temp[0]);
                idRemise.Add(temp[1]);
                idAdresse.Add(temp[2]);
                codePostB.Add(temp[3]);
                nomRueB.Add(temp[4]);
                numRueB.Add(Convert.ToInt32(temp[5]));
                telephoneB.Add(temp[6]);
                emailB.Add(temp[7]);
                nomContactB.Add(temp[8]);
            }
            ClientB ClientB = new ClientB(idB[0], idRemise[0], idAdresse[0], codePostB[0], nomRueB[0], numRueB[0], telephoneB[0], emailB[0], nomContactB[0]);
            return ClientB;
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
            using (StreamWriter writer = new StreamWriter("json/ClienB.json"))
            {
                writer.WriteLine(json);

            }

        }

        public void addClientB()
        {
            if (this != null)
            {
                string script = string.Format("INSERT INTO `Velomax`.`ClientB` (`idB`,`idRemise`,`idAdresse`,`codePostB`,`nomRueB`,`numRueB`,`telephoneB`,`emailB`,`nomContactB`) " +
                                "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')", this.idB, this.idRemise, this.idAdresse, this.codePostB, this.nomRueB, this.numRueB, this.telephoneB, this.emailB, this.nomContactB);
                Program.Execute(script);
            }
        }
        
        public void delClientB()
        {
            if (this != null)
            {
                string script = string.Format("DELETE FROM ClientB WHERE idB = '{0}'", this.idB);
                Program.Execute(script);
            }
        }
        public void editClientB()
        {
            if (this != null)
            {
                string script = string.Format("UPDATE ClientB " +
                                              "SET idRemise = '{0}', idAdresse = '{1}', codePostB = '{2}',nomRueB  = '{3}', numRueB = '{4}', telephoneB = '{5}', emailB = '{6}', nomContactB = '{7}' " +
                                              "WHERE idB = '{8}'", this.idRemise, this.idAdresse, this.codePostB, this.nomRueB, this.numRueB, this.telephoneB,this.emailB,this.nomContactB, this.idB);
                Program.Execute(script);
            }
        }
    }
}
