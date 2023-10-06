using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace aiCorporation
{
    public static class ListExtensions
    {
        /// <summary>
        /// Shuffles the element order of the specified list.
        /// </summary>
        public static List<T> Shuffle<T>(this List<T> lList)
        {
            int nListCount;
            int nLastIndex;
            int nCount;
            int nRandomIndex;
            T tTempItem;
            Random rRandom;
            List<T> lReturnList;

            nListCount = lList.Count;
            lReturnList = new List<T>(nListCount);
            lReturnList.AddRange(lList);

            nLastIndex = nListCount - 1;
            rRandom = new Random();

            for (nCount = 0; nCount < nLastIndex - 1; nCount++)
            {
                nRandomIndex = rRandom.Next(nCount + 1, nListCount);
                tTempItem = lReturnList[nCount];
                lReturnList[nCount] = lReturnList[nRandomIndex];
                lReturnList[nRandomIndex] = tTempItem;
            }

            return (lReturnList);
        }
    }

    public class TestValidator
    {
        private List<string> m_lszBankList;
        private List<string> m_lszCurrencyList;
        private Random m_rRandom;
        private int m_nMaxNumberOfSalesAgents;
        private int m_nMaxNumberOfClients;
        private int m_nMaxNumberOfBankAccounts;
        private int m_nMinNumberOfSalesAgents;
        private int m_nMinNumberOfClients;
        private int m_nMinNumberOfBankAccounts;

        public TestValidator(int nMinNumberOfSalesAgents,
                             int nMaxNumberOfSalesAgents,
                             int nMinNumberOfClients,
                             int nMaxNumberOfClients,
                             int nMinNumberOfBankAccounts,
                             int nMaxNumberOfBankAccounts)
        {
            m_nMinNumberOfBankAccounts = nMinNumberOfBankAccounts;
            m_nMaxNumberOfSalesAgents = nMaxNumberOfSalesAgents;
            m_nMinNumberOfClients = nMinNumberOfClients;
            m_nMaxNumberOfClients = nMaxNumberOfClients;
            m_nMinNumberOfSalesAgents = nMinNumberOfSalesAgents;
            m_nMaxNumberOfBankAccounts = nMaxNumberOfBankAccounts;

            m_rRandom = new Random();

            m_lszBankList = new List<string>();
            m_lszBankList.Add("Bank of Scotland plc");
            m_lszBankList.Add("Barclays Bank Plc");
            m_lszBankList.Add("Clydesdale Bank Plc");
            m_lszBankList.Add("HSBC Bank Plc");
            m_lszBankList.Add("Lloyds Bank Plc");
            m_lszBankList.Add("Metro Bank PLC");
            m_lszBankList.Add("Standard Chartered Bank");
            m_lszBankList.Add("Yorkshire Bank");

            m_lszCurrencyList = new List<string>();
            m_lszCurrencyList.Add("GBP");
            m_lszCurrencyList.Add("USD");
            m_lszCurrencyList.Add("EUR");
            m_lszCurrencyList.Add("AUD");
        }

        public string GenerateRandomFlatData(bool boRandomiseRecordOrder, out string szCsvFilename)
        {
            OriginalDoNotChange.SalesAgentFileRecordList safrlReturnSalesAgentFileRecordList;
            List<OriginalDoNotChange.SalesAgentFileRecord> lsafrSalesAgentFileRecordList;
            List<OriginalDoNotChange.SalesAgentFileRecord> lsafrReturnSalesAgentFileRecordList;
            OriginalDoNotChange.SalesAgentFileRecord safrSalesAgentFileRecord;
            string szSalesAgentName;
            string szSalesAgentEmailAddress;
            string szClientName;
            string szClientIdentifier;
            string szBankName;
            string szAccountNumber;
            string szSortCode;
            string szCurrency;
            int nNumberOfSalesAgents;
            int nNumberOfClients;
            int nNumberOfBankAccounts;
            int nSalesAgentCount;
            int nClientCount;
            int nBankAccountCount;

            szCsvFilename = String.Format("{0:yyyMMddHHmmss}-{1}.csv", DateTime.Now, GenerateRandomString(m_rRandom, 5, true));

            lsafrSalesAgentFileRecordList = new List<OriginalDoNotChange.SalesAgentFileRecord>();

            nNumberOfSalesAgents = m_nMinNumberOfSalesAgents + m_rRandom.Next(m_nMaxNumberOfSalesAgents - m_nMinNumberOfSalesAgents);

            for (nSalesAgentCount = 0; nSalesAgentCount < nNumberOfSalesAgents; nSalesAgentCount++)
            {
                szSalesAgentName = GenerateRandomString(m_rRandom, 15, true);
                szSalesAgentEmailAddress = String.Format("{0}@{1}.com", GenerateRandomString(m_rRandom, 10, true), GenerateRandomString(m_rRandom, 20, true));

                nNumberOfClients = m_nMinNumberOfClients + m_rRandom.Next(m_nMaxNumberOfClients - m_nMinNumberOfClients);

                for (nClientCount = 0; nClientCount < nNumberOfClients; nClientCount++)
                {
                    szClientName = GenerateRandomString(m_rRandom, 15, true);
                    szClientIdentifier = GenerateRandomString(m_rRandom, 20, true);

                    nNumberOfBankAccounts = m_nMinNumberOfBankAccounts + m_rRandom.Next(m_nMaxNumberOfBankAccounts - m_nMinNumberOfBankAccounts);

                    for (nBankAccountCount = 0; nBankAccountCount < nNumberOfBankAccounts; nBankAccountCount++)
                    {
                        szBankName = m_lszBankList[m_rRandom.Next(m_lszBankList.Count)];
                        szAccountNumber = GenerateRandomNumberString(m_rRandom, 8);
                        szSortCode = GenerateRandomNumberString(m_rRandom, 6);
                        szCurrency = m_lszCurrencyList[m_rRandom.Next(m_lszCurrencyList.Count)];

                        safrSalesAgentFileRecord = new OriginalDoNotChange.SalesAgentFileRecord(szSalesAgentName, szSalesAgentEmailAddress, szClientName, szClientIdentifier, szBankName, szAccountNumber, szSortCode, szCurrency);
                        lsafrSalesAgentFileRecordList.Add(safrSalesAgentFileRecord);
                    }
                }
            }

            if (!boRandomiseRecordOrder)
            {
                lsafrReturnSalesAgentFileRecordList = lsafrSalesAgentFileRecordList;
            }
            else
            {
                lsafrReturnSalesAgentFileRecordList = lsafrSalesAgentFileRecordList.Shuffle();
            }

            safrlReturnSalesAgentFileRecordList = new OriginalDoNotChange.SalesAgentFileRecordList(lsafrReturnSalesAgentFileRecordList);

            return (safrlReturnSalesAgentFileRecordList.ToCsvString());
        }

        private string GenerateRandomString(Random nRandom, int nSize, bool boLowerCase)
        {
            return (GenerateRandomString(nRandom, nSize, boLowerCase, false));
        }
        private string GenerateRandomString(Random nRandom, int nSize, bool boLowerCase, bool boNumbers)
        {
            StringBuilder sbString = new StringBuilder();
            char cChar;
            int nNumberOrChar;
            int nCount;
            string szReturnString;

            for (nCount = 0; nCount < nSize; nCount++)
            {
                if (!boNumbers)
                {
                    cChar = Convert.ToChar(nRandom.Next(26) + 65);
                }
                else
                {
                    nNumberOrChar = nRandom.Next(2);

                    if (nNumberOrChar == 0)
                    {
                        cChar = Convert.ToChar(nRandom.Next(26) + 65);
                    }
                    else
                    {
                        cChar = Convert.ToChar(nRandom.Next(10) + 48);
                    }
                }
                sbString.Append(cChar);
            }

            szReturnString = sbString.ToString();

            if (boLowerCase)
            {
                szReturnString = szReturnString.ToLower();
            }

            return (szReturnString);
        }
        private string GenerateRandomNumberString(Random rRandom, int nSize)
        {
            StringBuilder sbString = new StringBuilder();
            int nInt;
            int nCount;

            for (nCount = 0; nCount < nSize; nCount++)
            {
                nInt = rRandom.Next(10);
                sbString.Append(nInt);
            }

            return sbString.ToString();
        }

        public static bool SalesAgentListsEqual(OriginalDoNotChange.SalesAgentList salOriginalSalesAgentList, NewImproved.SalesAgentList salNewSalesAgentList)
        {
            bool boReturnValue = false;
            int nCount;
            bool boFinished;
            OriginalDoNotChange.SalesAgentList salOriginalSalesAgentListCopy;
            NewImproved.SalesAgentList salNewSalesAgentListCopy;

            // both null - they're equal
            if (salOriginalSalesAgentList == null && salNewSalesAgentList == null)
            {
                boReturnValue = true;
            }
            else
            {
                // either one is null - they're not equal
                if (salOriginalSalesAgentList == null || salNewSalesAgentList == null)
                {
                    boReturnValue = false;
                }
                else
                {
                    // counts not the same - they're not equal
                    if (salOriginalSalesAgentList.Count != salNewSalesAgentList.Count)
                    {
                        boReturnValue = false;
                    }
                    else
                    {
                        // create new lists as we need to do some sorting to ensure that if the ordering was changed as
                        // part of the parsing, that we can still validate the lists against each other
                        salOriginalSalesAgentListCopy = new OriginalDoNotChange.SalesAgentList(salOriginalSalesAgentList.GetListOfSalesAgentObjects());
                        salNewSalesAgentListCopy = new NewImproved.SalesAgentList(salNewSalesAgentList.GetListOfSalesAgentObjects());

                        salOriginalSalesAgentListCopy.Sort();
                        salNewSalesAgentListCopy.Sort();

                        nCount = 0;
                        boFinished = false;

                        while (!boFinished &&
                               nCount < salOriginalSalesAgentListCopy.Count)
                        {
                            if (!SalesAgentsEqual(salOriginalSalesAgentListCopy[nCount], salNewSalesAgentListCopy[nCount]))
                            {
                                boFinished = true;
                            }
                            nCount++;
                        }

                        boReturnValue = !boFinished;
                    }
                }
            }

            return (boReturnValue);
        }
        private static bool SalesAgentsEqual(OriginalDoNotChange.SalesAgent saOriginalSalesAgent, NewImproved.SalesAgent saNewSalesAgent)
        {
            bool boReturnValue = false;

            // both null - they're equal
            if (saOriginalSalesAgent == null && saNewSalesAgent == null)
            {
                boReturnValue = true;
            }
            else
            {
                // either one is null - they're not equal
                if (saOriginalSalesAgent == null || saNewSalesAgent == null)
                {
                    boReturnValue = false;
                }
                else
                {
                    // check the object contents
                    if (saOriginalSalesAgent.SalesAgentName == saNewSalesAgent.SalesAgentName &&
                        saOriginalSalesAgent.SalesAgentEmailAddress == saNewSalesAgent.SalesAgentEmailAddress &&
                        ClientListsEqual(saOriginalSalesAgent.ClientList, saNewSalesAgent.ClientList))
                    {
                        boReturnValue = true;
                    }
                    else
                    {
                        boReturnValue = false;
                    }
                }
            }

            return (boReturnValue);
        }
        private static bool ClientListsEqual(OriginalDoNotChange.ClientList clOriginalClientList, NewImproved.ClientList clNewClientList)
        {
            bool boReturnValue = false;
            int nCount;
            bool boFinished;

            // both null - they're equal
            if (clOriginalClientList == null && clNewClientList == null)
            {
                boReturnValue = true;
            }
            else
            {
                // either one is null - they're not equal
                if (clOriginalClientList == null || clNewClientList == null)
                {
                    boReturnValue = false;
                }
                else
                {
                    // counts not the same - they're not equal
                    if (clOriginalClientList.Count != clNewClientList.Count)
                    {
                        boReturnValue = false;
                    }
                    else
                    {
                        nCount = 0;
                        boFinished = false;

                        while (!boFinished &&
                               nCount < clOriginalClientList.Count)
                        {
                            if (!ClientsEqual(clOriginalClientList[nCount], clNewClientList[nCount]))
                            {
                                boFinished = true;
                            }
                            nCount++;
                        }

                        boReturnValue = !boFinished;
                    }
                }
            }

            return (boReturnValue);
        }
        private static bool ClientsEqual(OriginalDoNotChange.Client cOriginalClient, NewImproved.Client cNewClient)
        {
            bool boReturnValue = false;

            // both null - they're equal
            if (cOriginalClient == null && cNewClient == null)
            {
                boReturnValue = true;
            }
            else
            {
                // either one is null - they're not equal
                if (cOriginalClient == null || cNewClient == null)
                {
                    boReturnValue = false;
                }
                else
                {
                    // check the object contents
                    if (cOriginalClient.ClientName == cNewClient.ClientName &&
                        cOriginalClient.ClientIdentifier == cNewClient.ClientIdentifier &&
                        BankAccountListsEqual(cOriginalClient.BankAccountList, cNewClient.BankAccountList))
                    {
                        boReturnValue = true;
                    }
                    else
                    {
                        boReturnValue = false;
                    }
                }
            }

            return (boReturnValue);
        }
        private static bool BankAccountListsEqual(OriginalDoNotChange.BankAccountList balOriginalBankAccountList, NewImproved.BankAccountList balNewBankAccountList)
        {
            bool boReturnValue = false;
            int nCount;
            bool boFinished;

            // both null - they're equal
            if (balOriginalBankAccountList == null && balNewBankAccountList == null)
            {
                boReturnValue = true;
            }
            else
            {
                // either one is null - they're not equal
                if (balOriginalBankAccountList == null || balNewBankAccountList == null)
                {
                    boReturnValue = false;
                }
                else
                {
                    // counts not the same - they're not equal
                    if (balOriginalBankAccountList.Count != balNewBankAccountList.Count)
                    {
                        boReturnValue = false;
                    }
                    else
                    {
                        nCount = 0;
                        boFinished = false;

                        while (!boFinished &&
                               nCount < balOriginalBankAccountList.Count)
                        {
                            if (!BankAccountsEqual(balOriginalBankAccountList[nCount], balNewBankAccountList[nCount]))
                            {
                                boFinished = true;
                            }
                            nCount++;
                        }

                        boReturnValue = !boFinished;
                    }
                }
            }

            return (boReturnValue);
        }
        private static bool BankAccountsEqual(OriginalDoNotChange.BankAccount baOriginalBankAccount, NewImproved.BankAccount baNewBankAccount)
        {
            bool boReturnValue = false;

            // both null - they're equal
            if (baOriginalBankAccount == null && baNewBankAccount == null)
            {
                boReturnValue = true;
            }
            else
            {
                // either one is null - they're not equal
                if (baOriginalBankAccount == null || baNewBankAccount == null)
                {
                    boReturnValue = false;
                }
                else
                {
                    // check the object contents
                    if (baOriginalBankAccount.BankName == baNewBankAccount.BankName &&
                        baOriginalBankAccount.AccountNumber == baNewBankAccount.AccountNumber &&
                        baOriginalBankAccount.SortCode == baNewBankAccount.SortCode &&
                        baOriginalBankAccount.Currency == baNewBankAccount.Currency)
                    {
                        boReturnValue = true;
                    }
                    else
                    {
                        boReturnValue = false;
                    }
                }
            }

            return (boReturnValue);
        }
    }
}