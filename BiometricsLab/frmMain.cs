using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Algorithms;
using Utils;
using BiometricData;
using Images;

namespace BiometricsLab
{   /*
     * Image indexes:
     *  0 - trainning
     *  1 - trainning
     *  2 - trainning
     *  3 - trainning
     *  4 - testing
     *  5 - testing 
     */
    public partial class frmMain : Form
    {
        #region Private Members

        private string _sFileName;
        private System.Drawing.Image imgOriginal;
        private List<Bitmap> lstHandBio = new List<Bitmap>();       
        private Dictionary<string, int> loadedImages = new Dictionary<string, int>();
        //private Thread threadTrainning;
        //private Thread threadTesting;
        BackgroundWorker bgW = new BackgroundWorker();
        private StringBuilder data = new StringBuilder();        

        #endregion

        #region Constructor

        public frmMain()
        {
            InitializeComponent();            

           // threadTrainning = new Thread(LoadTrainningPictures);
            //threadTesting = new Thread(LoadTestingPictures);
        }

        #endregion

        #region Private Events

        private void frmMain_Load(object sender, EventArgs e)
        {
          
            this.bgW.DoWork += new DoWorkEventHandler(LoadTrainningPictures);
            bgW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LoadTestingPictures);
            
            this.bgW.RunWorkerAsync();
            //this.threadTrainning.Start();
            //this.threadTesting.Start();                      
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.bgW.Dispose();
            //this.threadTrainning.Abort();
        }

        #endregion

        #region Private Methods

        private void LoadTrainningPictures(Object sender, DoWorkEventArgs e)
        {
            if (this.richTextBox1.InvokeRequired)
            {
                this.richTextBox1.BeginInvoke((MethodInvoker)delegate() { this.richTextBox1.AppendText("Starting image processing for trainning...\n");}); 
            }
            else
            {
                this.richTextBox1.AppendText("Starting image processing for trainning...\n");
            }

           String searchFolder = @"C:\img_data_940nm Tra1";
           var filters = new String[] { "jpg" };
           var files = GetFilesFrom(searchFolder, filters);
            
            int imgIndex = 1;
            foreach (string fileName in files)
            {
                if (imgIndex >= 7)
                    imgIndex = 1;
               
                if (this.richTextBox1.InvokeRequired)
                {
                    this.richTextBox1.BeginInvoke((MethodInvoker)delegate() { this.richTextBox1.AppendText(string.Format("Processing train image file {0} \n", fileName)); ; });
                }
                else
                {
                    this.richTextBox1.AppendText(string.Format("Processing train image file {0} \n", fileName));
                }

                this.imgOriginal = System.Drawing.Image.FromFile(fileName);

                if (Array.IndexOf(imgOriginal.PropertyIdList, 274) > -1)
                {
                    var orientation = (int)imgOriginal.GetPropertyItem(274).Value[0];
                    if (orientation == 6)
                        imgOriginal.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }

                //get file name    
                this._sFileName = fileName.Split('\\')[2].Replace(".jpg", "");

                //get image label
                string fileLabel = this._sFileName.Split('_')[0];

                if (!this.loadedImages.ContainsKey(fileLabel))
                {
                    this.loadedImages.Add(fileLabel, 1);
                }
                else
                {
                    this.loadedImages[fileLabel]++;
                }

              
                if(imgIndex == 1)
                this.pbTra1.Image = ProcessImage(this.imgOriginal, fileLabel, "train");

                if(imgIndex == 2)
                this.pbTra2.Image = ProcessImage(this.imgOriginal, fileLabel, "train");

                if (imgIndex == 3)
                    this.pbTra3.Image = ProcessImage(this.imgOriginal, fileLabel, "train");

                if (imgIndex == 4)
                    this.pbTra4.Image = ProcessImage(this.imgOriginal, fileLabel, "train");

                if (imgIndex == 5)
                    this.pbTra5.Image = ProcessImage(this.imgOriginal, fileLabel, "train");

                if (imgIndex == 6)
                    this.pbTra6.Image = ProcessImage(this.imgOriginal, fileLabel, "train");
               // this.pbTra1.Refresh(); invoke required here
                imgIndex++;                
            }

            //*************** save data put on a method
            
            DataFuser df = new DataFuser(UserData.Instance);
            string data = df.FuseTranningData();
           // string data = df.FuseVeinsData(handPalmVeinData);
            OutputterTxt ot = new OutputterTxt();            
            ot.SaveTrainningData(data);

            //*******************************

            if (this.richTextBox1.InvokeRequired)
            {
                this.richTextBox1.BeginInvoke((MethodInvoker)delegate() { this.richTextBox1.AppendText("Trainning ended\n"); });
            }
            else
            {
                this.richTextBox1.AppendText("Trainning ended.\n");
            }

            if (this.richTextBox1.InvokeRequired)
            {
                this.richTextBox1.BeginInvoke((MethodInvoker)delegate() { this.richTextBox1.AppendText("Saving trainning data...\n"); });
            }
            else
            {
                this.richTextBox1.AppendText("Saving trainning data...\n");
            }

            //this.SaveData("train");

            if (this.richTextBox1.InvokeRequired)
            {
                this.richTextBox1.BeginInvoke((MethodInvoker)delegate() { this.richTextBox1.AppendText("Trainning data saved.\n"); });
            }
            else
            {
                this.richTextBox1.AppendText("Trainning data saved.\n");
            }
        }

        private void LoadTestingPictures(Object sender, RunWorkerCompletedEventArgs e)
        {
            this.data.Clear();

            if (this.richTextBox1.InvokeRequired)
            {
                this.richTextBox1.BeginInvoke((MethodInvoker)delegate() { this.richTextBox1.AppendText("Starting image processing for testing...\n"); });
            }
            else
            {
                this.richTextBox1.AppendText("Starting testing processing...\n");
            }

            String searchFolder = @"C:\img_data_940nm Tes1";
            var filters = new String[] { "jpg" };
            var files = GetFilesFrom(searchFolder, filters);

            int imgIndex = 1;
            foreach (string fileName in files)
            {
                if (imgIndex >= 7)
                    imgIndex = 1;

                if (this.richTextBox1.InvokeRequired)
                {
                    this.richTextBox1.BeginInvoke((MethodInvoker)delegate() { this.richTextBox1.AppendText(string.Format("Processing test file {0} \n", fileName)); ; });
                }
                else
                {
                    this.richTextBox1.AppendText(string.Format("Processing test file {0} \n", fileName));
                }

                this.imgOriginal = System.Drawing.Image.FromFile(fileName);

                if (Array.IndexOf(imgOriginal.PropertyIdList, 274) > -1)
                {
                    var orientation = (int)imgOriginal.GetPropertyItem(274).Value[0];
                    if (orientation == 6)
                        imgOriginal.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }

                //get file name    
                this._sFileName = fileName.Split('\\')[2].Replace(".jpg", "");

                //get image label
                string fileLabel = this._sFileName.Split('_')[0];

                if (!this.loadedImages.ContainsKey(fileLabel))
                {
                    this.loadedImages.Add(fileLabel, 1);
                }
                else
                {
                    this.loadedImages[fileLabel]++;
                }


                if (imgIndex == 1)
                    this.pbTra1.Image = ProcessImage(this.imgOriginal, fileLabel, "test");

                if (imgIndex == 2)
                    this.pbTra2.Image = ProcessImage(this.imgOriginal, fileLabel, "test");

                if (imgIndex == 3)
                    this.pbTra3.Image = ProcessImage(this.imgOriginal, fileLabel, "test");

                if (imgIndex == 4)
                    this.pbTra4.Image = ProcessImage(this.imgOriginal, fileLabel, "test");

                if (imgIndex == 5)
                    this.pbTra5.Image = ProcessImage(this.imgOriginal, fileLabel, "test");

                if (imgIndex == 6)
                    this.pbTra6.Image = ProcessImage(this.imgOriginal, fileLabel, "test");
                // this.pbTra1.Refresh(); invoke required here
                imgIndex++;
            }

            //*************** save data put on a method

            DataFuser df = new DataFuser(UserData.Instance);
            string data = df.FuseTestingData();
           // string data = df.FuseVeinsData(handPalmVeinData);
            OutputterTxt ot = new OutputterTxt();
            ot.SaveTestingData(data);

            //*******************************

            if (this.richTextBox1.InvokeRequired)
            {
                this.richTextBox1.BeginInvoke((MethodInvoker)delegate() { this.richTextBox1.AppendText("Testing image processing ended\n"); });
            }
            else
            {
                this.richTextBox1.AppendText("Testing ended.\n");
            }

            if (this.richTextBox1.InvokeRequired)
            {
                this.richTextBox1.BeginInvoke((MethodInvoker)delegate() { this.richTextBox1.AppendText("Saving testing data...\n"); });
            }
            else
            {
                this.richTextBox1.AppendText("Saving testing data...\n");
            }

            //this.SaveData("test");

            if (this.richTextBox1.InvokeRequired)
            {
                this.richTextBox1.BeginInvoke((MethodInvoker)delegate() { this.richTextBox1.AppendText("Testing data saved.\n"); });
            }
            else
            {
                this.richTextBox1.AppendText("Testing data saved.\n");
            }
        }      
               
        private Bitmap ProcessImage(System.Drawing.Image img, string fileLabel, string type)
        {
            try
            {
                ImageProcessorMediator imp = new ImageProcessorMediator(img, fileLabel, type);                
                Bitmap imgHandCountour = imp.GetProcessedImage();
             
                return imgHandCountour;             
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR!", "Error processing image: " + ex.Message);
                return null;
            }            
        }

        private void SaveData(string type)
        {
            OutputterTxt ot = new OutputterTxt();
            //save trainning svm input file
            if (type.Equals("train"))
            {
                ot.SaveTrainningData(this.data.ToString());
                //roi.Save("C:/palms/train" + _sFileName + ".bmp", ImageFormat.Bmp);
            }
            else //save testing svm input file
            {
                ot.SaveTestingData(this.data.ToString());
                // roi.Save("C:/palms/test" + _sFileName + ".bmp", ImageFormat.Bmp);
            }
        }

        #endregion

        #region Public Methods

        public static String[] GetFilesFrom(String searchFolder, String[] filters)
        {
            List<String> filesFound = new List<String>();            
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), SearchOption.TopDirectoryOnly));
            }
            return filesFound.ToArray();
        }
         
        #endregion                       
    }
}
