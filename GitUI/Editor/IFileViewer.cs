using System;
using System.Drawing;
using System.Windows.Forms;

namespace GitUI.Editor
{
    public interface IFileViewer
    {
        event EventHandler DoubleClick;

        event KeyEventHandler KeyDown;

        event EventHandler MouseEnter;

        event EventHandler MouseLeave;

        event MouseEventHandler MouseMove;

        event EventHandler ScrollPosChanged;

        event EventHandler<SelectedLineEventArgs> SelectedLineChanged;

        event EventHandler TextChanged;

        int FirstVisibleLine { get; set; }

        Font Font { get; set; }

        bool IsReadOnly { get; set; }

        int LineAtCaret { get; }

        int ScrollPos { get; set; }

        bool ShowEOLMarkers { get; set; }

        bool ShowLineNumbers { get; set; }

        bool ShowSpaces { get; set; }

        bool ShowTabs { get; set; }

        int TotalNumberOfLines { get; }

        bool Visible { get; set; }

        void AddPatchHighlighting();

        void ClearHighlighting();

        void EnableScrollBars(bool enable);

        void Find();

        void FocusTextArea();

        int GetLineFromVisualPosY(int visualPosY);

        string GetLineText(int line);

        string GetSelectedText();

        int GetSelectionLength();

        int GetSelectionPosition();

        string GetText();

        //lineNumber is 0 based
        void GoToLine(int lineNumber);

        void HighlightLine(int line, Color color);

        void HighlightLines(int startLine, int endLine, Color color);

        /// <summary>
        /// Indicates if the Goto line UI is applicable or not.
        /// Code-behind goto line function is always availabe, so we can goto next diff section.
        /// </summary>
        bool IsGotoLineUIApplicable();

        void SetFileLoader(Func<bool, Tuple<int, string>> fileLoader);

        void SetHighlighting(string syntax);

        void SetHighlightingForFile(string filename);

        void SetText(string text, bool isDiff = false);
    }

    public class SelectedLineEventArgs : EventArgs
    {
        public SelectedLineEventArgs(int selectedLine)
        {
            SelectedLine = selectedLine;
        }

        public int SelectedLine { get; private set; }
    }
}
