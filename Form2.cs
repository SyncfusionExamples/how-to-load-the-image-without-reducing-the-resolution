using Syncfusion.Windows.Forms.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridGroupingRebind
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.gridControl1.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Metro;
            gridControl1.ThemesEnabled = true;
            GridTextBoxCellRenderer ren = gridControl1.CellRenderers["TextBox"] as GridTextBoxCellRenderer;
            gridControl1.TableStyle.VerticalAlignment = GridVerticalAlignment.Middle;
            gridControl1.RowCount = 10;
            gridControl1.ColCount = 9;
            this.gridControl1.DefaultRowHeight = 150;
            this.gridControl1.ColWidths[9] = 200;
            CreateTable();
            this.gridControl1.PopulateHeaders(GridRangeInfo.Cells(0, 1, 0, this.gridControl1.ColCount), dt);
            this.gridControl1.ColStyles[9].CellType = GridCellTypeName.Image;
            for (int i = 1; i <= this.gridControl1.RowCount; i++)
            {
                for (int j = 1; j <= this.gridControl1.ColCount; j++)
                {
                    this.gridControl1[i, j].CellValue = dt.Rows[i - 1][j - 1];
                }
            }
        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
        }

        #region "Create DataTable"
        string[] name1 = new string[] { "John", "Peter", "Smith", "Jay", "Krish", "Mike" };
        string[] country = new string[] { "UK", "USA", "Pune", "India", "China", "England" };
        string[] city = new string[] { "Graz", "Resende", "Bruxelles", "Aires", "Rio de janeiro", "Campinas" };
        string[] scountry = new string[] { "Brazil", "Belgium", "Austria", "Argentina", "France", "Beiging" };
        DataTable dt = new DataTable();
        Random r = new Random();
        int col = 0;
        private DataTable CreateTable()
        {
            dt.Columns.Add("Name");
            dt.Columns.Add("Id");
            dt.Columns.Add("Date");
            dt.Columns.Add("Country");
            dt.Columns.Add("Ship City");
            dt.Columns.Add("Ship Country");
            dt.Columns.Add("Freight");
            dt.Columns.Add("Postal code");
            dt.Columns.Add("ImageColumn", typeof(byte[]));

            for (int l = 0; l <= gridControl1.RowCount; l++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = name1[r.Next(0, 5)];
                dr[1] = "E" + r.Next(30);
                dr[2] = new DateTime(2012, 5, 23);
                dr[3] = country[r.Next(0, 5)];
                dr[4] = city[r.Next(0, 5)];
                dr[5] = scountry[r.Next(0, 5)];
                dr[6] = r.Next(1000, 2000);
                dr[7] = r.Next(10 + (r.Next(600000, 600100)));
                //Adding image as Byte[] array.
                Byte[] imageArray = System.IO.File.ReadAllBytes(FindFile(@"flower" + l % 3 + ".jpg"));
                dr[8] = imageArray;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// Get the path of image file.
        /// </summary>
        /// <param name="bitmapName">ImageFile name.</param>
        /// <returns>ImageFile location.</returns>
        private static string FindFile(string fileName)
        {
            // Check both in parent folder and Parent\Data folders.
            //string dataFileName = @"Common\Data\" + fileName;
            string dataFileName = @"Image\" + fileName;
            for (int n = 0; n < 12; n++)
            {
                if (System.IO.File.Exists(fileName))
                {
                    return new FileInfo(fileName).FullName;
                }
                if (System.IO.File.Exists(dataFileName))
                {
                    return new FileInfo(dataFileName).FullName;
                }
                fileName = @"..\" + fileName;
                dataFileName = @"..\" + dataFileName;
            }

            return fileName;
        }
        #endregion
    }
}
