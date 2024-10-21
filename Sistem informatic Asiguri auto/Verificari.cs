using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Sistem_informatic_Asiguri_auto
{
    public class Verificari
    {
        public static bool checkName(string sirCaractere)
        {
            foreach(char cuv in sirCaractere)
            {
                if(!(char.IsLetter(cuv) || cuv==',' || cuv=='-' || char.IsWhiteSpace(cuv)))
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }
            return true;
        }
        

        public static bool checkCnp(string sirCaractere)
        {
            foreach(char cuv in sirCaractere)
            {
                if(!char.IsDigit(cuv))
                {
                    return false;
                } 
            }
            return true;
        }

        public static bool checkEmail(string sirCaractere)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            Regex regex = new Regex(pattern);
            return regex.IsMatch(sirCaractere);

        }

        public static bool checkRoleEmployee(string sirCaractere)
        {
            if(sirCaractere.ToUpper()=="ANGAJAT")            
                return true;
            return false;
        }

        public static bool checkTipAsigurare(string sirCaractere)
        {
            if (sirCaractere.ToUpper() == "RCA" || sirCaractere.ToUpper() == "CASCO")
                return true;
            return false;
        }

        public static bool IntervalVarsta(string Min,string Max)
        {
            int min = Convert.ToInt32(Min);
            int max = Convert.ToInt32(Max);
            if(min<18 || max>80)
            {
                return false;
            }
            return true;
        }

        public static bool IntervalBonusMalus(int procent)
        {
            if (procent < 0  || procent > 100)
                return false;
            return true;
        }

        public static void Listbox(ListBox list)
        {
            if (list.Items.Count == 0)
            {

            }
            else
            {
                list.SetSelected(0, false);
            }
        }
    }
}
