using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DataObjects;

namespace DataAccessLayer
{
    public class BankAccessor
    {
        public bool SaveBank(int amount)
        {
            bool result = false;
            string dataPath = AppData.DataPath + "\\" + "bank.txt";
            FileStream bankFile = null;
            StreamWriter writer = null;

            // unsafe code
            try
            {
                bankFile = new FileStream(dataPath, FileMode.Create,
                    FileAccess.Write);
                writer = new StreamWriter(bankFile);
                string line = amount.ToString();
                writer.WriteLine(line);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                writer.Close();
            }
            return result;
        }

        public int RestoreBank()
        {
 
            int bank = 500;
            string dataPath = AppData.DataPath + "\\" + "bank.txt";
            FileStream bankFile = null;
            StreamReader reader = null;
            try
            {
                bankFile = new FileStream(dataPath, FileMode.Open,
                    FileAccess.Read);
                reader = new StreamReader(bankFile);
                string line = reader.ReadLine();
                line = line.Trim();
                bank = int.Parse(line);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
            return bank;
        }
    }
}
