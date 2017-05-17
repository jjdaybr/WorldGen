using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldGen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            seedTextBox.Text = (0).ToString();
            seaLevelTextBox.Text = (90).ToString();
            landDensityXTextBox.Text = (200.0f).ToString();
            landDensityYTextBox.Text = (200.0f).ToString();
            landOffsetXTextBox.Text = (0.0f).ToString();
            landOffsetYTextBox.Text = (0.0f).ToString();
            treeLevelTextBox.Text = (175).ToString();
            treeDensityXTextBox.Text = (150.0f).ToString();
            treeDensityYTextBox.Text = (150.0f).ToString();
            treeOffsetXTextBox.Text = (0.0f).ToString();
            treeOffsetYTextBox.Text = (0.0f).ToString();
            mtnLevelTextBox.Text = (200).ToString();
            mtnDensityXTextBox.Text = (100.0f).ToString();
            mtnDensityYTextBox.Text = (100.0f).ToString();
            mtnOffsetXTextBox.Text = (0.0f).ToString();
            mtnOffsetYTextBox.Text = (0.0f).ToString();
            riverLevelTextBox.Text = (128).ToString();
            riverWindowTextBox.Text = (5).ToString();
            riverDensityXTextBox.Text = (300.0f).ToString();
            riverDensityYTextBox.Text = (300.0f).ToString();
            riverOffsetXTextBox.Text = (1000.0f).ToString();
            riverOffsetYTextBox.Text = (200.0f).ToString();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            int seaLevel = Convert.ToInt32(seaLevelTextBox.Text);
            float landDensityX = (float)Convert.ToDouble(landDensityXTextBox.Text);
            float landDensityY = (float)Convert.ToDouble(landDensityYTextBox.Text);
            float landOffsetX = (float)Convert.ToDouble(landOffsetXTextBox.Text);
            float landOffsetY = (float)Convert.ToDouble(landOffsetYTextBox.Text);
            int treeLevel = Convert.ToInt32(treeLevelTextBox.Text);
            float treeDensityX = (float)Convert.ToDouble(treeDensityXTextBox.Text);
            float treeDensityY = (float)Convert.ToDouble(treeDensityYTextBox.Text);
            float treeOffsetX = (float)Convert.ToDouble(treeOffsetXTextBox.Text);
            float treeOffsetY = (float)Convert.ToDouble(treeOffsetYTextBox.Text);
            int mtnLevel = Convert.ToInt32(mtnLevelTextBox.Text);
            float mtnDensityX = (float)Convert.ToDouble(mtnDensityXTextBox.Text);
            float mtnDensityY = (float)Convert.ToDouble(mtnDensityYTextBox.Text);
            float mtnOffsetX = (float)Convert.ToDouble( mtnOffsetXTextBox.Text);
            float mtnOffsetY = (float)Convert.ToDouble( mtnOffsetYTextBox.Text);

            int riverLevel = Convert.ToInt32(riverLevelTextBox.Text);
            int riverWindow = Convert.ToInt32(riverWindowTextBox.Text);
            float riverDensityX = (float)Convert.ToDouble(riverDensityXTextBox.Text);
            float riverDensityY = (float)Convert.ToDouble(riverDensityYTextBox.Text);
            float riverOffsetX = (float)Convert.ToDouble(riverOffsetXTextBox.Text);
            float riverOffsetY = (float)Convert.ToDouble(riverOffsetYTextBox.Text);

            PNoise.SimplexNoise(Convert.ToInt32(seedTextBox.Text));
            mapPictureBox.Image = PNoise.GetRender(mapPictureBox.Width, mapPictureBox.Height,
                seaLevel, landDensityX, landDensityY, landOffsetX, landOffsetY,
                treeLevel, treeDensityX, treeDensityY, treeOffsetX, treeOffsetY,
                mtnLevel, mtnDensityX, mtnDensityY, mtnOffsetX, mtnOffsetY,
                riverLevel, riverWindow, riverDensityX, riverDensityY, riverOffsetX, riverOffsetY);
        }
    }
}
