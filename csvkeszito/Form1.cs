using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace csvkeszito
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class EtteremAdatok
        {
            public string kategoria;
            public string megnevezes;
            public int kcal;
            public int szenhidrat;
            public int ar;
        }
        public EtteremAdatok etteremFeltoltese = new EtteremAdatok();
        public List<EtteremAdatok> etterem = new List<EtteremAdatok>();
        public string seged;
        public string seged2;
        public int intseged;
        public string[] Beolvaso(string fileName)
        {
            string[] a = new string[170];

            FileStream folyam = new FileStream(fileName, FileMode.Open);
            StreamReader olvas = new StreamReader(folyam);

            string elso = olvas.ReadLine();
            string[] resz;
            string kategoria = "";
            //int numericValue;
            
            while (!olvas.EndOfStream)
            {
                elso = olvas.ReadLine();
                resz = elso.Split('\t');
                EtteremAdatok etteremFeltoltese = new EtteremAdatok();
                //!int.TryParse(resz[0], out numericValue)
                if (resz[0] != "")
                {
                    kategoria = resz[0];
                    if (kategoria[kategoria.Length - 1] == ' ')
                    {
                        seged = kategoria;
                        kategoria = "";
                        for (int i = 0; i < seged.Length - 1; i++)
                        {
                            kategoria += seged[i];
                        }
                    }
                }
                etteremFeltoltese.kategoria = kategoria;
                for (int i = 2; i <= 5; i++)
                {
                    seged = resz[i];
                    if (seged[0] == ' ')
                    {
                        seged2 = seged;
                        seged = "";
                        for (int j = 1; j < seged2.Length; j++)
                        {
                            seged += seged2[j];
                        }
                    }
                    //label1.Text += $"{seged};";
                    if (i == 2)
                        etteremFeltoltese.megnevezes = seged;
                    if (i == 3)
                        etteremFeltoltese.kcal = Convert.ToInt32(seged);
                    if (i == 4)
                        etteremFeltoltese.szenhidrat = Convert.ToInt32(seged);
                    if (i == 5)
                    {
                        if (seged == " " || seged == "\t")
                        {
                            seged = resz[i + 1];
                            seged2 = seged;
                            seged = "";
                            for (int j = 0; j < seged2.Length; j++)
                            {
                                if (seged2[j] != Convert.ToChar(' '))
                                {
                                    seged += seged2[j];
                                }
                            }
                        }
                        else
                        {
                            seged2 = seged;
                            seged = "";
                            for (int j = 0; j < seged2.Length; j++)
                            {
                                if (seged2[j] != Convert.ToChar(' '))
                                {
                                    seged += seged2[j];
                                }
                            }
                        }
                        etteremFeltoltese.ar = Convert.ToInt32(seged);
                    }
                }
                //label1.Text += $"\n";
                etterem.Add(etteremFeltoltese);
            }
            folyam.Close();
            return a;
        }

        /*
            try
            {
                if (feltel)
                {
                    throw new Exception("üzenet");
                }
            }
            catch (Exception e)
            {
                label1.Text = e.Message;
                throw;
            }
            

            .Replace("; ","");
         */

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            label2.Text = "";
            Beolvaso("étterem.txt");
            for (int i = 0; i < etterem.Count; i++)
            {
                label1.Text += $"{etterem[i].kategoria};{etterem[i].megnevezes};{etterem[i].kcal};{etterem[i].szenhidrat};{etterem[i].ar}\n";
                intseged += etterem[i].ar;
            }
            label2.Text = $"{intseged / etterem.Count}";
        }
    }
}
