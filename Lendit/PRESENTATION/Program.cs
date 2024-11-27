using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRESENTATION
{
    internal class Program
    {

   
      
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Login principal = new Login();
           //Principal principal = new Principal();
            Application.Run(principal);
        }
    }
}