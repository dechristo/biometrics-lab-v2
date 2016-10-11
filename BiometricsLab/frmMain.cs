using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Utils;
using Images;
using System.Threading;

namespace BiometricsLab
{  
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
        private ImageProcessorMediator _imageProcessorMediator = null;

        #endregion

        #region Constructor

        public frmMain()
        {
            InitializeComponent();            

           // threadTrainning = new Thread(LoadTrainningPictures);
           // threadTesting = new Thread(LoadTestingPictures);
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
                this.pbTra1.Image = ProcessImage(this.imgOriginal, fileLabel, DataType.Trainning);

                if(imgIndex == 2)
                    this.pbTra2.Image = ProcessImage(this.imgOriginal, fileLabel, DataType.Trainning);

                if (imgIndex == 3)
                    this.pbTra3.Image = ProcessImage(this.imgOriginal, fileLabel, DataType.Trainning);

                if (imgIndex == 4)
                    this.pbTra4.Image = ProcessImage(this.imgOriginal, fileLabel, DataType.Trainning);

                if (imgIndex == 5)
                    this.pbTra5.Image = ProcessImage(this.imgOriginal, fileLabel, DataType.Trainning);

                if (imgIndex == 6)
                    this.pbTra6.Image = ProcessImage(this.imgOriginal, fileLabel, DataType.Trainning);
               // this.pbTra1.Refresh(); invoke required here
                imgIndex++;                
            }
            
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

            _imageProcessorMediator.SaveUserData(DataType.Trainning, false);

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
                    this.pbTra1.Image = ProcessImage(this.imgOriginal, fileLabel, DataType.Testing);

                if (imgIndex == 2)
                    this.pbTra2.Image = ProcessImage(this.imgOriginal, fileLabel, DataType.Testing);

                if (imgIndex == 3)
                    this.pbTra3.Image = ProcessImage(this.imgOriginal, fileLabel, DataType.Testing);

                if (imgIndex == 4)
                    this.pbTra4.Image = ProcessImage(this.imgOriginal, fileLabel, DataType.Testing);

                if (imgIndex == 5)
                    this.pbTra5.Image = ProcessImage(this.imgOriginal, fileLabel, DataType.Testing);

                if (imgIndex == 6)
                    this.pbTra6.Image = ProcessImage(this.imgOriginal, fileLabel, DataType.Testing);
                // this.pbTra1.Refresh(); invoke required here
                imgIndex++;
            }          

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

            _imageProcessorMediator.SaveUserData(DataType.Testing, false);

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
                _imageProcessorMediator = new ImageProcessorMediator(img, fileLabel, type);
                Bitmap imgHandCountour = _imageProcessorMediator.ProcessImage(type);
             
                return imgHandCountour;             
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error processing image: " + ex.Message, "ERROR");

                if (this.richTextBox1.InvokeRequired)
                {                    
                    this.richTextBoxErrors.BeginInvoke((MethodInvoker)delegate() { this.richTextBoxErrors.AppendText("Error processing file " + fileLabel + " :"); });
                    this.richTextBoxErrors.BeginInvoke((MethodInvoker)delegate() { this.richTextBoxErrors.AppendText(ex.Message); });
                    this.richTextBoxErrors.BeginInvoke((MethodInvoker)delegate() { this.richTextBoxErrors.AppendText("\n\n"); });
                    return null;
                }
                else
                {
                    this.richTextBoxErrors.AppendText("Error processing file " + fileLabel + " :");
                    this.richTextBoxErrors.AppendText(ex.Message);
                    this.richTextBoxErrors.AppendText("\n\n");
                    return null;
                }            
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

        private void binarizationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
