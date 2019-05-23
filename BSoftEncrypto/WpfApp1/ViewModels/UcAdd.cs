using System.Windows.Controls;

namespace WpfApp1.Class
{
    class UcAdd
    {
        /// <summary>
        /// thêm các ưu user control vao main UI
        /// </summary>
        /// <param name="grd"></param>
        /// <param name="uc"></param>
        public static void AddUc(Grid grd, System.Windows.Controls.UserControl uc)
        {
            if (grd.Children.Count > 0)
            {
                grd.Children.Clear();
                grd.Children.Add(uc);
            }
            else
            {
                grd.Children.Add(uc);
            }
        }
    }
}
