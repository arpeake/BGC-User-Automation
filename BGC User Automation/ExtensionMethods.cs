using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Drawing.Design;
using System.Drawing;



namespace BGC_User_Automation
{
    public static class ExtensionMethods
    {
        public static void UpdateLog()
        {

        }

        public static void AppendRTBText(this RichTextBox box, string text, System.Windows.Media.Brush color, bool bold = false)
        {
            if (box.Dispatcher.Thread != System.Threading.Thread.CurrentThread)
            {
                box.Dispatcher.BeginInvoke((Action) (() =>
                {
                    TextRange rangeOfText = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd);
                    rangeOfText.Text = text;
                    rangeOfText.ApplyPropertyValue(TextElement.ForegroundProperty, color);
                    if (bold)
                        rangeOfText.ApplyPropertyValue(TextElement.ForegroundProperty, FontStyle.Bold);
                    //box.AppendText(text);
                }));
            }
            else
            {
                TextRange rangeOfText = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd);
                rangeOfText.Text = text;
                rangeOfText.ApplyPropertyValue(TextElement.ForegroundProperty, color);
                if (bold)
                    rangeOfText.ApplyPropertyValue(TextElement.ForegroundProperty, FontStyle.Bold);
            }
        }

        public static void UpdateStatusLabel(this TextBlock box, string text)
        {
            if (box.Dispatcher.Thread != System.Threading.Thread.CurrentThread)
            {
                box.Dispatcher.BeginInvoke((Action) (()=>
                    {
                        box.Text = text;
                    }));
            }
            else
            {
                box.Text = text;
            }
        }

        public static void UpdateTextBoxTS(this TextBox box, string text)
        {
            if(box.Dispatcher.Thread != System.Threading.Thread.CurrentThread)
            {
                box.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        box.Text = text;
                    }));
            }
            else
            {
                box.Text = text;
            }
        }
    }
}
