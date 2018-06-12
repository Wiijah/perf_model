using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace PerformanceModeller.Components
{
    public partial class DurationRegexComponent : UserControl
    {
        public DurationRegexComponent()
        {
            InitializeComponent();
        }

        private void GroupIdCheckNumeric(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
