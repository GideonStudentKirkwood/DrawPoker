using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;


namespace LogicLayer
{
    public class Banker
    {
        private BankAccessor _bankAccessor = new BankAccessor();

        public bool SavePlayerBank(int bank)
        {
            bool result = false; // change to true if successful

            try
            {
                result = _bankAccessor.SaveBank(bank);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem saving your game.", ex);
            }

            return result;
        }

        public int RestoreSavedBank()
        {
            int amount = 500; // what to use if restore fails

            // invoke BankAccessor
            try
            {
                amount = _bankAccessor.RestoreBank();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem restoring your game.", ex);
            }

            return amount;
        }
    }
}
