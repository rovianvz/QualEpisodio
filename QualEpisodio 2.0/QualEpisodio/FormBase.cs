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

        private void Atualizar(object sender, EventArgs e)
        {
            new FormGerenciar(this).Show();
        }


        private void Sincronizar(object sender, EventArgs e)
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
