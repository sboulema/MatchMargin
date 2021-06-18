using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Windows;

namespace Microsoft.VisualStudio.PowerTools.MatchMargin
{
    internal class MatchMargin : IWpfTextViewMargin
    {
        public const string Name = "MatchMargin";
        private readonly MatchMarginElement _matchMarginElement;
        private bool _isDisposed;

        public MatchMargin(IWpfTextViewHost textViewHost, IVerticalScrollBar scrollBar, MatchMarginFactory factory)
        {
            if (textViewHost == null)
            {
                throw new ArgumentNullException("textViewHost");
            }

            _matchMarginElement = new MatchMarginElement(textViewHost.TextView, factory, scrollBar);
        }

        public FrameworkElement VisualElement
        {
            get
            {
                ThrowIfDisposed();
                return _matchMarginElement;
            }
        }

        public double MarginSize
        {
            get
            {
                ThrowIfDisposed();
                return _matchMarginElement.ActualWidth;
            }
        }

        public bool Enabled
        {
            get
            {
                ThrowIfDisposed();
                return _matchMarginElement.Enabled;
            }
        }

        public ITextViewMargin GetTextViewMargin(string marginName)
        {
            return string.Compare(marginName, MatchMargin.Name, StringComparison.OrdinalIgnoreCase) == 0 ? this : (ITextViewMargin)null;
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _matchMarginElement.Dispose();
                GC.SuppressFinalize(this);
                _isDisposed = true;
            }
        }

        private void ThrowIfDisposed()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(Name);
            }
        }
    }
}
