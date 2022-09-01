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
    internal class ProgrammeFidelite
    {
        public string idProgramme;
        public int typeProg;

        public ProgrammeFidelite(string idProgramme, int typeProg)
        {
            this.idProgramme = idProgramme;
            if (typeProg < 5 && typeProg > -1)
            {
                this.typeProg = typeProg;
            }
        }
        public static List<ProgrammeFidelite> LoadAllProgrammesFidelite()
        {
            string scriptRemise = "SELECT * FROM ProgrammeFidelite";

            ArrayList prePf;
            prePf = Program.Requete(scriptRemise);
            List<string> idProgramme = new List<string>();
            List<int> typeProg = new List<int>();
            string[] temp = new string[2];

            for (int i = 0; i < prePf.Count; i++)
            {
                temp = prePf[i].ToString().Split(',');
                for (int m = 0; m < temp.Length; m++)
                {
                    temp[m] = temp[m].Trim();
                }
                if (int.TryParse(temp[1], out var n2) == false)
                {
                    temp[1] = "0";
                }
                idProgramme.Add(temp[0]);
                typeProg.Add(Convert.ToInt32(temp[1]));
            }
            List<ProgrammeFidelite> PfList = new List<ProgrammeFidelite>();

            for (int j = 0; j < idProgramme.Count; j++)
            {
                PfList.Add(new ProgrammeFidelite(idProgramme[j], typeProg[j]));
            }
            return PfList;
        }
        public ProgrammeFidelite LoadProgramme()
        {
            string scriptRemise = string.Format("SELECT * FROM ProgrammeFidelite WHERE idProgramme = '{0}'", this.idProgramme);

            ArrayList prePf;
            prePf = Program.Requete(scriptRemise);
            List<string> idProgramme = new List<string>();
            List<int> typeProg = new List<int>();
            string[] temp = new string[2];

            for (int i = 0; i < prePf.Count; i++)
            {
                temp = prePf[i].ToString().Split(',');
                for (int m = 0; m < temp.Length; m++)
                {
                    temp[m] = temp[m].Trim();
                }
                if (int.TryParse(temp[1], out var n2) == false)
                {
                    temp[1] = "0";
                }
                idProgramme.Add(temp[0]);
                typeProg.Add(Convert.ToInt32(temp[1]));
            }
            ProgrammeFidelite prog = new ProgrammeFidelite(idProgramme[0], typeProg[0]);
            return prog;
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
            using (StreamWriter writer = new StreamWriter("json/ProgramF.json"))
            {
                writer.WriteLine(json);

            }

        }
        public void addProgrammeFidelite()
        {
            if (this != null)
            {
                string script = string.Format("INSERT INTO `Velomax`.`ProgrammeFidelite` (`idProgramme`,`typeProg`) " +
                                              "VALUES('{0}', '{1}')", this.idProgramme, this.typeProg);
                Program.Execute(script);
            }
        }
        public void delProgrammeFidelite()
        {
            if (this != null)
            {
                string script = string.Format("DELETE FROM ProgrammeFidelite WHERE idProgramme = '{0}'", this.idProgramme);
                Program.Execute(script);
            }
        }
        public void editProgrammeFidelite()
        {
            if (this != null)
            {
                string script = string.Format("UPDATE ProgrammeFidelite " +
                                              "SET  typeProg = {0} " +
                                              "WHERE idProgramme = '{1}'", this.typeProg, this.idProgramme);
                Program.Execute(script);
            }
        }

    }
}
