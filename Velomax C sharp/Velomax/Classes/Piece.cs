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
    public class Piece
    {
        public string idPiece;
        public string descriptionP;
        public string numCatalogue;
        public float prixUnitPiece;
        public DateTime dateImPiece;
        public DateTime dateDpPiece;
        public int quantiteStock;

        public Piece(string idPiece, string descriptionP = null, string numCatalogue = null, float prixUnitPiece = 1, int quantiteStock = 1, DateTime dateImPiece = new DateTime(), DateTime dateDpPiece = new DateTime())
        {
            this.idPiece = idPiece;
            this.descriptionP = descriptionP;
            this.numCatalogue = numCatalogue;
            this.prixUnitPiece = prixUnitPiece;
            this.dateImPiece = dateImPiece;
            this.dateDpPiece = dateDpPiece;
            this.quantiteStock = quantiteStock;
        }
        public Piece(Piece clone)
        {
            this.idPiece = clone.idPiece;
            this.descriptionP = clone.descriptionP;
            this.numCatalogue = clone.numCatalogue;
            this.prixUnitPiece = clone.prixUnitPiece;
            this.prixUnitPiece = clone.prixUnitPiece;
            this.dateImPiece = clone.dateImPiece;
            this.dateDpPiece = clone.dateDpPiece;
            this.quantiteStock = clone.quantiteStock;
        }
        public static List<Piece> LoadAllPieces()
        {
            string scriptPiece = "SELECT * FROM Piece";
            ArrayList prePiece;
            prePiece = Program.Requete(scriptPiece);
            List<string> idPiece = new List<string>();
            List<string> descriptionP = new List<string>();
            List<string> numCatalogue = new List<string>();
            List<float> prixUnitPiece = new List<float>();
            List<int> quantiteStock = new List<int>();
            List<DateTime> dateImPiece = new List<DateTime>();
            List<DateTime> dateDpPiece = new List<DateTime>();

            string[] temp = new string[8];
            string conv;

            for (int i = 0; i < prePiece.Count; i++)
            {
                conv = prePiece[i].ToString().Trim();
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

                idPiece.Add(temp[0]);
                descriptionP.Add(temp[1]);
                numCatalogue.Add(temp[2]);
                prixUnitPiece.Add(float.Parse(temp[3]));
                quantiteStock.Add(Convert.ToInt32(temp[5]));
                DateTime tempDateIm;
                DateTime.TryParse(temp[6], out tempDateIm);
                dateImPiece.Add(tempDateIm);
                DateTime tempDateDp;
                DateTime.TryParse(temp[7], out tempDateDp);
                dateDpPiece.Add(tempDateDp);
            }
            List<Piece> pieceList = new List<Piece>();

            for (int j = 0; j < idPiece.Count; j++)
            {
                pieceList.Add(new Piece(idPiece[j], descriptionP[j], numCatalogue[j], prixUnitPiece[j], quantiteStock[j], dateImPiece[j], dateDpPiece[j]));
            }
            return pieceList;
        }
        public Piece loadPiece()
        {
            string scriptPiece = String.Format("SELECT * FROM Piece where idpiece = '{0}'", this.idPiece);
            ArrayList prePiece;
            prePiece = Program.Requete(scriptPiece);
            List<string> idPiece = new List<string>();
            List<string> descriptionP = new List<string>();
            List<string> numCatalogue = new List<string>();
            List<float> prixUnitPiece = new List<float>();
            List<int> quantiteStock = new List<int>();
            List<DateTime> dateImPiece = new List<DateTime>();
            List<DateTime> dateDpPiece = new List<DateTime>();

            string[] temp = new string[7];

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
                if (float.TryParse(temp[3], out var n1) == false)
                {
                    temp[3] = "0";
                }
                if (int.TryParse(temp[4], out var n2) == false)
                {
                    temp[4] = "0";
                }
                if (DateTime.TryParse(temp[5], out var n3) == false)
                {
                    temp[5] = "0000-00-00";
                }
                if (DateTime.TryParse(temp[6], out var n4) == false)
                {
                    temp[6] = "0000-00-00";
                }

                idPiece.Add(temp[0]);
                descriptionP.Add(temp[1]);
                numCatalogue.Add(temp[2]);
                prixUnitPiece.Add(float.Parse(temp[3]));
                quantiteStock.Add(Convert.ToInt32(temp[4]));
                DateTime tempDateIm;
                DateTime.TryParse(temp[5], out tempDateIm);
                dateImPiece.Add(tempDateIm);
                DateTime tempDateDp;
                DateTime.TryParse(temp[6], out tempDateDp);
                dateDpPiece.Add(tempDateDp);

            }
            Piece p = new Piece(idPiece[0], descriptionP[0], numCatalogue[0], prixUnitPiece[0], quantiteStock[0], dateImPiece[0], dateDpPiece[0]);
            return p;
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
            using (StreamWriter writer = new StreamWriter("json/piece.json"))
            {
                writer.WriteLine(json);

            }

        }
        public void addPiece()
        {
            if (this != null)
            {
                string formatDate = "yyyy-MM-dd";
                string script = string.Format("INSERT INTO `Velomax`.`Piece` (`idPiece`,`descriptionP`,`numCatalogue`,`PrixUnitPiece`,`quantiteStock`,`dateImPiece`,`dateDpPiece`) " +
                                              "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', DATE('{5}'), DATE('{6}'))", this.idPiece, this.descriptionP, this.numCatalogue, this.prixUnitPiece, this.quantiteStock, this.dateImPiece.ToString(formatDate), this.dateDpPiece.ToString(formatDate));
                Program.Execute(script);
            }
        }
        public void delPiece()
        {
            if (this != null)
            {
                string script = string.Format("DELETE FROM Piece WHERE idPiece = '{0}'", this.idPiece);
                Program.Execute(script);
            }
        }
        public void editPiece()
        {
            if (this != null)
            {
                string formatDate = "yyyy-MM-dd";
                string script = string.Format("UPDATE Piece " +
                                              "SET descriptionP = '{0}', numCatalogue = '{1}', PrixUnitPiece = '{2}', quantiteStock = '{3}', dateImPiece = DATE('{4}'), dateDpPiece = DATE('{5}')" +
                                              "WHERE idPiece = '{6}'", this.descriptionP, this.numCatalogue, this.prixUnitPiece, this.quantiteStock, this.dateImPiece.ToString(formatDate), this.dateDpPiece.ToString(formatDate), this.idPiece);
                Program.Execute(script);
            }
        }
        public int stock()
        {
            ArrayList arr = Program.Requete(string.Format("select quantiteStock from Piece where idPiece = '{0}'", this.idPiece));
            int nb = 0;
            nb = Convert.ToInt32(arr[0].ToString().Split(',')[0]);
            return nb;
        }
    }
}
