using System;
using System.Collections.Generic;
using System.Text;

namespace GovernanceReminderMails
{
    public class SMTP
    {
        public string RITM { get; set; }
        public string Task { get; set; }
        public string InterfaceID { get; set; }
        public int NoOfInterfaces { get; set; }
        public string RequestSummary { get; set; }
        public DateTime AgreedRemediation { get; set; }
        public DateTime NextleadershipcallDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string EmailId { get; set; }
        public string InterfaceStatus { get; set; }
    }
}
