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

namespace Notepad
{
    public partial class MainForm : Form
    {
        private bool fileAlreadySave;
        private bool fileUpdated;
        private string currentFileName;
        private FontDialog fontDialog = new FontDialog();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            fileAlreadySave = false;
            fileUpdated = false;
            currentFileName = "";


        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sellectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectAll();
        }

        private void emekNotepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Notepad v1.0\nDeveloped by [Developer Name]", "About Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewMenuMethod();
        }

        private void NewMenuMethod()
        {
            if (fileUpdated)
            {
                DialogResult result = MessageBox.Show("Do you want to save changes to your document?", "Notepad", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (result)
                {
                    case DialogResult.Yes:
                        saveFileUpdated();
                        clearScreen();
                        break;
                    case DialogResult.No:
                        clearScreen();
                        break;
                }
            }
            else
            {
                clearScreen();
            }

            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = false;
            toolStripButton7.Enabled = false;
            toolStripButton8.Enabled = false;

            MessageToolStripStatusLabel.Text = "New document created.";
        }


        //OPEN
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMenuMethod();

        }

        private void OpenMenuMethod()
        {
            //OpenFileDialog ekledik
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Files (*.rtf)|*.rtf";

            DialogResult result = openFileDialog.ShowDialog();


            if (result == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialog.FileName) == ".txt")
                {
                    MainRichTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
                }
                if (Path.GetExtension(openFileDialog.FileName) == ".rtf")
                {
                    MainRichTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.RichText);
                }
                this.Text = Path.GetFileName(openFileDialog.FileName) + " - Notepad";

                fileAlreadySave = true;
                fileUpdated = false;
                currentFileName = openFileDialog.FileName;
            }
            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = false;
            toolStripButton7.Enabled = false;
            toolStripButton8.Enabled = false;

            MessageToolStripStatusLabel.Text = "File opened";
        }


        //SAVE
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void saveFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Files (*.rtf)|*.rtf";
            DialogResult saveFileResult = saveFileDialog.ShowDialog();


            if (saveFileResult == DialogResult.OK)
            {
                if (Path.GetExtension(saveFileDialog.FileName) == ".txt")
                {
                    MainRichTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                }
                if (Path.GetExtension(saveFileDialog.FileName) == ".rtf")
                {
                    MainRichTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                }
                this.Text = Path.GetFileName(saveFileDialog.FileName) + " - Notepad";

                fileAlreadySave = true;
                fileUpdated = false;
                currentFileName = saveFileDialog.FileName;
            }
        }

        private void MainRichTextBox_TextChanged(object sender, EventArgs e)
        {
            fileUpdated = true;
            undoToolStripMenuItem.Enabled = true;
            toolStripButton7.Enabled = true;

            undoToolStripMenuItem1.Enabled = true;
            redoToolStripMenuItem1.Enabled = false;

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileUpdated();

        }

        private void saveFileUpdated()
        {
            if (fileAlreadySave)
            {
                if (Path.GetExtension(currentFileName) == ".txt")
                {
                    MainRichTextBox.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);
                }
                if (Path.GetExtension(currentFileName) == ".rtf")
                {
                    MainRichTextBox.SaveFile(currentFileName, RichTextBoxStreamType.RichText);
                }

                fileUpdated = false;
            }
            else
            {
                if (fileUpdated)
                {
                    saveFile();
                }
                else
                {
                    clearScreen();
                }
            }

            MessageToolStripStatusLabel.Text = "Document saved.";
        }

        private void clearScreen()
        {
            MainRichTextBox.Clear();
            fileUpdated = false;
            this.Text = "Notepad";
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoMethod();
        }

        private void UndoMethod()
        {
            MainRichTextBox.Undo();
            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = true;
            toolStripButton7.Enabled = false;
            toolStripButton8.Enabled = true;
            undoToolStripMenuItem1.Enabled = false;
            redoToolStripMenuItem1.Enabled = true;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RedoMethod();
        }

        private void RedoMethod()
        {
            MainRichTextBox.Redo();
            redoToolStripMenuItem.Enabled = false;
            undoToolStripMenuItem.Enabled = true;
            toolStripButton7.Enabled = true;
            toolStripButton8.Enabled = false;
            undoToolStripMenuItem1.Enabled = true;
            redoToolStripMenuItem1.Enabled = false;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void formateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void formateFonteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormatFontMethod();
        }

        private void FormatFontMethod()
        {
            fontDialog.ShowColor = true;
            fontDialog.ShowApply = true;

            fontDialog.Apply += new System.EventHandler(font_Apply_Dialog);

            DialogResult result = fontDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (MainRichTextBox.SelectionLength > 0)
                {
                    MainRichTextBox.SelectionFont = fontDialog.Font;
                    MainRichTextBox.SelectionColor = fontDialog.Color;
                }

            }
        }

        private void font_Apply_Dialog(object sender, EventArgs e)
        {
            if (MainRichTextBox.SelectionLength > 0)
            {
                MainRichTextBox.SelectionFont = fontDialog.Font;
                MainRichTextBox.SelectionColor = fontDialog.Color;
            }
        }

        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectedText = DateTime.Now.ToString();
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontTextStyle(FontStyle.Bold);
        }

        private void FontTextStyle(FontStyle fontStyle)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, fontStyle);
        }

        private void ıtalicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontTextStyle(FontStyle.Italic);
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontTextStyle(FontStyle.Underline);
        }

        private void strikethroughToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontTextStyle(FontStyle.Strikeout);
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontTextStyle(FontStyle.Regular);
        }

        private void changeTextColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            DialogResult result = colorDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (MainRichTextBox.SelectionLength > 0)
                {
                    MainRichTextBox.SelectionColor = colorDialog.Color;
                }
            }
        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewMenuMethod();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenMenuMethod();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            saveFileUpdated();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            UndoMethod();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            RedoMethod();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            FontTextStyle(FontStyle.Bold);
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            FontTextStyle(FontStyle.Italic);
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            FontTextStyle(FontStyle.Underline);
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            FontTextStyle(FontStyle.Strikeout);
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            FormatFontMethod();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainRichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                capsLockToolStripStatusLabel.Text = "CAPS ON";
            }
            else
            {
                capsLockToolStripStatusLabel.Text = "caps off";
            }
        }

        private void capsLockToolStripStatusLabel_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UndoMethod();
        }

        private void redoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RedoMethod();
        }

        private void normalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontTextStyle(FontStyle.Regular);
        }

        private void boldToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontTextStyle(FontStyle.Bold);
        }

        private void ıtalicToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontTextStyle(FontStyle.Italic);
        }

        private void underlineToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontTextStyle(FontStyle.Underline);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                MainRichTextBox.Paste();
            }
            else
            {
                MessageBox.Show("Panoda yapıştırılacak bir metin bulunamadı.", // Gösterilecek ana mesaj
                                "Bilgi",                                     // Mesaj kutusunun başlığı
                                MessageBoxButtons.OK,                        // Gösterilecek buton (sadece "Tamam")
                                MessageBoxIcon.Information);                 // Gösterilecek ikon (bilgi ikonu)
            }
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Cut();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Copy();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                MainRichTextBox.Paste();
            }
            else
            {
                MessageBox.Show("Panoda yapıştırılacak bir metin bulunamadı.", // Gösterilecek ana mesaj
                                "Bilgi",                                     // Mesaj kutusunun başlığı
                                MessageBoxButtons.OK,                        // Gösterilecek buton (sadece "Tamam")
                                MessageBoxIcon.Information);                 // Gösterilecek ikon (bilgi ikonu)
            }
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Cut();
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Copy();
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                MainRichTextBox.Paste();
            }
            else
            {
                MessageBox.Show("Panoda yapıştırılacak bir metin bulunamadı.", // Gösterilecek ana mesaj
                                "Bilgi",                                     // Mesaj kutusunun başlığı
                                MessageBoxButtons.OK,                        // Gösterilecek buton (sadece "Tamam")
                                MessageBoxIcon.Information);                 // Gösterilecek ikon (bilgi ikonu)
            }
        }
    }
}