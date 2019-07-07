using System.Drawing;
using System.Windows.Forms;

public static class RichTextBoxExtension
{
    public static void AppendText(this RichTextBox box, string text, Color? color = null)
    {
        box.SelectionStart = box.TextLength;
        box.SelectionLength = 0;

        box.SelectionColor = color ?? Color.Green;
        box.AppendText(text);
        box.SelectionColor = box.ForeColor;
    }
}