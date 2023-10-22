using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aiCorporation.NewImproved
{
    public class SalesAgent
    {
        private string m_szSalesAgentName;
        private string m_szSalesAgentEmailAddress;
        private ClientList m_clClientList;

        public string SalesAgentName { get { return (m_szSalesAgentName); } }
        public string SalesAgentEmailAddress { get { return (m_szSalesAgentEmailAddress); } }
        public ClientList ClientList { get { return (m_clClientList); } }

        public static int Compare(SalesAgent saFirst, SalesAgent saSecond)
        {
            return (String.Compare(saFirst.SalesAgentEmailAddress, saSecond.SalesAgentEmailAddress));
        }

        public SalesAgent(string szSalesAgentName,
                          string szSalesAgentEmailAddress,
                          IEnumerable<Client> lcClientList)
        {
            m_szSalesAgentName = szSalesAgentName;
            m_szSalesAgentEmailAddress = szSalesAgentEmailAddress;
            m_clClientList = new ClientList(lcClientList);
        }
    }
    public class SalesAgentList
    {
        private List<SalesAgent> m_lsaSalesAgentList;

        public int Count { get { return (m_lsaSalesAgentList.Count); } }

        public SalesAgent this[int nIndex]
        {
            get
            {
                if (nIndex < 0 ||
                    nIndex >= m_lsaSalesAgentList.Count)
                {
                    throw new Exception("Array index out of bounds");
                }
                return (m_lsaSalesAgentList[nIndex]);
            }
        }

        public SalesAgent this[string szSalesAgentEmailAddress]
        {
            get
            {
                return m_lsaSalesAgentList.Where(x => x.SalesAgentEmailAddress == szSalesAgentEmailAddress).FirstOrDefault();
            }
        }

        public SalesAgentFileRecordList ToSalesAgentFileRecordList()
        {
            List<SalesAgentFileRecord> lsafrSalesAgentFileRecordList;
            SalesAgentFileRecordList safrlSalesAgentFileRecordList;
            SalesAgentFileRecord safrSalesAgentFileRecord;

            lsafrSalesAgentFileRecordList = new List<SalesAgentFileRecord>();

            foreach(var salesAgentList in m_lsaSalesAgentList)
            {
                foreach ( var clientList in salesAgentList.ClientList.GetListOfClientObjects() )
                {
                    foreach ( var bankList in clientList.BankAccountList.GetListOfBankAccountObjects() )
                    {
                        safrSalesAgentFileRecord = new SalesAgentFileRecord(salesAgentList.SalesAgentName,
                                                                            salesAgentList.SalesAgentEmailAddress,
                                                                            clientList.ClientName,
                                                                            clientList.ClientIdentifier,
                                                                            bankList.BankName,
                                                                            bankList.AccountNumber,
                                                                            bankList.SortCode,
                                                                            bankList.Currency);
                        lsafrSalesAgentFileRecordList.Add(safrSalesAgentFileRecord);
                    }
                }
            }

            safrlSalesAgentFileRecordList = new SalesAgentFileRecordList(lsafrSalesAgentFileRecordList);

            return (safrlSalesAgentFileRecordList);
        }

        public void Sort()
        {
            int nCount;

            m_lsaSalesAgentList.Sort(SalesAgent.Compare);

            for (nCount = 0; nCount < m_lsaSalesAgentList.Count; nCount++)
            {
                m_lsaSalesAgentList[nCount].ClientList.Sort();
            }
        }

        public SalesAgentList(IEnumerable<SalesAgent> lsaSalesAgentList)
        {
            m_lsaSalesAgentList = lsaSalesAgentList.ToList();
        }

        public List<SalesAgent> GetListOfSalesAgentObjects()
        {
            return m_lsaSalesAgentList;
        }
    }

    public class Client
    {
        private string m_szClientName;
        private string m_szClientIdentifier;
        private BankAccountList m_balBankAccountList;

        public string ClientName { get { return (m_szClientName); } }
        public string ClientIdentifier { get { return (m_szClientIdentifier); } }
        public BankAccountList BankAccountList { get { return (m_balBankAccountList); } }

        public static int Compare(Client cFirst, Client cSecond)
        {
            return (String.Compare(cFirst.ClientIdentifier, cSecond.ClientIdentifier));
        }

        public Client(string szClientName,
                      string szClientIdentifier,
                      IEnumerable<BankAccount> lbaBankAccountList)
        {
            m_szClientName = szClientName;
            m_szClientIdentifier = szClientIdentifier;
            m_balBankAccountList = new BankAccountList(lbaBankAccountList);
        }
    }
    public class ClientList
    {
        private List<Client> m_lcClientList;

        public int Count { get { return (m_lcClientList.Count); } }

        public Client this[int nIndex]
        {
            get
            {
                if (nIndex < 0 ||
                    nIndex >= m_lcClientList.Count)
                {
                    throw new Exception("Array index out of bounds");
                }
                return (m_lcClientList[nIndex]);
            }
        }

        public Client this[string szClientIdentifier]
        {
            get
            {
                return m_lcClientList.Where(x => x.ClientIdentifier == szClientIdentifier).FirstOrDefault();
            }
        }

        public void Sort()
        {
            int nCount;

            m_lcClientList.Sort(Client.Compare);

            for (nCount = 0; nCount < m_lcClientList.Count; nCount++)
            {
                m_lcClientList[nCount].BankAccountList.Sort();
            }
        }

        public ClientList(ClientListBuilder clClientList)
        {
            int nCount;

            for (nCount = 0; nCount < clClientList.Count; nCount++)
            {
                m_lcClientList.Add(clClientList[nCount].ToClient());
            }
        }

        public ClientList(IEnumerable<Client> lcClientList)
        {
            m_lcClientList = lcClientList.ToList();
        }

        public List<Client> GetListOfClientObjects()
        {
            List<Client> lcClientList = null;
            int nCount = 0;

            lcClientList = new List<Client>();

            for (nCount = 0; nCount < m_lcClientList.Count; nCount++)
            {
                lcClientList.Add(m_lcClientList[nCount]);
            }

            return (lcClientList);
        }
    }

    public class BankAccount
    {
        private string m_szBankName;
        private string m_szAccountNumber;
        private string m_szSortCode;
        private string m_szCurrency;

        public string BankName { get { return (m_szBankName); } }
        public string AccountNumber { get { return (m_szAccountNumber); } }
        public string SortCode { get { return (m_szSortCode); } }
        public string Currency { get { return (m_szCurrency); } }

        public static int Compare(BankAccount baFirst, BankAccount baSecond)
        {
            int nReturnValue;

            nReturnValue = String.Compare(baFirst.BankName, baSecond.BankName);

            // bank names equal?
            if (nReturnValue == 0)
            {
                // check the account numbers
                nReturnValue = String.Compare(baFirst.AccountNumber, baSecond.AccountNumber);

                // account numbers equal?
                if (nReturnValue == 0)
                {
                    // check the sort code
                    nReturnValue = String.Compare(baFirst.SortCode, baSecond.SortCode);
                }
            }

            return (nReturnValue);
        }

        public BankAccount(string szBankName,
                           string szAccountNumber,
                           string szSortCode,
                           string szCurrency)
        {
            m_szBankName = szBankName;
            m_szAccountNumber = szAccountNumber;
            m_szSortCode = szSortCode;
            m_szCurrency = szCurrency;
        }
    }
    public class BankAccountList
    {
        private List<BankAccount> m_lbaBankAccountList;

        public int Count { get { return (m_lbaBankAccountList.Count); } }

        public BankAccount this[int nIndex]
        {
            get
            {
                if (nIndex < 0 ||
                    nIndex >= m_lbaBankAccountList.Count)
                {
                    throw new Exception("Array index out of bounds");
                }
                return (m_lbaBankAccountList[nIndex]);
            }
        }

        public void Sort()
        {
            m_lbaBankAccountList.Sort(BankAccount.Compare);
        }

        public BankAccountList(IEnumerable<BankAccount> lbaBankAccountList)
        {
            m_lbaBankAccountList = lbaBankAccountList.ToList();
        }

        public IEnumerable<BankAccount> GetListOfBankAccountObjects()
        {
            return m_lbaBankAccountList;
        }
    }

    public class SalesAgentBuilder
    {
        private string m_szSalesAgentName;
        private string m_szSalesAgentEmailAddress;
        private ClientListBuilder m_clClientList;

        public string SalesAgentName
        {
            get { return (m_szSalesAgentName); }
            set { m_szSalesAgentName = value; }
        }
        public string SalesAgentEmailAddress
        {
            get { return (m_szSalesAgentEmailAddress); }
            set { m_szSalesAgentEmailAddress = value; }
        }
        public ClientListBuilder ClientList { get { return (m_clClientList); } }

        public SalesAgent ToSalesAgent()
        {
            return new SalesAgent(m_szSalesAgentName, m_szSalesAgentEmailAddress, m_clClientList.GetListOfClientObjects());
        }

        public SalesAgentBuilder()
        {
            m_clClientList = new ClientListBuilder();
        }
    }
    public class SalesAgentListBuilder
    {
        private List<SalesAgentBuilder> m_lsaSalesAgentList;

        public int Count { get { return (m_lsaSalesAgentList.Count); } }

        public SalesAgentBuilder this[int nIndex]
        {
            get
            {
                if (nIndex < 0 ||
                    nIndex >= m_lsaSalesAgentList.Count)
                {
                    throw new Exception("Array index out of bounds");
                }
                return (m_lsaSalesAgentList[nIndex]);
            }
        }

        public SalesAgentBuilder this[string szSalesAgentEmailAddress]
        {
            get
            {
                return m_lsaSalesAgentList.Where(x=>x.SalesAgentEmailAddress == szSalesAgentEmailAddress).FirstOrDefault();
            }
        }

        public void Add(SalesAgentBuilder saSalesAgent)
        {
            if (saSalesAgent != null)
            {
                m_lsaSalesAgentList.Add(saSalesAgent);
            }
        }

        public SalesAgentListBuilder()
        {
            m_lsaSalesAgentList = new List<SalesAgentBuilder>();
        }

        public IEnumerable<SalesAgent> GetListOfSalesAgentObjects()
        {
            return m_lsaSalesAgentList.Select(x=>x.ToSalesAgent());
        }
    }

    public class ClientBuilder
    {
        private string m_szClientName;
        private string m_szClientIdentifier;
        private BankAccountListBuilder m_balBankAccountList;

        public string ClientName
        {
            get { return (m_szClientName); }
            set { m_szClientName = value; }
        }
        public string ClientIdentifier
        {
            get { return (m_szClientIdentifier); }
            set { m_szClientIdentifier = value; }
        }
        public BankAccountListBuilder BankAccountList { get { return (m_balBankAccountList); } }

        public Client ToClient()
        {
            return new Client(m_szClientName, m_szClientIdentifier, m_balBankAccountList.GetListOfBankAccountObjects());
        }

        public ClientBuilder()
        {
            m_balBankAccountList = new BankAccountListBuilder();
        }
    }
    public class ClientListBuilder
    {
        private List<ClientBuilder> m_lcClientList;

        public int Count { get { return (m_lcClientList.Count); } }

        public ClientBuilder this[int nIndex]
        {
            get
            {
                if (nIndex < 0 ||
                    nIndex >= m_lcClientList.Count)
                {
                    throw new Exception("Array index out of bounds");
                }
                return (m_lcClientList[nIndex]);
            }
        }

        public ClientBuilder this[string szClientIdentifier]
        {
            get
            {
                return m_lcClientList.Where(x => x.ClientIdentifier == szClientIdentifier).FirstOrDefault();
            }
        }

        public void Add(ClientBuilder cClient)
        {
            if (cClient != null)
            {
                m_lcClientList.Add(cClient);
            }
        }

        public ClientListBuilder()
        {
            m_lcClientList = new List<ClientBuilder>();
        }

        public IEnumerable<Client> GetListOfClientObjects()
        {
            return m_lcClientList.Select(x=>x.ToClient());
        }
    }

    public class BankAccountBuilder
    {
        private string m_szBankName;
        private string m_szAccountNumber;
        private string m_szSortCode;
        private string m_szCurrency;

        public string BankName
        {
            get { return (m_szBankName); }
            set { m_szBankName = value; }
        }
        public string AccountNumber
        {
            get { return (m_szAccountNumber); }
            set { m_szAccountNumber = value; }
        }
        public string SortCode
        {
            get { return (m_szSortCode); }
            set { m_szSortCode = value; }
        }
        public string Currency
        {
            get { return (m_szCurrency); }
            set { m_szCurrency = value; }
        }

        public BankAccount ToBankAccount()
        {
            return new BankAccount(m_szBankName, m_szAccountNumber, m_szSortCode, m_szCurrency);
        }
    }
    public class BankAccountListBuilder
    {
        private List<BankAccountBuilder> m_lbaBankAccountList;

        public int Count { get { return (m_lbaBankAccountList.Count); } }

        public BankAccountBuilder this[int nIndex]
        {
            get
            {
                if (nIndex < 0 ||
                    nIndex >= m_lbaBankAccountList.Count)
                {
                    throw new Exception("Array index out of bounds");
                }
                return (m_lbaBankAccountList[nIndex]);
            }
        }

        public BankAccountBuilder GetBankAccount(string szBankName, string szAccountNumber, string szSortCode)
        {
            return m_lbaBankAccountList.Where(x => x.BankName == szBankName &&
                    x.AccountNumber == szAccountNumber &&
                    x.SortCode == szSortCode).FirstOrDefault();
        }

        public void Add(BankAccountBuilder baBankAccount)
        {
            m_lbaBankAccountList.Add(baBankAccount);
        }

        public BankAccountListBuilder()
        {
            m_lbaBankAccountList = new List<BankAccountBuilder>();
        }

        public IEnumerable<BankAccount> GetListOfBankAccountObjects()
        {
            return m_lbaBankAccountList.Select(x => x.ToBankAccount());
        }
    }
}