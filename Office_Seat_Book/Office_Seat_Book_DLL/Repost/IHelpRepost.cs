using Office_Seat_Book_Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Office_Seat_Book_DLL.Repost
{
    public interface IHelpRepost
    {
        void AddHelp(Help help);
        void UpdateHelp(Help help);

        void DeleteHelp(int helpId);

        Help GetHelpById(int helpId);

        IEnumerable<Help> GetHelps();
    }
}
