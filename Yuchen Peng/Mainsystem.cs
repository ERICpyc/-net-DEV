using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yuchen_Peng_13147715_Assignment2
{
   
    public partial class Mainsystem : Form
    {
        string path = null;//set a path to judge the current open file 
        string isSaved = "n";//set a string to judge whether the file is saved
        int initLenglt = 0;
        bool ve_logout = false;//set a bool for judge different mode when logout
        public Mainsystem(string type)
        {
            InitializeComponent();
            if (type == "View")//check whether in view mode
            {
                //change the textbox to readonly and add a  view mode in the title
                richTextBox1.ReadOnly = true;
                Text = "Text Editor (View Mode *)";
                //change all these Enabled property to false if in view mode
                this.Save_tsml.Enabled = false;
                this.toolButton_Saveas.Enabled = false;
                this.toolButton_b.Enabled = false;
                this.toolButton_i.Enabled = false;
                this.toolButton1_u.Enabled = false;
                this.toolButton_cut.Enabled = false;
                this.toolButton_paste.Enabled = false;
                this.toolStripButton2.Enabled = false;
                this.toolStripButton3.Enabled = false;
                this.combox_font.Enabled = false;
                this.savefile_btn.Enabled = false;
                this.toolStripMenuItem7.Enabled = false;
                this.cut_btn.Enabled = false;
                this.paste_btn.Enabled = false;
                this.redoToolStripMenuItem.Enabled = false;
                this.undo_btn.Enabled = false;
                this.font_btn.Enabled = false;
                this.color_btn.Enabled = false;
                ve_logout = true;//set the flag to true if in view mode 
            }
        }


        private void AboutItem_Click_1(object sender, EventArgs e)
        {
            About about = new About();//open about form
            about.Show();
        }

        private void LoginoutItem_Click(object sender, EventArgs e)
        {
            if (ve_logout)//if in view mode,show a differnet prompt when logout
            {
                DialogResult Vchoice = MessageBox.Show("Sure to logout? ", "logout", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Vchoice == DialogResult.Yes)
                {
                    this.Close();//close this page
                    Login login = new Login();//logout to login page
                    login.Show();//load login page
                }
            }
            else
            {
                DialogResult Echoice = MessageBox.Show("Sure to logout? Any unsaved changes will not be saved automatically! ", "logout", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Echoice == DialogResult.Yes)
                {
                    this.Close();
                    Login login = new Login();
                    login.Show();
                }
            }
            
        }

        private void openfile_btn_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        public void OpenFile()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Open a Text File";
            openFile.DefaultExt = "*.rtf";
            openFile.Filter = "RTF Files|*.rtf";//Filter the rtf type to open 
            DialogResult dr = openFile.ShowDialog();
            if (dr == DialogResult.OK)
            {
                path = openFile.FileName;
                try
                {
                    richTextBox1.LoadFile(path);
                    this.Text = "Current file: " + path;//change the title text to current file
                    this.isSaved = "y";//Opened file is saved
                }
                catch (Exception)
                {

                    MessageBox.Show("Please select a suitable file, open failed");
                }

                // Do Something to read the file content
            }

        }
        public void NewFile()
        {
            if (this.richTextBox1.Text == "" || isSaved == "y" )//when the textbox is empty and is saved, clear all the info without asking
            {
                richTextBox1.Clear();
                Text = "New File";//change the title text to new file
                path = null;
                isSaved = "n";//change the saved flag to n
            }
            else//if something there and not saved text
            {
                DialogResult result = MessageBox.Show("The content has not been saved, do you want to save it as a new file?", "Warning", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Warning);//prompt user whether to save it
                if (result == DialogResult.Yes)
                {
                    SaveAsFile();//sane the unsaved text
                    isSaved = "y";//change flag
                }
                else if (result == DialogResult.No)
                {
                    richTextBox1.Clear();//clear the textbox
                    Text = "New File";
                    path = null;
                    isSaved = "n";
                }
            }
            
        }
        private void newfile_btn_Click(object sender, EventArgs e)
        {
            NewFile();

        }

        private void savefile_btn_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0 || isSaved == "n")//when click save button, the unsaved text should prompt the dialog also save as 
            {
                SaveAsFile();
            }
            
            else//if the file saved before just save it without asking
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                this.richTextBox1.SaveFile(path, RichTextBoxStreamType.RichText);
                this.Text = "Current file: " + path + " has been updated!";//change a message prompt
            }
        }
        private void Save_tsml_Click(object sender, EventArgs e)//same method 
        {
            if (richTextBox1.Text.Length == 0 || isSaved == "n")
            {
                SaveAsFile();
            }
            else
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                this.richTextBox1.SaveFile(path, RichTextBoxStreamType.RichText);
                this.Text = "Current file: " + path + " has been updated!";//change the text in title to prompt user the file updated/saved
            }
        }
        private void toolButton_Saveas_Click(object sender, EventArgs e)
        {
            SaveAsFile();//save as 
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            SaveAsFile();//save as
        }

        public void SaveAsFile()
        {
            SaveFileDialog saveasFile = new SaveFileDialog();
            saveasFile.Filter = "RTF Files (*.rtf)|*.rtf ";//save as rtf format
            DialogResult dr = saveasFile.ShowDialog();
            if (dr == DialogResult.OK)
            {
                path = saveasFile.FileName;
                this.richTextBox1.SaveFile(path, RichTextBoxStreamType.RichText); 
                this.Text = "Current file: " + path;
                this.initLenglt = richTextBox1.TextLength;
                this.isSaved = "y";//change the isSaved to y
            }
        }


        private void cut_tsmi_Click(object sender, EventArgs e)
        {
            Cut();//cut method
        }

        public void Cut()//cut method used twice
        {
            if (richTextBox1.SelectionLength > 0)//should have something selected
            {
                this.richTextBox1.Cut();
            }
        }
        private void copy_tsmi_Click(object sender, EventArgs e)
        {
            Copy();
        }

        public void Copy()//copy used twice
        {
            if (richTextBox1.SelectionLength > 0)
            {
                this.richTextBox1.Copy();//if text exist perform copy
            }
        }
        private void paste_tsmi_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Paste();
        }

        private void undo_btn_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Undo();
        }

        private void font_btn_Click(object sender, EventArgs e)
        {
            if (this.fontDialog1.ShowDialog() == DialogResult.OK)
            {
                Font font = this.fontDialog1.Font;//open the font dialog
                this.richTextBox1.SelectionFont = font;
            }
        }

        private void color_btn_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color color = this.colorDialog1.Color;//open the color dialog

                this.richTextBox1.SelectionColor = color;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void toolButton_cut_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void toolButton_copy_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void toolButton_paste_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Paste();
        }

        private void toolButton_b_Click(object sender, EventArgs e)//blod button
        {
            ChangeFontStyle(FontStyle.Bold);
        }
        private void toolButton_i_Click(object sender, EventArgs e)//italics button 
        {
            ChangeFontStyle(FontStyle.Italic);
        }

        private void toolButton1_u_Click(object sender, EventArgs e)//underline button
        {
            ChangeFontStyle(FontStyle.Underline);
        }

        

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            About about = new About();//show about form
            about.Show();
        }

        private void Mainsystem_Load(object sender, EventArgs e)
        {
            richTextBox1.Focus();
            usershow.Text = PassValue.passvalue;//pass the username value via passvalue class and show it on usershow label
            
        }

        private void combox_font_SelectedIndexChanged(object sender, EventArgs e)//font selection combobox
        {
            Font currentFont = richTextBox1.SelectionFont;
            FontStyle newFontStyle = (FontStyle)(currentFont.Style);
            richTextBox1.SelectionFont = new Font(currentFont.FontFamily, Int32.Parse(combox_font.SelectedItem.ToString()), newFontStyle);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Redo();
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            this.richTextBox1.Undo();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Redo();
        }
        private void ChangeFontStyle(FontStyle style)//this is a method for modifying the correct font
        {
            if (style != FontStyle.Bold && style != FontStyle.Italic &&
                style != FontStyle.Underline)
                throw new System.InvalidProgramException("wrong font!");
            RichTextBox tempRichTextBox = new RichTextBox();  //store a copy of the selected text
            int curRtbStart = richTextBox1.SelectionStart;
            int len = richTextBox1.SelectionLength;//store a copy of selected length
            int tempRtbStart = 0;
            Font font = richTextBox1.SelectionFont;
            if (len <= 1 && font != null) //not selected text 
            {
                if (style == FontStyle.Bold && font.Bold ||
                    style == FontStyle.Italic && font.Italic ||
                    style == FontStyle.Underline && font.Underline)
                {
                    richTextBox1.SelectionFont = new Font(font, font.Style ^ style);
                }
                else if (style == FontStyle.Bold && !font.Bold ||
                        style == FontStyle.Italic && !font.Italic ||
                        style == FontStyle.Underline && !font.Underline)
                {
                    richTextBox1.SelectionFont = new Font(font, font.Style | style);
                }
                return;
            }
            tempRichTextBox.Rtf = richTextBox1.SelectedRtf;
            tempRichTextBox.Select(len - 1, 1); 
            //Select the last text in the copy 
            //Clone the selected text Font, this tempFont is mainly used to judge
            //Whether the selected text should be bold, bold, italic, oblique, underlined, or underlined
            Font tempFont = (Font)tempRichTextBox.SelectionFont.Clone(); 
            for (int i = 0; i < len; i++)
            {
                tempRichTextBox.Select(tempRtbStart + i, 1);  //Select one at a time, and bold or remove one by one
                if (style == FontStyle.Bold && tempFont.Bold ||
                    style == FontStyle.Italic && tempFont.Italic ||
                    style == FontStyle.Underline && tempFont.Underline)
                {
                    tempRichTextBox.SelectionFont =
                        new Font(tempRichTextBox.SelectionFont,
                                 tempRichTextBox.SelectionFont.Style ^ style);
                }
                else if (style == FontStyle.Bold && !tempFont.Bold ||
                        style == FontStyle.Italic && !tempFont.Italic ||
                        style == FontStyle.Underline && !tempFont.Underline)
                {
                    tempRichTextBox.SelectionFont =
                        new Font(tempRichTextBox.SelectionFont,
                                 tempRichTextBox.SelectionFont.Style | style);
                }
            }
            tempRichTextBox.Select(tempRtbStart, len);
            richTextBox1.SelectedRtf = tempRichTextBox.SelectedRtf; //Copy the formatted copy to the prototype 
            richTextBox1.Select(curRtbStart, len);
        }
    }
}
