using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace Microsoft.VisualStudio.PowerTools.MatchMargin
{
    [Export(typeof(IWpfTextViewMarginProvider))]
    [MarginContainer(PredefinedMarginNames.VerticalScrollBar)]
    [Name(MatchMargin.Name)]
    [Order(After = PredefinedMarginNames.OverviewChangeTracking, Before = PredefinedMarginNames.OverviewMark)]
    [ContentType("text")]
    [TextViewRole(PredefinedTextViewRoles.Interactive)]
    [DeferCreation(OptionName = MatchMarginEnabledOption.OptionName)]
    internal sealed class MatchMarginFactory : IWpfTextViewMarginProvider
    {
        [Import]
        internal IEditorFormatMapService EditorFormatMapService;

        [Export]
        [Name("MatchMarginAdornmentLayer")]
        [Order(After = PredefinedAdornmentLayers.Outlining, Before = PredefinedAdornmentLayers.Selection)]
        internal AdornmentLayerDefinition matchLayerDefinition;

        public IWpfTextViewMargin CreateMargin(IWpfTextViewHost textViewHost, IWpfTextViewMargin containerMargin)
            => containerMargin is IVerticalScrollBar containerMarginAsVerticalScrollBar
                ? new MatchMargin(textViewHost, containerMarginAsVerticalScrollBar, this)
                : null;
    }
}
