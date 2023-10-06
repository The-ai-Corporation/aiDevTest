using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace aiCorporation
{
    public partial class Form1 : Form
    {
        private string m_PathToStoreFailedCsvFiles;

        public Form1()
        {
            string szPathToStoreFailedCsvFiles;

            InitializeComponent();

            szPathToStoreFailedCsvFiles = ConfigurationManager.AppSettings["PathToStoreFailedCsvFiles"];

            if (!String.IsNullOrEmpty(szPathToStoreFailedCsvFiles))
            {
                m_PathToStoreFailedCsvFiles = szPathToStoreFailedCsvFiles.Replace("@@APPDIR@@", AppDomain.CurrentDomain.BaseDirectory);

                if (!Directory.Exists(m_PathToStoreFailedCsvFiles))
                {
                    Directory.CreateDirectory(m_PathToStoreFailedCsvFiles);
                }
            }
        }

        private void tbClear_Click(object sender, EventArgs e)
        {
            tbLog.Clear();
        }

        private void tbRunBulkTest_Click(object sender, EventArgs e)
        {
            TestValidator tvTestValidator;
            string szCsvContents;
            MemoryStream msMemoryStream;
            int nCount;
            int nBulkTestRounds;
            TimeSpan tsExecutionTime_NewImproved;
            TimeSpan? tsExecutionTime_Original;
            bool boParsingSuccessful;
            bool boAtLeastOneFileFailed;
            TimeSpan tsRunningExecutionTime_NewImproved;
            TimeSpan? tsRunningExecutionTime_Original = null;
            string szCsvFilename;

            try
            {
                nBulkTestRounds = Convert.ToInt32(tbBulkTestRounds.Text);

                tbLog.AppendText(String.Format("{0:dd/MM/yyyy HH:mm:ss}: Starting bulk test with {1} rounds\r\n", DateTime.Now, nBulkTestRounds));

                tvTestValidator = new TestValidator(Convert.ToInt32(tbMinSalesAgents.Text),
                                                    Convert.ToInt32(tbMaxSalesAgents.Text),
                                                    Convert.ToInt32(tbMinClients.Text),
                                                    Convert.ToInt32(tbMaxClients.Text),
                                                    Convert.ToInt32(tbMinBankAccounts.Text),
                                                    Convert.ToInt32(tbMaxBankAccounts.Text));

                boAtLeastOneFileFailed = false;
                tsRunningExecutionTime_NewImproved = new TimeSpan();

                for (nCount = 0; nCount < nBulkTestRounds; nCount++)
                {
                    // generate file with random data
                    szCsvContents = tvTestValidator.GenerateRandomFlatData(cbRandomiseRecordOrder.Checked, out szCsvFilename);
                    msMemoryStream = new MemoryStream(Encoding.ASCII.GetBytes(szCsvContents));

                    boParsingSuccessful = ValidateCsvFileContents(msMemoryStream, out tsExecutionTime_NewImproved, out tsExecutionTime_Original);

                    // compare this to the list that we started with
                    if (!boParsingSuccessful)
                    {
                        boAtLeastOneFileFailed = true;
                        tsRunningExecutionTime_Original = null;
                        File.WriteAllText(String.Format("{0}\\{1}", m_PathToStoreFailedCsvFiles, szCsvFilename), szCsvContents);
                        tbLog.AppendText(String.Format("{0:dd/MM/yyyy HH:mm:ss}: FILE {1} DIDN'T MATCH\r\n", DateTime.Now, nCount + 1));
                    }
                    else
                    {
                        if (!boAtLeastOneFileFailed)
                        {
                            tsRunningExecutionTime_NewImproved += tsExecutionTime_NewImproved;

                            if (tsRunningExecutionTime_Original == null)
                            {
                                tsRunningExecutionTime_Original = new TimeSpan();
                            }
                            tsRunningExecutionTime_Original += tsExecutionTime_Original.Value;
                        }
                    }
                }

                if (boAtLeastOneFileFailed)
                {
                    tbLog.AppendText(String.Format("{0:dd/MM/yyyy HH:mm:ss}: Ended bulk ({1} rounds) test with a failure\r\n", DateTime.Now, nBulkTestRounds));
                }
                else
                {
                    tbLog.AppendText(String.Format("{0:dd/MM/yyyy HH:mm:ss}: Successfully ended bulk test of {1} rounds. NEW classes are {2:0.0}x faster (on average)\r\n", DateTime.Now, nBulkTestRounds, (double)tsRunningExecutionTime_Original.Value.Ticks / (double)tsRunningExecutionTime_NewImproved.Ticks));
                }
            }
            catch (Exception exc)
            {
                tbLog.AppendText(exc.ToString() + "\r\n");
            }
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            DialogResult drDialogResult;
            OpenFileDialog ofdOpenFileDialog;

            ofdOpenFileDialog = new OpenFileDialog();

            ofdOpenFileDialog.Multiselect = false;
            ofdOpenFileDialog.DefaultExt = "*.csv";
            ofdOpenFileDialog.Filter = "CSV Files (*.csv)|*.csv;|All Files (*.*)|*.*";
            ofdOpenFileDialog.InitialDirectory = GetFilePath(tbCSVFilename.Text);

            drDialogResult = ofdOpenFileDialog.ShowDialog(this);

            if (drDialogResult == DialogResult.OK)
            {
                tbCSVFilename.Text = ofdOpenFileDialog.FileName;
            }
        }

        public static string GetFilePath(string szFullFilePath)
        {
            string szReturnValue = null;
            int nCount = 0;
            bool boFound = false;

            if (String.IsNullOrEmpty(szFullFilePath))
            {
                return (null);
            }

            nCount = szFullFilePath.Length - 1;

            while (!boFound &&
                   nCount >= 0)
            {
                if (szFullFilePath[nCount] == '\\' ||
                    szFullFilePath[nCount] == '/')
                {
                    szReturnValue = szFullFilePath.Substring(0, nCount + 1);
                    boFound = true;
                }
                else
                {
                    nCount--;
                }
            }

            return (szReturnValue);
        }

        private bool ValidateCsvFileContents(MemoryStream msMemoryStream, out TimeSpan dtNewImprovedExecutionTime, out TimeSpan? dtOriginalExecutionTime)
        {
            aiCorporation.OriginalDoNotChange.SalesAgentFileRecordList safrlSalesAgentFileRecordList_Original;
            aiCorporation.NewImproved.SalesAgentFileRecordList safrlSalesAgentFileRecordList_NewImproved;
            aiCorporation.NewImproved.SalesAgentFileRecordList safrlConvertedSalesAgentFileRecordList_NewImproved;
            DateTime dtStartDateTime;
            DateTime dtEndDateTime;
            aiCorporation.OriginalDoNotChange.SalesAgentList salSalesAgentList_Original;
            aiCorporation.NewImproved.SalesAgentList salSalesAgentList_NewImproved;
            bool boParsingSuccessful;

            dtOriginalExecutionTime = null;

            // read the csv file from the specified path
            msMemoryStream.Seek(0, SeekOrigin.Begin);
            safrlSalesAgentFileRecordList_NewImproved = aiCorporation.NewImproved.SalesAgentFileRecordList.FromCsvStream(msMemoryStream);

            // set this run's start time
            dtStartDateTime = DateTime.Now;

            // CONVERT TO A HEIRACRCHICAL LIST
            salSalesAgentList_NewImproved = safrlSalesAgentFileRecordList_NewImproved.ToSalesAgentList();

            // set this run's end time
            dtEndDateTime = DateTime.Now;
            // calculate this run's execution time
            dtNewImprovedExecutionTime = dtEndDateTime - dtStartDateTime;

            // convert the heirarchical list back to a flat list
            safrlConvertedSalesAgentFileRecordList_NewImproved = salSalesAgentList_NewImproved.ToSalesAgentFileRecordList();

            // the file matched, so now compare the time to process against the original classes

            // read the csv file from the specified path
            msMemoryStream.Seek(0, SeekOrigin.Begin);
            safrlSalesAgentFileRecordList_Original = aiCorporation.OriginalDoNotChange.SalesAgentFileRecordList.FromCsvStream(msMemoryStream);

            // set this run's start time
            dtStartDateTime = DateTime.Now;

            // CONVERT TO A HEIRACRCHICAL LIST
            salSalesAgentList_Original = safrlSalesAgentFileRecordList_Original.ToSalesAgentList();

            // set this run's end time
            dtEndDateTime = DateTime.Now;
            // calculate this run's execution time
            dtOriginalExecutionTime = dtEndDateTime - dtStartDateTime;

            // compare this to the list that we started with
            boParsingSuccessful = TestValidator.SalesAgentListsEqual(salSalesAgentList_Original, salSalesAgentList_NewImproved);

            return (boParsingSuccessful);
        }

        private void btTest_Click(object sender, EventArgs e)
        {
            MemoryStream msMemoryStream;
            TimeSpan tsExecutionTime_NewImproved;
            TimeSpan? tsExecutionTime_Original = null;
            bool boParsingSuccessful;

            try
            {
                tbLog.AppendText(String.Format("{0:dd/MM/yyyy HH:mm:ss}: Starting single file test\r\n", DateTime.Now));

                msMemoryStream = new MemoryStream(File.ReadAllBytes(tbCSVFilename.Text));

                boParsingSuccessful = ValidateCsvFileContents(msMemoryStream, out tsExecutionTime_NewImproved, out tsExecutionTime_Original);

                if (!boParsingSuccessful)
                {
                    tbLog.AppendText(String.Format("{0:dd/MM/yyyy HH:mm:ss}: FILE DIDN'T MATCH\r\n", DateTime.Now));
                }
                else
                {
                    tbLog.AppendText(String.Format("{0:dd/MM/yyyy HH:mm:ss}: FILE MATCHED\r\n", DateTime.Now));
                }

                tbLog.AppendText(String.Format("{0:dd/MM/yyyy HH:mm:ss}: Ended single file test. NEW classes Execution time: {1:0.00} ms\r\n", DateTime.Now, tsExecutionTime_NewImproved.TotalMilliseconds));

                if (tsExecutionTime_Original != null)
                {
                    tbLog.AppendText(String.Format("{0:dd/MM/yyyy HH:mm:ss}: Ended single file test. Original classes Execution time: {1:0.00} ms. NEW classes are {2:0.0}x faster\r\n", DateTime.Now, tsExecutionTime_Original.Value.TotalMilliseconds, (double)tsExecutionTime_Original.Value.Ticks / (double)tsExecutionTime_NewImproved.Ticks));
                }
            }
            catch (Exception exc)
            {
                tbLog.AppendText(exc.ToString() + "\r\n");
            }
        }
    }
}