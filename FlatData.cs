using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using aiCorporation.Models;

namespace aiCorporation.NewImproved
{
    public class SalesAgentFileRecord
    {
        private string m_szSalesAgentName;
        private string m_szSalesAgentEmailAddress;
        private string m_szClientName;
        private string m_szClientIdentifier;
        private string m_szBankName;
        private string m_szAccountNumber;
        private string m_szSortCode;
        private string m_szCurrency;

        public string SalesAgentName { get { return (m_szSalesAgentName); } }
        public string SalesAgentEmailAddress { get { return (m_szSalesAgentEmailAddress); } }
        public string ClientName { get { return (m_szClientName); } }
        public string ClientIdentifier { get { return (m_szClientIdentifier); } }
        public string BankName { get { return (m_szBankName); } }
        public string AccountNumber { get { return (m_szAccountNumber); } }
        public string SortCode { get { return (m_szSortCode); } }
        public string Currency { get { return (m_szCurrency); } }

        public static string CsvHeader()
        {
            StringBuilder sbString;

            sbString = new StringBuilder();

            sbString.Append("SalesAgentName,SalesAgentEmailAddress,ClientName,ClientIdentifier,BankName,AccountNumber,SortCode,Currency\r\n");

            return (sbString.ToString());
        }

        public string ToCsvRecord()
        {
            StringBuilder sbString;

            sbString = new StringBuilder();

            sbString.AppendFormat("\"{0}\"", m_szSalesAgentName);
            sbString.AppendFormat(",\"{0}\"", m_szSalesAgentEmailAddress);
            sbString.AppendFormat(",\"{0}\"", m_szClientName);
            sbString.AppendFormat(",\"{0}\"", m_szClientIdentifier);
            sbString.AppendFormat(",\"{0}\"", m_szBankName);
            sbString.AppendFormat(",\"{0}\"", m_szAccountNumber);
            sbString.AppendFormat(",\"{0}\"", m_szSortCode);
            sbString.AppendFormat(",\"{0}\"", m_szCurrency);

            sbString.Append("\r\n");

            return (sbString.ToString());
        }

        public SalesAgentFileRecord(SalesAgentFileRecordModel model)
        {
            m_szSalesAgentName = model.szSalesAgentName;
            m_szSalesAgentEmailAddress = model.szSalesAgentEmailAddress;
            m_szClientName = model.szClientName;
            m_szClientIdentifier = model.szClientIdentifier;
            m_szBankName = model.szBankName;
            m_szAccountNumber = model.szAccountNumber;
            m_szSortCode = model.szSortCode;
            m_szCurrency = model.szCurrency;
        }
    }
    public class SalesAgentFileRecordList
    {
        private List<SalesAgentFileRecord> m_lsafrSalesAgentFileRecordList;

        public int Count { get { return (m_lsafrSalesAgentFileRecordList.Count); } }

        public SalesAgentFileRecord this[int nIndex]
        {
            get
            {
                if (nIndex < 0 ||
                    nIndex >= m_lsafrSalesAgentFileRecordList.Count)
                {
                    throw new Exception("Array index out of bounds");
                }
                return (m_lsafrSalesAgentFileRecordList[nIndex]);
            }
        }

        public SalesAgentFileRecord this[string szSalesAgentEmailAddress]
        {
            get
            {
                int nCount = 0;
                bool boFound = false;
                SalesAgentFileRecord safrSalesAgentFileRecord = null;

                while (!boFound &&
                       nCount < m_lsafrSalesAgentFileRecordList.Count)
                {
                    if (m_lsafrSalesAgentFileRecordList[nCount].SalesAgentEmailAddress == szSalesAgentEmailAddress)
                    {
                        boFound = true;
                        safrSalesAgentFileRecord = m_lsafrSalesAgentFileRecordList[nCount];
                    }
                    nCount++;
                }
                return (safrSalesAgentFileRecord);
            }
        }

        public string ToCsvString()
        {
            StringBuilder sbCsvString;
            int nCount;

            sbCsvString = new StringBuilder();

            sbCsvString.Append(SalesAgentFileRecord.CsvHeader());

            for (nCount = 0; nCount < m_lsafrSalesAgentFileRecordList.Count; nCount++)
            {
                sbCsvString.AppendFormat("{0}", m_lsafrSalesAgentFileRecordList[nCount].ToCsvRecord());
            }

            return (sbCsvString.ToString());
        }

        public static SalesAgentFileRecordList FromCsvStream(Stream sStream)
        {
            StreamReader srReader;
            CsvReader crReader;
            SalesAgentFileRecord safrSalesAgentFileRecord;
            List<SalesAgentFileRecord> lsafrSalesAgentFileRecordList;
            SalesAgentFileRecordList safrlSalesAgentFileRecordList;
            int nCount;

            lsafrSalesAgentFileRecordList = new List<SalesAgentFileRecord>();

            if (sStream != null)
            {
                nCount = 0;

                srReader = new StreamReader(sStream);
                crReader = new CsvReader(srReader);

                while (crReader.Read())
                {
                    // don't read in the first row as it's the header data
                    if (nCount > 0)
                    {
                        SalesAgentFileRecordModel salesAgentFileRecord = new SalesAgentFileRecordModel()
                        {
                            szSalesAgentName = crReader.GetField<string>(0),
                            szSalesAgentEmailAddress = crReader.GetField<string>(1),
                            szClientName = crReader.GetField<string>(2),
                            szClientIdentifier = crReader.GetField<string>(3),
                            szBankName = crReader.GetField<string>(4),
                            szAccountNumber = crReader.GetField<string>(5),
                            szSortCode = crReader.GetField<string>(6),
                            szCurrency = crReader.GetField<string>(7)
                        };

                        safrSalesAgentFileRecord = new SalesAgentFileRecord(salesAgentFileRecord);
                        lsafrSalesAgentFileRecordList.Add(safrSalesAgentFileRecord);
                    }
                    nCount++;
                }
            }
            safrlSalesAgentFileRecordList = new SalesAgentFileRecordList(lsafrSalesAgentFileRecordList);

            return (safrlSalesAgentFileRecordList);
        }

        /************************************************************/
        /* THIS IS THE FUNCTION THAT WE WOULD LIKE YOU TO IMPLEMENT */
        /************************************************************/
        public SalesAgentList ToSalesAgentList()
        {
            SalesAgentListBuilder salSalesAgentList;      

            salSalesAgentList = new SalesAgentListBuilder();

            int nCount;
            for (nCount = 0; nCount < m_lsafrSalesAgentFileRecordList.Count; nCount++)
            {
                // if we write m_lsafrSalesAgentFileRecordList[nCount] again and again then this will more traverse in array so first instance is created with data, later we access it
                var thissafrSalesAgentFileRecord = m_lsafrSalesAgentFileRecordList[nCount];

                SalesAgentBuilder saCurrentSalesAgent;
                saCurrentSalesAgent = salSalesAgentList[thissafrSalesAgentFileRecord.SalesAgentEmailAddress];

                if (saCurrentSalesAgent == null)
                {
                    saCurrentSalesAgent = new SalesAgentBuilder()
                    {
                        SalesAgentEmailAddress = thissafrSalesAgentFileRecord.SalesAgentEmailAddress,
                        SalesAgentName = thissafrSalesAgentFileRecord.SalesAgentName
                    };
                    ClientBuilder cClient;
                    cClient = saCurrentSalesAgent.ClientList[thissafrSalesAgentFileRecord.ClientIdentifier];

                    if (cClient == null)
                    {
                        cClient = new ClientBuilder()
                        {
                            ClientIdentifier = thissafrSalesAgentFileRecord.ClientIdentifier,
                            ClientName = thissafrSalesAgentFileRecord.ClientName,
                        };
                        BankAccountBuilder baBankAccount;
                        baBankAccount = cClient.BankAccountList.GetBankAccount(thissafrSalesAgentFileRecord.BankName, thissafrSalesAgentFileRecord.AccountNumber, thissafrSalesAgentFileRecord.SortCode);

                        if (baBankAccount == null)
                        {
                            baBankAccount = new BankAccountBuilder()
                            {
                                BankName = thissafrSalesAgentFileRecord.BankName,
                                AccountNumber = thissafrSalesAgentFileRecord.AccountNumber,
                                SortCode = thissafrSalesAgentFileRecord.SortCode
                            };

                            cClient.BankAccountList.Add(baBankAccount);
                        }

                        baBankAccount.Currency = thissafrSalesAgentFileRecord.Currency;

                        saCurrentSalesAgent.ClientList.Add(cClient);
                    }
                    salSalesAgentList.Add(saCurrentSalesAgent);
                }
            }

            SalesAgentList salReturnSalesAgentList;
            salReturnSalesAgentList = new SalesAgentList(salSalesAgentList.GetListOfSalesAgentObjects());

            return (salReturnSalesAgentList);
        }

        public SalesAgentFileRecordList(List<SalesAgentFileRecord> lsafrSalesAgentFileRecordList)
        {
            int nCount = 0;

            m_lsafrSalesAgentFileRecordList = new List<SalesAgentFileRecord>();

            if (lsafrSalesAgentFileRecordList != null)
            {
                for (nCount = 0; nCount < lsafrSalesAgentFileRecordList.Count; nCount++)
                {
                    m_lsafrSalesAgentFileRecordList.Add(lsafrSalesAgentFileRecordList[nCount]);
                }
            }
        }

        public List<SalesAgentFileRecord> GetListOfSalesAgentFileRecordObjects()
        {
            List<SalesAgentFileRecord> lsafrSalesAgentFileRecordList = null;
            int nCount = 0;

            lsafrSalesAgentFileRecordList = new List<SalesAgentFileRecord>();

            for (nCount = 0; nCount < m_lsafrSalesAgentFileRecordList.Count; nCount++)
            {
                lsafrSalesAgentFileRecordList.Add(m_lsafrSalesAgentFileRecordList[nCount]);
            }

            return (lsafrSalesAgentFileRecordList);
        }
    }
}