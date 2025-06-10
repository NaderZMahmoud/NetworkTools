using System;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace NetworkTool
{
    public class EndpointCheckerControl : UserControl
    {
        public TextBox endpointTextBox;
        public Button pingButton;
        public RichTextBox resultRichTextBox;
        public Label endpointLabel;
        public NumericUpDown durationNumericUpDown;
        public NumericUpDown intervalNumericUpDown;
        public Label durationLabel;
        public Label intervalLabel;

        public EndpointCheckerControl()
        {
            endpointTextBox = new TextBox();
            pingButton = new Button();
            resultRichTextBox = new RichTextBox();
            endpointLabel = new Label();
            durationNumericUpDown = new NumericUpDown();
            intervalNumericUpDown = new NumericUpDown();
            durationLabel = new Label();
            intervalLabel = new Label();

            // endpointLabel
            endpointLabel.Location = new System.Drawing.Point(12, 15);
            endpointLabel.Size = new System.Drawing.Size(60, 23);
            endpointLabel.Text = "Endpoint:";

            // endpointTextBox
            endpointTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            endpointTextBox.Location = new System.Drawing.Point(80, 12);
            endpointTextBox.Size = new System.Drawing.Size(300, 23);
            endpointTextBox.Text = "8.8.8.8";

            // pingButton
            pingButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pingButton.Location = new System.Drawing.Point(390, 12);
            pingButton.Size = new System.Drawing.Size(60, 23);
            pingButton.Text = "Ping";
            pingButton.Click += PingButton_Click!;

            // durationLabel
            durationLabel.Location = new System.Drawing.Point(12, 40);
            durationLabel.Size = new System.Drawing.Size(80, 23);
            durationLabel.Text = "Duration (s):";

            durationNumericUpDown.Location = new System.Drawing.Point(100, 40);
            durationNumericUpDown.Size = new System.Drawing.Size(60, 23);
            durationNumericUpDown.Minimum = 1;
            durationNumericUpDown.Maximum = 3600;
            durationNumericUpDown.Value = 5;

            // intervalLabel
            intervalLabel.Location = new System.Drawing.Point(180, 40);
            intervalLabel.Size = new System.Drawing.Size(90, 23);
            intervalLabel.Text = "Interval (ms):";

            intervalNumericUpDown.Location = new System.Drawing.Point(270, 40);
            intervalNumericUpDown.Size = new System.Drawing.Size(80, 23);
            intervalNumericUpDown.Minimum = 100;
            intervalNumericUpDown.Maximum = 10000;
            intervalNumericUpDown.Value = 1000;

            // resultRichTextBox
            resultRichTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            resultRichTextBox.Location = new System.Drawing.Point(12, 75);
            resultRichTextBox.Size = new System.Drawing.Size(438, 185);
            resultRichTextBox.ReadOnly = true;
            resultRichTextBox.Font = new System.Drawing.Font("Consolas", 10);
            resultRichTextBox.BackColor = System.Drawing.Color.White;
            resultRichTextBox.ForeColor = System.Drawing.Color.Black;
            resultRichTextBox.WordWrap = false;

            Controls.Add(endpointLabel);
            Controls.Add(endpointTextBox);
            Controls.Add(pingButton);
            Controls.Add(durationLabel);
            Controls.Add(durationNumericUpDown);
            Controls.Add(intervalLabel);
            Controls.Add(intervalNumericUpDown);
            Controls.Add(resultRichTextBox);
            this.Dock = DockStyle.Fill;

            this.Resize += (s, e) =>
            {
                endpointTextBox.Size = new System.Drawing.Size(this.Width - 80 - 70 - 18, 23);
                pingButton.Location = new System.Drawing.Point(this.Width - 70 - 18, 12);
                resultRichTextBox.Size = new System.Drawing.Size(this.Width - 24, this.Height - 62);
            };
        }

        private async void PingButton_Click(object sender, EventArgs e)
        {
            string endpoint = endpointTextBox.Text.Trim();
            if (string.IsNullOrEmpty(endpoint))
            {
                resultRichTextBox.Text = "Please enter an endpoint.";
                return;
            }
            // Extract hostname if a URL is entered
            if (endpoint.StartsWith("http://") || endpoint.StartsWith("https://"))
            {
                try
                {
                    var uri = new Uri(endpoint);
                    endpoint = uri.Host;
                }
                catch
                {
                    resultRichTextBox.Text = "Invalid URL format.";
                    return;
                }
            }
            int durationSeconds = (int)durationNumericUpDown.Value;
            int intervalMs = (int)intervalNumericUpDown.Value;
            int elapsed = 0;
            int count = 0;
            resultRichTextBox.Text = $"Pinging {endpoint} for {durationSeconds} seconds (interval {intervalMs} ms)...\n";
            try
            {
                using var ping = new System.Net.NetworkInformation.Ping();
                var watch = System.Diagnostics.Stopwatch.StartNew();
                while (elapsed < durationSeconds * 1000)
                {
                    var reply = await ping.SendPingAsync(endpoint, 2000);
                    resultRichTextBox.AppendText($"Reply {++count}: Status={reply.Status}, Time={reply.RoundtripTime}ms\n");
                    await System.Threading.Tasks.Task.Delay(intervalMs);
                    elapsed = (int)watch.ElapsedMilliseconds;
                }
                resultRichTextBox.AppendText($"--- Finished pinging {endpoint} for {durationSeconds} seconds ---\n");
            }
            catch (Exception ex)
            {
                resultRichTextBox.AppendText($"Error: {ex.Message}\n");
            }
        }
    }
}
