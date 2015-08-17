using QualEpisodioCore.BLL;
using QualEpisodioCore.Common;
using QualEpisodioCore.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualEpisodioCore
{
    public class InicializacaoDev
    {
        public static void Inicializa()
        {
            if (File.Exists(@"Data\qe.db"))
                File.Delete(@"Data\qe.db");

            List<SerieModel> series = new List<SerieModel>();

            series.Add(new SerieModel("Criminal Minds", 8, 24, 8, 24, ""));
            series.Add(new SerieModel("Elementary", 1, 24, 1, 24, ""));
            series.Add(new SerieModel("Game of Thrones", 3, 8, 3, 5, ""));
            series.Add(new SerieModel("How I Met Your Mother", 8, 24, 8, 24, ""));
            series.Add(new SerieModel("CSI Miami"));
            series.Add(new SerieModel("CSI New York"));

            new SerieBLL().InsereSeries(series);

            new BibliotecaBLL().InsereBiblioteca(new BibliotecaModel(@"C:\Users\Rovian\Documents\Shared"));
        }
    }
}
