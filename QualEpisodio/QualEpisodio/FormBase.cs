using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Microsoft.Win32;

namespace QualEpisodio
{
    public partial class FormBase : Form
    {
        public FormBase()
        {
            InitializeComponent();
            SysTrayApp();
        }

        private NotifyIcon  trayIcon;
        private ContextMenu trayMenu;
 
        public void SysTrayApp()
        {

            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon      = new NotifyIcon();
            trayIcon.Text = "Qual episódio?";

            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            
            Stream myStream = myAssembly.GetManifestResourceStream("QualEpisodio.icon.ico");
            
            trayIcon.Icon = new Icon(myStream, 40, 40);
            trayIcon.MouseUp += new MouseEventHandler(notifyIcon1_MouseUp);
            SetContextMenu();
            
            trayIcon.Visible     = true;
        }

        public void SetContextMenu()
        {
            // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();
            AssistirProximo();
            QualEpisodioAssistido();
            QualEpisodioBaixado();
            //trayMenu.MenuItems.Add("Sincronizar", Sincronizar);
            trayMenu.MenuItems.Add("Gerenciar Séries", Atualizar);
            WindowsStart();
            trayMenu.MenuItems.Add("Fechar", OnExit);

            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            
        }


        private void WindowsStart()
        {
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            // Check to see the current state (running at startup or not)
            if (rkApp.GetValue("QualEpisodio") == null)
            {
                // The value doesn't exist, the application is not set to run at startup
                trayMenu.MenuItems.Add("Iniciar com o Windows", ChangeStart).Checked = false;
            }
            else
            {
                // The value exists, the application is set to run at startup
                trayMenu.MenuItems.Add("Iniciar com o Windows", ChangeStart).Checked = true;
            }
        }
        private void ChangeStart(object sender, EventArgs e)
        {
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rkApp.GetValue("QualEpisodio") == null)
            {
                // Add the value in the registry so that the application runs at startup
                rkApp.SetValue("QualEpisodio", Application.ExecutablePath.ToString());
            }
            else
            {
                // Remove the value from the registry so that the application doesn't start
                rkApp.DeleteValue("QualEpisodio", false);
            }

            SetContextMenu();
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible       = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.
 
            base.OnLoad(e);
        }
 
        private void OnExit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            trayIcon.Dispose();
            Application.Exit();
        }

        private void QualEpisodioAssistido()
        {
            MenuItem qualEpisodioAssistido = new MenuItem("Último Assitido?");
            List<QualEpisodioModel> assistidas = DBHelper.Instance.Select(true);
            foreach (QualEpisodioModel serie in assistidas)
            {
                qualEpisodioAssistido.MenuItems.Add(string.Format("{0} - S{1}E{2}", serie.Serie, serie.Temporada.ToString("00"), serie.Episodio.ToString("00")), AtualizarAssistida);
            }

            trayMenu.MenuItems.Add(qualEpisodioAssistido);
            
        }
        private void QualEpisodioBaixado()
        {
            MenuItem qualEpisodioBaixado = new MenuItem("Último Baixado?");
            List<QualEpisodioModel> baixadas = DBHelper.Instance.Select(false);
            foreach (QualEpisodioModel serie in baixadas)
            {
                qualEpisodioBaixado.MenuItems.Add(string.Format("{0} - S{1}E{2}", serie.Serie, serie.Temporada.ToString("00"), serie.Episodio.ToString("00")), AtualizarBaixada);
            }
            trayMenu.MenuItems.Add(qualEpisodioBaixado);

        }

        List<string> videos;

        private void DirSearchRec(string sDir)
        {


            DirectoryInfo pasta = new DirectoryInfo(sDir);

            foreach (DirectoryInfo d in pasta.GetDirectories())
            {
                foreach (FileInfo f in GetFiles(d))
                {
                    videos.Add(f.FullName);
                }
                DirSearchRec(d.FullName);
            }

        }

        private FileInfo[] GetFiles(DirectoryInfo dir)
        {

            string filter = "mkv|rmvb|mp4|avi|mpeg|mpg";
            // ArrayList will hold all file names
            List<FileInfo> files = new List<FileInfo>();

            // Create an array of filter string
            string[] MultipleFilters = filter.Split('|');

            // for each filter find mathing file names
            foreach (string FileFilter in MultipleFilters)
            {
                // add found file names to array list
                files.AddRange(dir.GetFiles(string.Format("*.{0}", FileFilter)));
            }

            // returns string array of relevant file names
            return files.ToArray();
        }

        private void AssistirProximo()
        {
            MenuItem assistirProximo = new MenuItem("Assitir Próximo");
            List<QualEpisodioModel> assistidas = DBHelper.Instance.Select();
            String folder = @"C:\Users\Rovian\Documents\Shared";
            videos = new List<string>();
            DirSearchRec(folder);
            foreach (QualEpisodioModel serie in assistidas)
            {
                MenuItem item = VerificaDisponibilidade(serie);
                assistirProximo.MenuItems.Add(item);
            }
            trayMenu.MenuItems.Add(assistirProximo);
        }

        private MenuItem VerificaDisponibilidade(QualEpisodioModel serie)
        {
            //, AtualizarBaixada
            MenuItem item = new MenuItem();
            item.Text = string.Format("{0} - S{1}E{2}", serie.Serie, serie.Temporada.ToString("00"), (serie.Episodio + 1).ToString("00"));

            List<string> nome = serie.Serie.Split(' ').ToList();
            nome.RemoveAll(x => x.Length < 3);

            item.Enabled = false;
            foreach (string episodio in videos)
            {
                if (NomeBate(episodio, nome) && NumeracaoBate(episodio, serie))
                {
                    item.Enabled = true;
                    item.Click += (s, e) =>
                    {
                        System.Diagnostics.Process.Start(episodio);
                    };
                    break;
                }
            }
            
            
            return item;
        }

        private bool NumeracaoBate(string episodio, QualEpisodioModel serie)
        {
            if ((episodio.Contains(serie.Temporada.ToString()) && episodio.Contains((serie.Episodio + 1).ToString())))
            {
                if (episodio.IndexOf(serie.Temporada.ToString()) < episodio.IndexOf((serie.Episodio + 1).ToString()))
                    return true;
            }
            return false;
        }

        private bool NomeBate(string nome, List<string> nomes)
        {
            bool bate = true;
            foreach (string s in nomes)
            {
                if (!nome.ToLowerInvariant().Contains(s.ToLowerInvariant()))
                {
                    bate = false;
                    break;
                }
            }

            return bate;
        }
        private void Atualizar(object sender, EventArgs e)
        {
            new FormGerenciar(this).Show();
        }


        private void Sincronizar(object sender, EventArgs e)
        {

        }

        private void AbreEpisodio(object sender, EventArgs e)
        {
            
        }

        private void AtualizarAssistida(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            String serie = item.Text.Substring(0, (item.Text.LastIndexOf("-") - 1));
            QualEpisodio qualEpisodio = new QualEpisodio(serie, true, this);
            qualEpisodio.Show();
        }

        private void AtualizarBaixada(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            String serie = item.Text.Substring(0, (item.Text.LastIndexOf("-") - 1));
            QualEpisodio qualEpisodio = new QualEpisodio(serie, false, this);
            qualEpisodio.Show();
        }

        private void notifyIcon1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(trayIcon, null);
            }
        }
    }
}
