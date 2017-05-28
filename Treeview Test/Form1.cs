using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Treeview_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (e.Node.Tag != null)
            {
                string fileImage = e.Node.Tag.ToString();
                pictureBox1.Load(fileImage);
            }
            else
                pictureBox1.Image = null;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            var folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                ListDirectory(this.treeView1, folderBrowser.SelectedPath);
             
            }
            
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
            
        }
        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));

            string[] extensions = new[] { ".jpg", ".jpeg", ".png", ".bmp" };
            FileInfo[] files =
                directoryInfo.EnumerateFiles()
                .Where(f => extensions.Contains(f.Extension.ToLower()))
                .ToArray();

            foreach (var file in files)
            {
                
                var newNode = new TreeNode(file.Name);
                    newNode.Tag = file.FullName;
                directoryNode.Nodes.Add(newNode);

            }

            return directoryNode;          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            else
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        
        }
    }

}
