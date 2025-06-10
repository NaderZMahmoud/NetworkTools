namespace NetworkTool;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        // Use method group conversion to avoid nullability mismatch
        httpCheckerControl.sendButton.Click += sendButton_Click!;
    }

    private void FormatAndDisplayResponse(string content)
    {
        var selectedFormat = httpCheckerControl.formatComboBox.SelectedItem?.ToString();
        httpCheckerControl.responseRichTextBox.Clear();
        if (selectedFormat == "JSON")
        {
            try
            {
                var parsedJson = System.Text.Json.JsonDocument.Parse(content);
                string prettyJson = System.Text.Json.JsonSerializer.Serialize(parsedJson, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                httpCheckerControl.responseRichTextBox.Text = prettyJson;
                HighlightJson(prettyJson);
            }
            catch
            {
                httpCheckerControl.responseRichTextBox.Text = "Invalid JSON or not JSON response.";
            }
        }
        else if (selectedFormat == "HTML")
        {
            try
            {
                var doc = new System.Xml.XmlDocument();
                doc.LoadXml(content);
                using var stringWriter = new System.IO.StringWriter();
                using var xmlTextWriter = new System.Xml.XmlTextWriter(stringWriter) { Formatting = System.Xml.Formatting.Indented };
                doc.WriteContentTo(xmlTextWriter);
                xmlTextWriter.Flush();
                string prettyHtml = stringWriter.GetStringBuilder().ToString();
                httpCheckerControl.responseRichTextBox.Text = prettyHtml;
                HighlightHtml(prettyHtml);
            }
            catch
            {
                httpCheckerControl.responseRichTextBox.Text = content;
            }
        }
        else
        {
            httpCheckerControl.responseRichTextBox.Text = content;
        }
    }

    private void HighlightJson(string json)
    {
        var box = httpCheckerControl.responseRichTextBox;
        box.SelectAll();
        box.SelectionColor = System.Drawing.Color.Black;
        int idx = 0;
        while (idx < json.Length)
        {
            if (json[idx] == '"')
            {
                int start = idx;
                idx++;
                while (idx < json.Length && json[idx] != '"') idx++;
                idx++;
                box.Select(start, idx - start);
                box.SelectionColor = System.Drawing.Color.Brown;
            }
            else if (json[idx] == ':' || json[idx] == ',' || json[idx] == '{' || json[idx] == '}' || json[idx] == '[' || json[idx] == ']')
            {
                box.Select(idx, 1);
                box.SelectionColor = System.Drawing.Color.Blue;
                idx++;
            }
            else
            {
                idx++;
            }
        }
        box.Select(0, 0);
    }

    private void HighlightHtml(string html)
    {
        var box = httpCheckerControl.responseRichTextBox;
        box.SelectAll();
        box.SelectionColor = System.Drawing.Color.Black;
        int idx = 0;
        while (idx < html.Length)
        {
            if (html[idx] == '<')
            {
                int start = idx;
                while (idx < html.Length && html[idx] != '>') idx++;
                idx++;
                box.Select(start, idx - start);
                box.SelectionColor = System.Drawing.Color.DarkBlue;
            }
            else
            {
                idx++;
            }
        }
        box.Select(0, 0);
    }

    private async void sendButton_Click(object sender, EventArgs e)
    {
        string url = httpCheckerControl.urlTextBox.Text;
        string method = httpCheckerControl.methodComboBox.SelectedItem?.ToString() ?? "GET";
        string headersRaw = httpCheckerControl.headersTextBox.Text;
        string body = httpCheckerControl.bodyTextBox.Text;
        using var httpClient = new HttpClient();
        try
        {
            httpCheckerControl.responseRichTextBox.Text = "Sending request...";
            var request = new HttpRequestMessage(new HttpMethod(method), url);
            // Add headers
            if (!string.IsNullOrWhiteSpace(headersRaw))
            {
                var headerPairs = headersRaw.Split(',');
                foreach (var pair in headerPairs)
                {
                    var kv = pair.Split(':', 2);
                    if (kv.Length == 2)
                    {
                        var key = kv[0].Trim();
                        var value = kv[1].Trim();
                        if (!string.IsNullOrEmpty(key))
                        {
                            // Some headers can't be set directly (like Content-Type for content)
                            if (!request.Headers.TryAddWithoutValidation(key, value))
                            {
                                if (request.Content == null)
                                    request.Content = new StringContent("");
                                request.Content.Headers.TryAddWithoutValidation(key, value);
                            }
                        }
                    }
                }
            }
            // Add body for POST/PUT
            if ((method == "POST" || method == "PUT") && !string.IsNullOrEmpty(body))
            {
                request.Content = new StringContent(body, System.Text.Encoding.UTF8);
            }
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            FormatAndDisplayResponse(content);
        }
        catch (Exception ex)
        {
            httpCheckerControl.responseRichTextBox.Text = $"Error: {ex.Message}";
        }
    }

    private void httpCheckerMenuItem_Click(object sender, EventArgs e)
    {
        httpCheckerControl.Visible = true;
        endpointCheckerControl.Visible = false;
    }

    private void endpointCheckerMenuItem_Click(object sender, EventArgs e)
    {
        httpCheckerControl.Visible = false;
        endpointCheckerControl.Visible = true;
    }
}
