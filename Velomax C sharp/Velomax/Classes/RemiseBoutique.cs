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
    internal class RemiseBoutique
    {
        public string idRemise;
        public int pourcentage;

        public RemiseBoutique(string idRemise, int pourcentage)
        {
            this.idRemise = idRemise;
            this.pourcentage = pourcentage;
        }
        public static List<RemiseBoutique> LoadAllRemisesBoutique()
        {
            string scriptRemise = "SELECT * FROM RemiseBoutique";

            ArrayList preRb;
            preRb = Program.Requete(scriptRemise);
            List<string> idRemise = new List<string>();
            List<int> pourcentage = new List<int>();
            string[] temp = new string[2];

            for (int i = 0; i < preRb.Count; i++)
            {
                temp = preRb[i].ToString().Split(',');
                for (int m = 0; m < temp.Length; m++)
                {
                    temp[m] = temp[m].Trim();
                }
                if (int.TryParse(temp[1], out var n2) == false)
                {
                    temp[1] = "0";
                }
                idRemise.Add(temp[0]);
                pourcentage.Add(Convert.ToInt32(temp[1]));
            }
            List<RemiseBoutique> RbList = new List<RemiseBoutique>();

            for (int j = 0; j < idRemise.Count; j++)
            {
                RbList.Add(new RemiseBoutique(idRemise[j], pourcentage[j]));
            }
            return RbList;
        }

        public RemiseBoutique LoadRemise()
        {
            string scriptRemise = string.Format("SELECT * FROM RemiseBoutique WHERE idRemise = '{0}'", this.idRemise);
            ArrayList preRb;
            preRb = Program.Requete(scriptRemise);
            List<string> idRemise = new List<string>();
            List<int> pourcentage = new List<int>();
            string[] temp = new string[2];

            for (int i = 0; i < preRb.Count; i++)
            {
                temp = preRb[i].ToString().Split(',');
                for (int m = 0; m < temp.Length; m++)
                {
                    temp[m] = temp[m].Trim();
                }
                if (int.TryParse(temp[1], out var n2) == false)
                {
                    temp[1] = "0";
                }
                idRemise.Add(temp[0]);
                pourcentage.Add(Convert.ToInt32(temp[1]));
            }
            RemiseBoutique rem = new RemiseBoutique(idRemise[0], pourcentage[0]);
            return rem;
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
            using (StreamWriter writer = new StreamWriter("json/RemiseB.json"))
            {
                writer.WriteLine(json);

            }

        }
        public void addRemiseBoutique()
        {
            if (this != null)
            {
                
                string script = string.Format("INSERT INTO `Velomax`.`RemiseBoutique` (`idRemise`,`pourcentage`) " +
                                              "VALUES('{0}', '{1}')", this.idRemise, this.pourcentage);
                Program.Execute(script);
            }
        }
        public void delRemiseBoutique()
        {
            if (this != null)
            {
                string script = string.Format("DELETE FROM RemiseBoutique WHERE idRemise = '{0}'", this.idRemise);
                Program.Execute(script);
            }
        }
        public void editRemiseBoutique()
        {
            if (this != null)
            {
                
                string script = string.Format("UPDATE RemiseBoutique " +
                                              "SET  pourcentage = '{0}'" +
                                              "WHERE idRemise = '{1}'", this.pourcentage, this.idRemise);
                Program.Execute(script);
            }
        }
    }
    

}
