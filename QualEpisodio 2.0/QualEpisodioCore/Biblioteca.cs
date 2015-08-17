using QualEpisodioCore.BLL;
using QualEpisodioCore.DAL;
using QualEpisodioCore.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QualEpisodioCore
{
    public class Biblioteca
    {
        private List<EpisodioModel> episodios;
        public List<EpisodioModel> ListEpisodiosEmDisco(SerieModel _serie)
        {
            BibliotecaBLL udtBibliotecaBLL = new BibliotecaBLL();
            List<BibliotecaModel> folders = udtBibliotecaBLL.ListaBibliotecas();
            episodios = new List<EpisodioModel>();

            videos = new List<FileInfo>();

            foreach (BibliotecaModel folder in folders)
                BuscaRecursivaNaPasta(folder.Diretorio);

            MontaListEpisodios(_serie.Nome);
            episodios = episodios.OrderByDescending(x => x.Temporada).ThenByDescending(x => x.Episodio).ToList();
            return episodios;
        }

        private void MontaListEpisodios(string serie)
        {
            List<string> nome = serie.Split(' ').ToList();

            nome.RemoveAll(x => x.Length < 3);

            foreach (FileInfo arquivo in videos)
            {
                if (NomeBate(arquivo.Name, nome))
                {
                    // Split on one or more non-digit characters.
                    string value = "";
                    //99X99
                    if (Regex.Match(arquivo.Name, @"[^0-9][0-9][0-9][^0-9][0-9][0-9][^0-9]").Success)
                    {
                        value = Regex.Match(arquivo.Name, @"[^0-9][0-9][0-9][^0-9][0-9][0-9][^0-9]").Value.ToString();
                        value = value.Remove(0, 1);
                        value = value.Remove(value.Length - 1, 1);
                        EpisodioModel em = new EpisodioModel(serie, Int32.Parse(value.Substring(0, 2)), Int32.Parse(value.Substring(3, 2)));
                        em.CaminhoVideo = arquivo.FullName;
                        episodios.Add(em);
                    }
                    //9X99
                    else if (Regex.Match(arquivo.Name, @"[^0-9][0-9][^0-9][0-9][0-9][^0-9]").Success)
                    {
                        value = Regex.Match(arquivo.Name, @"[^0-9][0-9][^0-9][0-9][0-9][^0-9]").Value.ToString();
                        value = value.Remove(0, 1);
                        value = value.Remove(value.Length - 1, 1);
                        EpisodioModel em = new EpisodioModel(serie, Int32.Parse(value.Substring(0, 1)), Int32.Parse(value.Substring(2, 2)));
                        em.CaminhoVideo = arquivo.FullName;
                        episodios.Add(em);
                    }
                    //99X9
                    else if (Regex.Match(arquivo.Name, @"[^0-9][0-9][0-9][^0-9][0-9][^0-9]").Success)
                    {
                        value = Regex.Match(arquivo.Name, @"[^0-9][0-9][0-9][^0-9][0-9][^0-9]").Value.ToString();
                        value = value.Remove(0, 1);
                        value = value.Remove(value.Length - 1, 1);
                        EpisodioModel em = new EpisodioModel(serie, Int32.Parse(value.Substring(0, 2)), Int32.Parse(value.Substring(3, 1)));
                        em.CaminhoVideo = arquivo.FullName;
                        episodios.Add(em);
                    }
                    //9X9
                    else if (Regex.Match(arquivo.Name, @"[^0-9][0-9][0-9][^0-9][0-9][0-9][^0-9]").Success)
                    {
                        value = Regex.Match(arquivo.Name, @"[^0-9][0-9][0-9][^0-9][0-9][0-9][^0-9]").Value.ToString();
                        EpisodioModel em = new EpisodioModel(serie, Int32.Parse(value.Substring(0, 1)), Int32.Parse(value.Substring(1, 1)));
                        em.CaminhoVideo = arquivo.FullName;
                        episodios.Add(em);
                    }
                    else
                    {
                        string[] numbers = Regex.Split(arquivo.Name, @"\D+");
                        numbers = numbers.Where(x => x.Length < 3 && !x.Equals("")).ToArray();
                        if (numbers.Length > 1)
                        {
                            try
                            {
                                EpisodioModel em = new EpisodioModel(serie, Int32.Parse(numbers[0]), Int32.Parse(numbers[1]));
                                em.CaminhoVideo = arquivo.FullName;
                                episodios.Add(em);
                            }
                            catch { }
                        }
                    }
                }
            }
        }

        List<FileInfo> videos;

        private void BuscaRecursivaNaPasta(string sDir)
        {

            DirectoryInfo pasta = new DirectoryInfo(sDir);

            foreach (DirectoryInfo d in pasta.GetDirectories())
            {
                foreach (FileInfo f in GetFiles(d))
                {
                    videos.Add(f);
                }
                BuscaRecursivaNaPasta(d.FullName);
            }

        }

        private FileInfo[] GetFiles(DirectoryInfo dir)
        {

            ExtensaoBLL extBLL = new ExtensaoBLL();

            // ArrayList will hold all file names
            List<FileInfo> files = new List<FileInfo>();

            // for each filter find mathing file names
            foreach (ExtensaoModel FileFilter in extBLL.ListaExtensoes())
            {
                // add found file names to array list
                files.AddRange(dir.GetFiles(string.Format("*.{0}", FileFilter)));
            }

            // returns string array of relevant file names
            return files.ToArray();
        }

        private bool NomeBate(string nome, List<string> nomes)
        {
            bool bate = true;
            foreach (string s in nomes)
            {
                if (!nome.Contains(s))
                {
                    bate = false;
                    break;
                }
            }

            return bate;
        }

    }
}
