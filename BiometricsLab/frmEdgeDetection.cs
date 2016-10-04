using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Image.Filters;
using Image.Pool;
using FactoryMethod;
namespace BiometricsLab
{
    public partial class frmEdgeDetection : Form
    {
        #region Private Members

        private Bitmap imgEdgeDetected;
        private Bitmap imgBinary;
        private Filter edgeDetection = null;

        #endregion

        #region Constructors

        public frmEdgeDetection()
        {
            InitializeComponent();
            FactoryFilterEdgeDetection ffed = new FactoryFilterEdgeDetection();
            this.edgeDetection = ffed.Create();
        }

        public frmEdgeDetection(Bitmap img)
        {
            InitializeComponent();
            FactoryFilterEdgeDetection ffed = new FactoryFilterEdgeDetection();
            this.edgeDetection = ffed.Create();
            this.imgBinary = img;                        
            this.imgEdgeDetected = this.edgeDetection.ApplyFilter(this.imgBinary);           
            this.pictureBoxMain.Image = this.imgEdgeDetected;
            Pool.Instance.EdgeDetected = this.imgEdgeDetected;
        }

        #endregion

        #region Private Events

        private void pictureBoxMain_Click(object sender, EventArgs e)
        {
            this.pictureBoxMain.Image = this.edgeDetection.Invert(this.imgEdgeDetected);
            this.pictureBoxMain.Refresh();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Image.Filters.Thinning ft = new Thinning();
            //this.imgEdgeDetected = ft.Invert(this.imgEdgeDetected);
            //this.imgEdgeDetected = ft.ApplyFilter(this.imgEdgeDetected);
           // this.pictureBoxMain.Image = this.imgEdgeDetected;
        }
    }
}
