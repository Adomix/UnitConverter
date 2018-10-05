using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Converter
{
    public partial class ConversionForm : Form
    {
        public decimal BaseValue { get; set; }
        WARequest Request { get; set; }
        Point PointA { get; set; }
        Point PointB { get; set; }

        public ConversionForm()
        {
            InitializeComponent();
            BaseValue = 0;
            Request = new WARequest();
            PointA = new Point(146, 71);
            PointB = new Point(312, 71);
        }

        private void InputBox_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(InputBox.Text, out var result))
            {
                BaseValue = result;
            }
            else
            {
                InputBox.Undo();
            }
        }

        private async void ConvertButton_Click(object sender, EventArgs e)
        {
            ComboBox[] boxes = new ComboBox[] { MetricUnits, ImperialUnits };
            var unitA = boxes.FirstOrDefault(x => x.Location == PointA).SelectedItem.ToString();
            var unitB = boxes.FirstOrDefault(x => x.Location == PointB).SelectedItem.ToString();

            if (Resultant.Text != string.Empty)
            {
                Resultant.Text = string.Empty;
            }
            Resultant.Text = "Loading!";
            var result = await Request.WaAsync(BaseValue, unitA, unitB);
            Resultant.Text = result;
        }

        private void UnitButton_Click(object sender, EventArgs e)
        {
            if (UnitButton.Text.Contains("Metric"))
            {
                UnitButton.Text = "To Imperial";
                MetricUnits.Location = PointA;
                ImperialUnits.Location = PointB;
            }
            else
            {
                UnitButton.Text = "To Metric";
                MetricUnits.Location = PointB;
                ImperialUnits.Location = PointA;
            }
        }
    }
}
