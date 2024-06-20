using System;
using System.Linq;
using System.Windows.Forms;

namespace Pogranichnayia_Slugba
{
    internal class DeleteClass
    {
        public static void CloseAllForms()
        {
            var openForms = Application.OpenForms.Cast<Form>().ToList();
            foreach (var form in openForms)
            {
                if (form.Name != "Login") // Пропустить форму Login
                {
                    form.Close();
                }
            }
        }
    }
}
