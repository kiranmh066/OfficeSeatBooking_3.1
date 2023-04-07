using Office_Seat_Book_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Office_Seat_Book_DLL.Repost
{
    public class HelpRepost:IHelpRepost
    {
        Office_DB_Context _dbContext;//default private

        public HelpRepost(Office_DB_Context dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddHelp(Help help)
        {
            _dbContext.help.Add(help);
            _dbContext.SaveChanges();
        }

        public void DeleteHelp(int helpId)
        {
            var help = _dbContext.help.Find(helpId);
            _dbContext.help.Remove(help);
            _dbContext.SaveChanges();
        }

        public Help GetHelpById(int helpId)
        {
            return _dbContext.help.Find(helpId);
        }

        public IEnumerable<Help> GetHelps()
        {
            return _dbContext.help.ToList();
        }


        public void UpdateHelp(Help help)
        {

            _dbContext.Entry(help).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
