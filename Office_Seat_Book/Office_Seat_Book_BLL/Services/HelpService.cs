using Office_Seat_Book_DLL.Repost;
using Office_Seat_Book_Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Office_Seat_Book_BLL.Services
{
    public class HelpService
    {
        IHelpRepost _helpRepost;
        public HelpService(IHelpRepost helpRepost)
        {
            _helpRepost = helpRepost;
        }

        //Add Help
        public void AddHelp(Help help)
        {
            _helpRepost.AddHelp(help);
        }

        //Delete Help

        public void DeleteHelp(int helpID)
        {
            _helpRepost.DeleteHelp(helpID);
        }

        //Update Help

        public void UpdateHelp(Help felp)
        {
            _helpRepost.UpdateHelp(felp);
        }

        //Get getHelps

        public IEnumerable<Help> GetHelp()
        {
            return _helpRepost.GetHelps();
        }
        public Help GetByHelpId(int EmployeeID)
        {
            return _helpRepost.GetHelpById(EmployeeID);
        }
    }
}
