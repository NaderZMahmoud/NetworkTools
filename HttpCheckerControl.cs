using System.Windows.Forms;

namespace NetworkTool
{
    public class HttpCheckerControl : UserControl
    {
        public TextBox urlTextBox;
        public Button sendButton;
        public RichTextBox responseRichTextBox;
        public ComboBox formatComboBox;
        public ComboBox methodComboBox;
        public TextBox headersTextBox;
        public TextBox bodyTextBox;
        public Label headersLabel;
        public Label bodyLabel;

        public HttpCheckerControl()
        {
            urlTextBox = new TextBox();
            sendButton = new Button();
            responseRichTextBox = new RichTextBox();
            formatComboBox = new ComboBox();
            methodComboBox = new ComboBox();
            headersTextBox = new TextBox();
            bodyTextBox = new TextBox();
            headersLabel = new Label();
            bodyLabel = new Label();

            // urlTextBox
            urlTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            urlTextBox.Location = new System.Drawing.Point(12, 12);
            urlTextBox.Size = new System.Drawing.Size(this.Width - 12 - 12 - 270, 23);
            urlTextBox.Text = "https://www.example.com";

            // methodComboBox
            methodComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            methodComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            methodComboBox.Items.AddRange(new object[] { "GET", "POST", "PUT" });
            methodComboBox.Location = new System.Drawing.Point(this.Width - 250, 12);
            methodComboBox.Size = new System.Drawing.Size(70, 23);
            methodComboBox.SelectedIndex = 0;

            // sendButton
            sendButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            sendButton.Location = new System.Drawing.Point(this.Width - 175, 12);
            sendButton.Size = new System.Drawing.Size(60, 23);
            sendButton.Text = "Send";

            // formatComboBox
            formatComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            formatComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            formatComboBox.Items.AddRange(new object[] { "Plain Text", "JSON", "HTML" });
            formatComboBox.Location = new System.Drawing.Point(this.Width - 105, 12);
            formatComboBox.Size = new System.Drawing.Size(80, 23);
            formatComboBox.SelectedIndex = 0;

            // headersLabel
            headersLabel.Location = new System.Drawing.Point(12, 45);
            headersLabel.Size = new System.Drawing.Size(52, 15);
            headersLabel.Text = "Headers:";

            // headersTextBox
            headersTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            headersTextBox.Location = new System.Drawing.Point(70, 42);
            headersTextBox.Size = new System.Drawing.Size(this.Width - 82, 23);
            headersTextBox.PlaceholderText = "Key: Value, Key2: Value2";

            // bodyLabel
            bodyLabel.Location = new System.Drawing.Point(12, 75);
            bodyLabel.Size = new System.Drawing.Size(37, 15);
            bodyLabel.Text = "Body:";

            // bodyTextBox
            bodyTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            bodyTextBox.Location = new System.Drawing.Point(70, 72);
            bodyTextBox.Size = new System.Drawing.Size(this.Width - 82, 50);
            bodyTextBox.Multiline = true;

            // responseRichTextBox
            responseRichTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            responseRichTextBox.Location = new System.Drawing.Point(12, 130);
            responseRichTextBox.Size = new System.Drawing.Size(this.Width - 24, this.Height - 142);
            responseRichTextBox.ReadOnly = true;
            responseRichTextBox.Font = new System.Drawing.Font("Consolas", 10);
            responseRichTextBox.BackColor = System.Drawing.Color.White;
            responseRichTextBox.ForeColor = System.Drawing.Color.Black;
            responseRichTextBox.WordWrap = false;

            // Handle resizing to reposition controls
            this.Resize += (s, e) =>
            {
                urlTextBox.Size = new System.Drawing.Size(this.Width - 12 - 12 - 270, 23);
                methodComboBox.Location = new System.Drawing.Point(this.Width - 250, 12);
                sendButton.Location = new System.Drawing.Point(this.Width - 175, 12);
                formatComboBox.Location = new System.Drawing.Point(this.Width - 105, 12);
                headersTextBox.Size = new System.Drawing.Size(this.Width - 82, 23);
                bodyTextBox.Size = new System.Drawing.Size(this.Width - 82, 50);
                responseRichTextBox.Size = new System.Drawing.Size(this.Width - 24, this.Height - 142);
            };

            Controls.Add(urlTextBox);
            Controls.Add(methodComboBox);
            Controls.Add(sendButton);
            Controls.Add(formatComboBox);
            Controls.Add(headersLabel);
            Controls.Add(headersTextBox);
            Controls.Add(bodyLabel);
            Controls.Add(bodyTextBox);
            Controls.Add(responseRichTextBox);
            this.Dock = DockStyle.Fill;
        }
    }
}
