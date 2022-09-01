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
    internal class Commande
    {
        public string idCommande;
        public string idC;
        public string idB;
        public DateTime dateCom;
        public string adresseLivraison;
        public DateTime dateLivraison;
        public List<Velo> contenuCommande;
        public List<int> nbVelos;
        public List<Piece> supplement;
        public List<int> nbPieces;

        public Commande(string idCommande, string idC = "0", string idB = "0", DateTime dateCom = new DateTime(), string adresseLivraison = null, DateTime dateLivraison = new DateTime(), List<Velo> contenuCommande = null, List<int> nbVelos = null, List<Piece> supplement = null, List<int> nbPieces = null)
        {
            this.idCommande = idCommande;
            this.idC = idC;
            this.idB = idB;
            this.dateCom = dateCom;
            this.adresseLivraison = adresseLivraison;
            this.dateLivraison = dateLivraison;
            this.contenuCommande = contenuCommande;
            this.nbVelos = nbVelos;
            this.supplement = supplement;
            this.nbPieces = nbPieces;
            this.contenuCommande = new List<Velo>();
            this.nbVelos = new List<int>();
            this.supplement = new List<Piece>();
            this.nbPieces = new List<int>();
        }
        public static List<Commande> LoadAllCommandes()
        {
            string scriptCommande = "SELECT * FROM Commande";
            ArrayList preCommande;
            preCommande = Program.Requete(scriptCommande);
            List<string> idCommande = new List<string>();
            List<string> idC = new List<string>();
            List<string> idB = new List<string>();
            List<DateTime> dateCommande = new List<DateTime>();
            List<string> adresseLivraison = new List<string>();
            List<DateTime> dateLivraison = new List<DateTime>();


            string[] temp = new string[7];
            string conv;

            for (int i = 0; i < preCommande.Count; i++)
            {
                conv = preCommande[i].ToString().Trim();
                temp = conv.Split(',');
                //temp = preCommande[i].ToString().Split(',');
                if (String.IsNullOrWhiteSpace(temp[1]))
                {
                    temp[1] = "null";
                }
                if (String.IsNullOrWhiteSpace(temp[2]))
                {
                    temp[2] = "null";
                }
                if (DateTime.TryParse(temp[3], out var n3) == false)
                {
                    temp[3] = "0000-00-00";
                }
                if (String.IsNullOrWhiteSpace(temp[4]))
                {
                    temp[4] = "null";
                }
                if (DateTime.TryParse(temp[5], out var n4) == false)
                {
                    temp[5] = "0000-00-00";
                }

                idCommande.Add(temp[0]);
                idC.Add(temp[1]);
                idB.Add(temp[2]);
                DateTime tempDateCom;
                DateTime.TryParse(temp[3], out tempDateCom);
                dateCommande.Add(tempDateCom);
                adresseLivraison.Add(temp[4]);
                DateTime tempDateLiv;
                DateTime.TryParse(temp[5], out tempDateLiv);
                dateLivraison.Add(tempDateLiv);
            }
            List<Commande> commandeList = new List<Commande>();

            for (int j = 0; j < idCommande.Count; j++)
            {
                commandeList.Add(new Commande(idCommande[j], idC[j], idB[j], dateCommande[j], adresseLivraison[j], dateLivraison[j]));
            }
            
            /*--------------------------------------*/

            string scriptSupplement;
            ArrayList curSupplement;

            string[] tempSupplement = new string[4];
            string convSup;
            for (int k = 0; k < commandeList.Count; k++)
            {
                scriptSupplement = String.Format("SELECT * FROM Supplement WHERE idCommande = '{0}'", commandeList[k].idCommande);
                curSupplement = Program.Requete(scriptSupplement);
                for (int l = 0; l < curSupplement.Count; l++)
                {
                    convSup = curSupplement[l].ToString().Trim();
                    tempSupplement = convSup.Split(',');
                    for (int m = 0; m < tempSupplement.Length; m++)
                    {
                        tempSupplement[m] = tempSupplement[m].Trim();
                    }
                    if (int.TryParse(tempSupplement[2], out var n2) == false)
                    {
                        tempSupplement[2] = "0";
                    }
                    Piece tempP = new Piece(tempSupplement[0]);
                    Piece curP = tempP.loadPiece();
                    commandeList[k].supplement.Add(curP);
                    commandeList[k].nbPieces.Add(Convert.ToInt32(tempSupplement[2]));
                }
            }

            /*---------------------------------------*/

            string scriptContenu;
            ArrayList curContenu;

            string[] tempContenu = new string[4];
            string convCont;
            for (int k = 0; k < commandeList.Count; k++)
            {
                scriptContenu = string.Format("SELECT * FROM ContenuCommande WHERE idCommande = '{0}'", commandeList[k].idCommande);
                curContenu = Program.Requete(scriptContenu);
                for (int l = 0; l < curContenu.Count; l++)
                {
                    convCont = curContenu[l].ToString();
                    tempContenu = convCont.Trim().Split(',');
                    for (int m = 0; m < tempContenu.Length; m++)
                    {
                        tempContenu[m] = tempContenu[m].Trim();
                    }
                    if (int.TryParse(tempContenu[2], out var n2) == false)
                    {
                        tempContenu[2] = "0";
                    }
                    Velo tempV = new Velo(tempContenu[1]);
                    Velo curV = tempV.LoadVelo();
                    commandeList[k].contenuCommande.Add(curV);
                    commandeList[k].nbVelos.Add(Convert.ToInt32(tempContenu[2]));
                }
            }

            return commandeList;
        }
        public Commande Loadcommande()
        {
            string scriptCommande = string.Format("SELECT * FROM Commande where idCommande = '{0}'", this.idCommande);
            ArrayList preCommande;
            preCommande = Program.Requete(scriptCommande);
            List<string> idCommande = new List<string>();
            List<string> idC = new List<string>();
            List<string> idB = new List<string>();
            List<DateTime> dateCommande = new List<DateTime>();
            List<string> adresseLivraison = new List<string>();
            List<DateTime> dateLivraison = new List<DateTime>();

            string[] temp = new string[7];
            string conv;

            for (int i = 0; i < preCommande.Count; i++)
            {
                conv = preCommande[i].ToString().Trim();
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
                if (DateTime.TryParse(temp[3], out var n3) == false)
                {
                    temp[3] = "0000-00-00";
                }
                if (String.IsNullOrWhiteSpace(temp[4]))
                {
                    temp[4] = "null";
                }
                if (DateTime.TryParse(temp[5], out var n4) == false)
                {
                    temp[5] = "0000-00-00";
                }

                idCommande.Add(temp[0]);
                idC.Add(temp[1]);
                idB.Add(temp[2]);
                DateTime tempDateCom;
                DateTime.TryParse(temp[3], out tempDateCom);
                dateCommande.Add(tempDateCom);
                adresseLivraison.Add(temp[4]);
                DateTime tempDateLiv;
                DateTime.TryParse(temp[5], out tempDateLiv);
                dateLivraison.Add(tempDateLiv);
            }
            List<Commande> commandeList = new List<Commande>();

            //Commande c = new Commande(idCommande[0], idC[0], idB[0], dateCommande[0], adresseLivraison[0], dateLivraison[0]);
            for (int j = 0; j < idCommande.Count; j++)
            {
                commandeList.Add(new Commande(idCommande[j], idC[j], idB[j], dateCommande[j], adresseLivraison[j], dateLivraison[j]));
            }

            /*--------------------------------------*/

            string scriptSupplement;
            ArrayList curSupplement;

            string[] tempSupplement = new string[4];
            string convSup;

            for (int k = 0; k < commandeList.Count; k++)
            {
                scriptSupplement = String.Format("SELECT * FROM Supplement WHERE idCommande = '{0}'", commandeList[k].idCommande);
                curSupplement = Program.Requete(scriptSupplement);
                for (int l = 0; l < curSupplement.Count; l++)
                {
                    convSup = curSupplement[l].ToString().Trim();
                    tempSupplement = convSup.Split(',');
                    for (int m = 0; m < tempSupplement.Length; m++)
                    {
                        tempSupplement[m] = tempSupplement[m].Trim();
                    }
                    if (int.TryParse(tempSupplement[2], out var n2) == false)
                    {
                        tempSupplement[2] = "0";
                    }
                    Piece tempP = new Piece(tempSupplement[0]);
                    Piece curP = tempP.loadPiece();
                    commandeList[k].supplement.Add(curP);
                    commandeList[k].nbPieces.Add(Convert.ToInt32(tempSupplement[2]));
                }
            }

            /*---------------------------------------*/

            string scriptContenu;
            ArrayList curContenu;

            string[] tempContenu = new string[4];
            string convCont;

            for (int k = 0; k < commandeList.Count; k++)
            {
                scriptContenu = string.Format("SELECT * FROM ContenuCommande WHERE idCommande = '{0}'", commandeList[k].idCommande);
                curContenu = Program.Requete(scriptContenu);
                for (int l = 0; l < curContenu.Count; l++)
                {
                    convCont = curContenu[l].ToString().Trim();
                    tempContenu = convCont.Split(',');
                    for (int m = 0; m < tempContenu.Length; m++)
                    {
                        tempContenu[m] = tempContenu[m].Trim();
                    }
                    if (int.TryParse(tempContenu[2], out var n2) == false)
                    {
                        tempContenu[2] = "0";
                    }
                    Velo tempV = new Velo(tempContenu[1]);
                    Velo curV = tempV.LoadVelo();
                    commandeList[k].contenuCommande.Add(curV);
                    commandeList[k].nbVelos.Add(Convert.ToInt32(tempContenu[2]));
                }
            }
            return commandeList[0];
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
            List<Commande> pieceList = Commande.LoadAllCommandes();
            for (int i = 0; i < pieceList.Count; i++)
            {
                json = json + pieceList[i].Json() + ",";
            }
            json = "[" + json + "]";
            using (StreamWriter writer = new StreamWriter("json/Commande.json"))
            {
                writer.WriteLine(json);
            }
        }
        public void addCommande()
        {
            if (this != null)
            {
                string formatDate = "yyyy-MM-dd";
                string script = string.Format("INSERT INTO `Velomax`.`Commande` (`idCommande`,`idC`,`idB`,`dateCom`,`adresseLivraison`,`dateLivraison`) " +
                                              "VALUES('{0}', '{1}', '{2}', DATE('{3}'), '{4}', DATE('{5}'))", this.idCommande, this.idC, this.idB, this.dateCom.ToString(formatDate), this.adresseLivraison, this.dateLivraison.ToString(formatDate));
                Program.Execute(script);
            }
            if (this != null && this.contenuCommande != null)
            {
                string scriptContenu = string.Empty;
                for (int i = 0; i < this.contenuCommande.Count; i++)
                {
                    scriptContenu = string.Format("INSERT INTO `Velomax`.`ContenuCommande` (`idCommande`, `idVelo`, `nbItemVelo`) " +
                                           "VALUES('{0}', '{1}', '{2}')", this.idCommande, this.contenuCommande[i].idVelo, this.nbVelos[i]);
                    Program.Execute(scriptContenu);
                }
            }
            if ( this != null && this.supplement != null)
            {
                string scriptSupplement = string.Empty;
                for (int i = 0; i < this.supplement.Count; i++)
                {
                    scriptSupplement = string.Format("INSERT INTO `Velomax`.`Supplement` (`idCommande`, `idPiece`, `nbItemPiece`) " +
                                           "VALUES('{0}', '{1}', '{2}')", this.idCommande, this.supplement[i].idPiece, this.nbPieces[i]);
                    Program.Execute(scriptSupplement);
                }
            }
        }
        public void delCommande()
        {
            if (this != null)
            {
                string script = string.Format("DELETE FROM Commande WHERE idCommande = '{0}'", this.idCommande);
                Program.Execute(script);
            }
            if (this != null && this.contenuCommande != null)
            {
                string scriptContenu = string.Empty;
                for (int i = 0; i < this.contenuCommande.Count; i++)
                {
                    scriptContenu = string.Format("DELETE FROM ContenuCommande WHERE idCommande = '{0}' and idVelo = '{1}'", this.idCommande, this.contenuCommande[i].idVelo);
                    Program.Execute(scriptContenu);
                }
            }
            if (this != null && this.supplement != null)
            {
                string scriptSupplement = string.Empty;
                for (int i = 0; i < this.supplement.Count; i++)
                {
                    scriptSupplement = string.Format("DELETE FROM Supplement WHERE idCommande = '{0}' and idPiece = '{1}'", this.idCommande, this.supplement[i].idPiece);
                    Program.Execute(scriptSupplement);
                }
            }
        }
        public void editCommande()
        {
            if (this != null)
            {
                string formatDate = "yyyy-MM-dd";
                string script = string.Format("UPDATE Commande " +
                                              "SET idC = '{0}', idB = '{1}', dateCom = DATE('{2}'), adresseLivraison = '{3}', dateLivraison = DATE('{4}')" +
                                              "WHERE idCommande = '{5}'", this.idC, this.idB, this.dateCom.ToString(formatDate), this.adresseLivraison, this.dateLivraison.ToString(formatDate), this.idCommande);
                Program.Execute(script);
            }
            if (this != null && this.contenuCommande != null)
            {
                string scriptContenu = string.Empty;
                for (int i = 0; i < this.contenuCommande.Count; i++)
                {
                    scriptContenu = string.Format("UPDATE ContenuCommande " +
                                                "SET nbItemVelo = '{0}' " +
                                                "WHERE idCommande = '{1}' and idVelo = '{2}'", this.nbVelos[i], this.idCommande, this.contenuCommande[i].idVelo);
                    Program.Execute(scriptContenu);
                }
            }
            if (this != null && this.supplement != null)
            {
                string scriptSupplement = string.Empty;
                for (int i = 0; i < this.supplement.Count; i++)
                {
                    scriptSupplement = string.Format("UPDATE Supplement " +
                                                "SET nbItemPiece = '{0}' " +
                                                "WHERE idCommande = '{1}' and idPiece = '{2}'", this.nbPieces[i], this.idCommande, this.supplement[i].idPiece);
                    Program.Execute(scriptSupplement);
                }
            }
        }
        public bool checkCommande()
        {
            string scriptPiece = "SELECT idPiece, quantiteStock FROM Piece";
            string scriptVelo = "SELECT idVelo, stockVelo FROM Velo";
            ArrayList prePiece;
            ArrayList preVelo;
            prePiece = Program.Requete(scriptPiece);
            preVelo = Program.Requete(scriptVelo);
            List<string> idPiece = new List<string>();
            List<int> nbPiece = new List<int>();
            List<string> idVelo = new List<string>();
            List<int> nbVelo = new List<int>();

            string[] temp = new string[2];

            for (int i = 0; i < prePiece.Count; i++)
            {
                temp = prePiece[i].ToString().Split(',');
                if (int.TryParse(temp[1], out var n) && String.IsNullOrWhiteSpace(temp[0]) == false)
                {
                    idPiece.Add(temp[0]);
                    nbPiece.Add(Convert.ToInt32(temp[1]));
                }
            }
            for (int i = 0; i < preVelo.Count; i++)
            {
                temp = preVelo[i].ToString().Split(',');
                if (int.TryParse(temp[1], out var n) && String.IsNullOrWhiteSpace(temp[0]) == false)
                {
                    idVelo.Add(temp[0]);
                    nbVelo.Add(Convert.ToInt32(temp[1]));
                }
            }

            for (int i = 0; i < this.contenuCommande.Count; i++)
            {
                int ivel = idVelo.IndexOf(this.contenuCommande[i].idVelo);
                if (ivel != -1)
                {
                    if (nbVelo[ivel] < this.nbVelos[i])
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            for (int i = 0; i < this.supplement.Count; i++)
            {
                int idp = idPiece.IndexOf(this.supplement[i].idPiece);
                if (idp != -1)
                {
                    if (nbPiece[idp] < this.nbPieces[i])
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
