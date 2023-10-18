using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aiCorporation.Models
{
    public class SalesAgentFileRecordModel
    {
        public string szSalesAgentName { get; set; }
        public string szSalesAgentEmailAddress { get; set; }
        public string szClientName { get; set; }
        public string szClientIdentifier { get; set; }
        public string szBankName { get; set; }
        public string szAccountNumber { get; set; }
        public string szSortCode { get; set; }
        public string szCurrency { get; set; }
    }
}
