using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Net;
using System.Net.Sockets;
using System.Globalization;

namespace CanerCandan_21897853             // HOCAM EĞER İSİM ŞİFRE İSTERSE  :  Acc : MasterFF / Pass : 4565123644aa  // EntityFramework ' ü Sql isim şifresine göre ayarladım. 
{
    public partial class Form1 : Form
    {
        SpeechEntities db;
        
        SpeechRecognitionEngine recoEngine = new SpeechRecognitionEngine();
        public Form1()
        {
            InitializeComponent();
        }
        private void btnKonus_Click(object sender, EventArgs e)
        {
            btnKonus.Text = "Dinliyorum...";
            sestanima_ayarlari();
            recoEngine.RecognizeAsync();
        }
        public void sestanima_ayarlari()
        {
            string[] ihtimaller = { "hi", "hello" };
            Choices secenekler = new Choices(ihtimaller);
            Grammar grammer = new Grammar(new GrammarBuilder(secenekler));
            recoEngine.LoadGrammar(grammer);
            recoEngine.SetInputToDefaultAudioDevice();
            recoEngine.SpeechRecognized += ses_tanıdığında;
        }
        private void ses_tanıdığında(object sender, SpeechRecognizedEventArgs e)
        {
            if(e.Result.Text=="hi")
            {
                txtKonus.Text = "hi";
            }
            else if (e.Result.Text == "hello")
            {
                txtKonus.Text = "hello";

            }
   
        }
        private void AddEventHandler()
        {
            txtKonus.BindingContextChanged += new EventHandler(BindingContext_Changed);
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        private void BindingContext_Changed(object sender, EventArgs e)
        {
            if(txtKonus.Text == "hello")
            {
                Save_data();
            }
        }
        public void Save_data()
        {

            try
            {
                db = new SpeechEntities();
                AddEventHandler();
                tbl_bilgiler info = new CanerCandan_21897853.tbl_bilgiler();
                info.Adi = txtAdi.Text;
                info.Soyadi = txtSoyadi.Text;
                info.Ogr_No = txtOgrNo.Text;
                DateTime localDate = DateTime.Now;
                info.Zaman = localDate;
                string LocalIpAdress = GetLocalIPAddress();
                info.IP = LocalIpAdress;
                MessageBox.Show("Adi : " + info.Adi + "Soyadi : " + info.Soyadi + "Ogr No : " + info.Ogr_No + "Zaman : " + info.Zaman + "İP : " + info.IP);
                db.tbl_bilgiler.Add(info);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }


        }

    }
}
