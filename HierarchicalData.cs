﻿using aiCorporation.Models;
using System;
using System.Collections.Generic;

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
            return String.Compare(saFirst.SalesAgentEmailAddress, saSecond.SalesAgentEmailAddress);
        }

        public SalesAgent(string szSalesAgentName,
                          string szSalesAgentEmailAddress,
                          List<Client> lcClientList)
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
                int nCount = 0;
                bool boFound = false;
                SalesAgent saSalesAgent = null;

                while (!boFound &&
                       nCount < m_lsaSalesAgentList.Count)
                {
                    if (m_lsaSalesAgentList[nCount].SalesAgentEmailAddress == szSalesAgentEmailAddress)
                    {
                        boFound = true;
                        saSalesAgent = m_lsaSalesAgentList[nCount];
                    }
                    nCount++;
                }
                return (saSalesAgent);
            }
        }

        public SalesAgentFileRecordList ToSalesAgentFileRecordList()
        {
            List<SalesAgentFileRecord> lsafrSalesAgentFileRecordList;
            SalesAgentFileRecordList safrlSalesAgentFileRecordList;
            SalesAgentFileRecord safrSalesAgentFileRecord;
            int nSalesAgentCount;
            int nClientCount;
            int nBankAccountCount;

            lsafrSalesAgentFileRecordList = new List<SalesAgentFileRecord>();

            for (nSalesAgentCount = 0; nSalesAgentCount < m_lsaSalesAgentList.Count; nSalesAgentCount++)
            {
                var this_m_lsaSalesAgent = m_lsaSalesAgentList[nSalesAgentCount];
                for (nClientCount = 0; nClientCount < this_m_lsaSalesAgent.ClientList.Count; nClientCount++)
                {
                    var this_m_lsaSalesAgentClientAccount = this_m_lsaSalesAgent.ClientList[nClientCount];
                    for (nBankAccountCount = 0; nBankAccountCount < this_m_lsaSalesAgent.ClientList[nClientCount].BankAccountList.Count; nBankAccountCount++)
                    {

                        SalesAgentFileRecordModel salesAgentFileRecord = new SalesAgentFileRecordModel()
                        {
                            szSalesAgentName = this_m_lsaSalesAgent.SalesAgentName,
                            szSalesAgentEmailAddress = this_m_lsaSalesAgent.SalesAgentEmailAddress,
                            szClientName = this_m_lsaSalesAgentClientAccount.ClientName,
                            szClientIdentifier = this_m_lsaSalesAgentClientAccount.ClientIdentifier,
                            szBankName = this_m_lsaSalesAgentClientAccount.BankAccountList[nBankAccountCount].BankName,
                            szAccountNumber = this_m_lsaSalesAgentClientAccount.BankAccountList[nBankAccountCount].AccountNumber,
                            szSortCode = this_m_lsaSalesAgentClientAccount.BankAccountList[nBankAccountCount].SortCode,
                            szCurrency = this_m_lsaSalesAgentClientAccount.BankAccountList[nBankAccountCount].Currency
                        };
                        safrSalesAgentFileRecord = new SalesAgentFileRecord(salesAgentFileRecord);
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

        public SalesAgentList(List<SalesAgent> lsaSalesAgentList)
        {
            int nCount = 0;

            m_lsaSalesAgentList = new List<SalesAgent>();

            for (nCount = 0; nCount < lsaSalesAgentList.Count; nCount++)
            {
                m_lsaSalesAgentList.Add(lsaSalesAgentList[nCount]);
            }
        }

        public List<SalesAgent> GetListOfSalesAgentObjects()
        {
            List<SalesAgent> lsaSalesAgentList = null;
            int nCount = 0;

            lsaSalesAgentList = new List<SalesAgent>();

            for (nCount = 0; nCount < m_lsaSalesAgentList.Count; nCount++)
            {
                lsaSalesAgentList.Add(m_lsaSalesAgentList[nCount]);
            }

            return (lsaSalesAgentList);
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
                      List<BankAccount> lbaBankAccountList)
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
                int nCount = 0;
                bool boFound = false;
                Client cClient = null;

                while (!boFound &&
                       nCount < m_lcClientList.Count)
                {
                    if (m_lcClientList[nCount].ClientIdentifier == szClientIdentifier)
                    {
                        boFound = true;
                        cClient = m_lcClientList[nCount];
                    }
                    nCount++;
                }
                return (cClient);
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

            m_lcClientList = new List<Client>();

            for (nCount = 0; nCount < clClientList.Count; nCount++)
            {
                m_lcClientList.Add(clClientList[nCount].ToClient());
            }
        }

        public ClientList(List<Client> lcClientList)
        {
            int nCount = 0;

            m_lcClientList = new List<Client>();

            for (nCount = 0; nCount < lcClientList.Count; nCount++)
            {
                m_lcClientList.Add(lcClientList[nCount]);
            }
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

        public BankAccountList(List<BankAccount> lbaBankAccountList)
        {
            int nCount = 0;

            m_lbaBankAccountList = new List<BankAccount>();

            for (nCount = 0; nCount < lbaBankAccountList.Count; nCount++)
            {
                m_lbaBankAccountList.Add(lbaBankAccountList[nCount]);
            }
        }

        public List<BankAccount> GetListOfBankAccountObjects()
        {
            List<BankAccount> lbaBankAccountList = null;
            int nCount = 0;

            lbaBankAccountList = new List<BankAccount>();

            for (nCount = 0; nCount < m_lbaBankAccountList.Count; nCount++)
            {
                lbaBankAccountList.Add(m_lbaBankAccountList[nCount]);
            }

            return (lbaBankAccountList);
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
            SalesAgent saSalesAgent;

            saSalesAgent = new SalesAgent(m_szSalesAgentName, m_szSalesAgentEmailAddress, m_clClientList.GetListOfClientObjects());

            return (saSalesAgent);
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
                int nCount = 0;
                bool boFound = false;
                SalesAgentBuilder saSalesAgent = null;

                while (!boFound &&
                       nCount < m_lsaSalesAgentList.Count)
                {
                    if (m_lsaSalesAgentList[nCount].SalesAgentEmailAddress == szSalesAgentEmailAddress)
                    {
                        boFound = true;
                        saSalesAgent = m_lsaSalesAgentList[nCount];
                    }
                    nCount++;
                }
                return (saSalesAgent);
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

        public List<SalesAgent> GetListOfSalesAgentObjects()
        {
            List<SalesAgent> lsaSalesAgentList = null;
            int nCount = 0;

            lsaSalesAgentList = new List<SalesAgent>();

            for (nCount = 0; nCount < m_lsaSalesAgentList.Count; nCount++)
            {
                lsaSalesAgentList.Add(m_lsaSalesAgentList[nCount].ToSalesAgent());
            }

            return (lsaSalesAgentList);
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
            Client cClient;

            cClient = new Client(m_szClientName, m_szClientIdentifier, m_balBankAccountList.GetListOfBankAccountObjects());

            return (cClient);
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
                int nCount = 0;
                bool boFound = false;
                ClientBuilder cClient = null;

                while (!boFound &&
                       nCount < m_lcClientList.Count)
                {
                    if (m_lcClientList[nCount].ClientIdentifier == szClientIdentifier)
                    {
                        boFound = true;
                        cClient = m_lcClientList[nCount];
                    }
                    nCount++;
                }
                return (cClient);
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

        public List<Client> GetListOfClientObjects()
        {
            List<Client> lcClientList = null;
            int nCount = 0;

            lcClientList = new List<Client>();

            for (nCount = 0; nCount < m_lcClientList.Count; nCount++)
            {
                lcClientList.Add(m_lcClientList[nCount].ToClient());
            }

            return (lcClientList);
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
            BankAccount baBankAccount;

            baBankAccount = new BankAccount(m_szBankName, m_szAccountNumber, m_szSortCode, m_szCurrency);

            return (baBankAccount);
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
            int nCount = 0;
            bool boFound = false;
            BankAccountBuilder baBankAccount = null;

            while (!boFound &&
                   nCount < m_lbaBankAccountList.Count)
            {
                if (m_lbaBankAccountList[nCount].BankName == szBankName &&
                    m_lbaBankAccountList[nCount].AccountNumber == szAccountNumber &&
                    m_lbaBankAccountList[nCount].SortCode == szSortCode)
                {
                    boFound = true;
                    baBankAccount = m_lbaBankAccountList[nCount];
                }
                nCount++;
            }
            return (baBankAccount);
        }

        public void Add(BankAccountBuilder baBankAccount)
        {
            if (baBankAccount != null)
            {
                m_lbaBankAccountList.Add(baBankAccount);
            }
        }

        public BankAccountListBuilder()
        {
            m_lbaBankAccountList = new List<BankAccountBuilder>();
        }

        public List<BankAccount> GetListOfBankAccountObjects()
        {
            List<BankAccount> lbaBankAccountList = null;
            int nCount = 0;

            lbaBankAccountList = new List<BankAccount>();

            for (nCount = 0; nCount < m_lbaBankAccountList.Count; nCount++)
            {
                lbaBankAccountList.Add(m_lbaBankAccountList[nCount].ToBankAccount());
            }

            return (lbaBankAccountList);
        }
    }
}