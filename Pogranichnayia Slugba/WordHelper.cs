using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.IO;

namespace Pogranichnayia_Slugba
{
    internal class WordHelper
    {
        private FileInfo _fileinfo;

        public WordHelper(string fileName)
        {
            if (File.Exists(fileName))
            {
                _fileinfo = new FileInfo(fileName);
            }
            else
            {
                throw new ArgumentException("Файл не найден");
            }
        }

        internal bool Procсess(Dictionary<string, string> items, out string newFileName) // Изменение сигнатуры метода
        {
            Word.Application app = null;
            newFileName = null; // Инициализация выходного параметра

            try
            {
                app = new Word.Application();
                Object file = _fileinfo.FullName;
                Object missing = Type.Missing;
                app.Documents.Open(ref file, ref missing, ref missing, ref missing);

                foreach (var item in items)
                {
                    Word.Find find = app.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;

                    Object wrap = Word.WdFindWrap.wdFindContinue;
                    Object replace = Word.WdReplace.wdReplaceAll;

                    find.Execute(FindText: Type.Missing,
                                MatchCase: false,
                                MatchWholeWord: false,
                                MatchWildcards: false,
                                MatchSoundsLike: missing,
                                MatchAllWordForms: false,
                                Forward: true,
                                Wrap: wrap,
                                Format: false,
                                ReplaceWith: missing, Replace: replace);
                }

                newFileName = Path.Combine(_fileinfo.DirectoryName, DateTime.Now.ToString("yyyyMMdd HHmmss") + " " + _fileinfo.Name);
                app.ActiveDocument.SaveAs2(newFileName);
                app.ActiveDocument.Close();

                // Открытие документа после сохранения
                app.Documents.Open(newFileName);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (app != null)
                {
                    app.Quit();
                }
            }
            return false;
        }
    }
}
