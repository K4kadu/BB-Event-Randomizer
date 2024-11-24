using System.Text.RegularExpressions;

namespace EventRandomizer
{
    public class MainForm : Form
    {
        public string eventOutput = "";
        private System.Windows.Forms.Label eventLabel;
        private System.Windows.Forms.Label propertyLabel;
        private System.Windows.Forms.Label minValueLabel;
        private System.Windows.Forms.Label maxValueLabel;
        private System.Windows.Forms.Label roundValueLabel;
        private System.Windows.Forms.Label addMinimumLabel;
        private System.Windows.Forms.CheckBox addMinimumCheck;
        private System.Windows.Forms.TextBox eventInput;
        private System.Windows.Forms.ComboBox propertyBox;
        private System.Windows.Forms.TextBox minValueBox;
        private System.Windows.Forms.TextBox maxValueBox;
        private System.Windows.Forms.TextBox roundValueBox;
        private System.Windows.Forms.Label stepSizeLabel;
        private System.Windows.Forms.TextBox stepSize;
        private System.Windows.Forms.Label stepSizeLabelToo;
        private System.Windows.Forms.Label startingBeatLabel;
        private System.Windows.Forms.TextBox startingBeat;
        private System.Windows.Forms.Label endingBeatLabel;
        private System.Windows.Forms.TextBox endingBeat;
        private System.Windows.Forms.Button randomizeButton;
        private System.Windows.Forms.TextBox outputBox;
        private System.Windows.Forms.Button openOutput;
        private System.Windows.Forms.Button saveAsTag;
        private System.Windows.Forms.Button copyOutput;
        private System.Windows.Forms.Label acrossTimeLabel;
        private System.Windows.Forms.CheckBox acrossTime;

        public MainForm()
        {
            InitializeComponents();
        }

        public void outBox(string input)
        {
            if (outputBox.Text.Equals(input))
                outputBox.Text = " " + input;
            else
                outputBox.Text = input;
        }

        private void InitializeComponents()
        {

            eventLabel = new System.Windows.Forms.Label
            {
                Text = "Paste the event that you want to be randomized, starting with { and ending with }.",
                Top = 10,
                Left = 20,
                AutoSize = true,
            };

            // EVENT INPUT
            eventInput = new System.Windows.Forms.TextBox
            {
                Multiline = true,
                Width = 600,
                Height = 40,
                Top = 32,
                Left = 20
            };

            List<string> foundProperties = new List<string> { "time", "angle", "order" };

            eventInput.TextChanged += (sender, e) =>
            {
                System.Windows.Forms.TextBox eventInput = sender as System.Windows.Forms.TextBox; // Cast the sender to a TextBox
                string text = eventInput.Text;

                // Clear the list to update with new found properties
                foundProperties.Clear();

                // Use a regular expression to find words in quotation marks
                var matches = System.Text.RegularExpressions.Regex.Matches(text, "\"([^\"]+?)\":");

                foreach (var match in matches)
                {
                    foundProperties.Add(match.ToString().Trim(':').Trim('"')); // Remove quotation marks and add to the list
                }

                // Populate property box with elements from the list
                propertyBox.Items.Clear();
                propertyBox.Items.AddRange(foundProperties.ToArray());
                // propertyBox.Items.Remove("time");

                if (propertyBox.Items.Count == 0)
                {
                    propertyBox.Items.Add("no properties found");
                    propertyBox.SelectedIndex = 0;
                }
            };

            propertyLabel = new System.Windows.Forms.Label
            {
                Text = "randomized property:",
                Top = 80,
                Left = 20,
                AutoSize = true,
            };

            // PROPERTY BOX
            propertyBox = new System.Windows.Forms.ComboBox
            {
                Width = 140,
                Top = 80,
                Left = 160,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Items = { "no properties found" },
                SelectedIndex = 0
            };

            propertyBox.SelectedIndexChanged += (sender, e) =>
            {
                if (propertyBox.SelectedItem != null && propertyBox.SelectedItem.ToString() == "time")
                    acrossTime.Checked = false;
            };

            minValueLabel = new System.Windows.Forms.Label
            {
                Text = "minimum value:",
                Top = 120,
                Left = 20,
                AutoSize = true,
            };

            minValueBox = new System.Windows.Forms.TextBox
            {
                Text = "0",
                Width = 80,
                Height = 20,
                Top = 120,
                Left = 130
            };

            minValueBox.KeyPress += (sender, e) =>
            {
                char keyChar = e.KeyChar;

                // Allow control keys (e.g., Backspace) and one dot
                if (char.IsControl(keyChar) || keyChar == '.' && !((System.Windows.Forms.TextBox)sender).Text.Contains('.'))
                {
                    return; // Allow the key
                }

                // Allow digits
                if (!char.IsDigit(keyChar))
                {
                    e.Handled = true; // Block the key
                }
            };

            maxValueLabel = new System.Windows.Forms.Label
            {
                Text = "maximum value:",
                Top = 150,
                Left = 20,
                AutoSize = true,
            };

            maxValueBox = new System.Windows.Forms.TextBox
            {
                Text = "0",
                Width = 80,
                Height = 20,
                Top = 150,
                Left = 130
            };

            maxValueBox.KeyPress += (sender, e) =>
            {
                char keyChar = e.KeyChar;

                // Allow control keys (e.g., Backspace) and one dot
                if (char.IsControl(keyChar) || keyChar == '.' && !((System.Windows.Forms.TextBox)sender).Text.Contains('.'))
                {
                    return; // Allow the key
                }

                // Allow digits
                if (!char.IsDigit(keyChar))
                {
                    e.Handled = true; // Block the key
                }
            };

            roundValueLabel = new System.Windows.Forms.Label
            {
                Text = "rounded to\na multiple of:",
                Top = 180,
                Left = 20,
                AutoSize = true,
            };

            roundValueBox = new System.Windows.Forms.TextBox
            {
                Text = "1",
                Width = 80,
                Height = 20,
                Top = 185,
                Left = 130
            };

            roundValueBox.KeyPress += (sender, e) =>
            {
                char keyChar = e.KeyChar;

                // Allow control keys (e.g., Backspace) and one dot
                if (char.IsControl(keyChar) || keyChar == '.' && !((System.Windows.Forms.TextBox)sender).Text.Contains('.'))
                {
                    return; // Allow the key
                }

                // Allow digits
                if (!char.IsDigit(keyChar))
                {
                    e.Handled = true; // Block the key
                }
            };

            addMinimumLabel = new System.Windows.Forms.Label
            {
                Text = "add minimum value to each result",
                Top = 220,
                Left = 20,
                AutoSize = true,
            };

            addMinimumLabel.Click += (sender, e) =>
             {
                 addMinimumCheck.Checked = !addMinimumCheck.Checked;
             };

            addMinimumCheck = new CheckBox
            {
                Top = 220,
                Left = 210,
                Width = 20,
                Height = 20,
                Checked = true
            };

            acrossTimeLabel = new System.Windows.Forms.Label
            {
                Text = "repeat with different timings",
                Top = 80,
                Left = 380,
                AutoSize = true,
            };

            acrossTimeLabel.Click += (sender, e) =>
            {
                acrossTime.Checked = !acrossTime.Checked;
            };

            acrossTime = new CheckBox
            {
                Width = 20,
                Top = 78,
                Left = 360,
                Checked = true
            };

            acrossTime.CheckedChanged += (sender, e) =>
            {
                if (acrossTime.Checked)
                {
                    if (propertyBox.SelectedItem != null && propertyBox.SelectedItem.ToString() == "time")
                        propertyBox.SelectedItem = null;

                    stepSize.Show();
                    stepSizeLabel.Show();
                    stepSizeLabelToo.Show();
                    startingBeatLabel.Show();
                    startingBeat.Show();
                    endingBeatLabel.Show();
                    endingBeat.Show();
                }
                else
                {
                    stepSize.Hide();
                    stepSizeLabel.Hide();
                    stepSizeLabelToo.Hide();
                    startingBeatLabel.Hide();
                    startingBeat.Hide();
                    endingBeatLabel.Hide();
                    endingBeat.Hide();
                }
            };

            stepSizeLabel = new System.Windows.Forms.Label
            {
                Text = "Place an event every",
                Top = 120,
                Left = 360,
                AutoSize = true,
            };

            stepSize = new System.Windows.Forms.TextBox
            {
                Text = "1",
                Width = 40,
                Height = 20,
                Top = 120,
                Left = 476
            };

            stepSize.KeyPress += (sender, e) =>
            {
                char keyChar = e.KeyChar;

                // Allow control keys (e.g., Backspace) and one dot
                if (char.IsControl(keyChar) || keyChar == '.' && !((System.Windows.Forms.TextBox)sender).Text.Contains('.'))
                {
                    return; // Allow the key
                }

                // Allow digits
                if (!char.IsDigit(keyChar))
                {
                    e.Handled = true; // Block the key
                }
            };

            stepSizeLabelToo = new System.Windows.Forms.Label
            {
                Text = "beats,",
                Top = 120,
                Left = 518,
                AutoSize = true,
            };

            startingBeatLabel = new System.Windows.Forms.Label
            {
                Text = "from beat",
                Top = 150,
                Left = 360,
                AutoSize = true,
            };

            startingBeat = new System.Windows.Forms.TextBox
            {
                Text = "0",
                Width = 40,
                Height = 20,
                Top = 150,
                Left = 425
            };

            startingBeat.KeyPress += (sender, e) =>
            {
                char keyChar = e.KeyChar;

                // Allow control keys (e.g., Backspace) and one dot
                if (char.IsControl(keyChar) || keyChar == '.' && !((System.Windows.Forms.TextBox)sender).Text.Contains('.'))
                {
                    return; // Allow the key
                }

                // Allow digits
                if (!char.IsDigit(keyChar))
                {
                    e.Handled = true; // Block the key
                }
            };

            endingBeatLabel = new System.Windows.Forms.Label
            {
                Text = "until beat",
                Top = 180,
                Left = 360,
                AutoSize = true,
            };

            endingBeat = new System.Windows.Forms.TextBox
            {
                Text = "0",
                Width = 40,
                Height = 20,
                Top = 180,
                Left = 425
            };

            endingBeat.KeyPress += (sender, e) =>
            {
                char keyChar = e.KeyChar;

                // Allow control keys (e.g., Backspace) and one dot
                if (char.IsControl(keyChar) || keyChar == '.' && !((System.Windows.Forms.TextBox)sender).Text.Contains('.'))
                {
                    return; // Allow the key
                }

                // Allow digits
                if (!char.IsDigit(keyChar))
                {
                    e.Handled = true; // Block the key
                }
            };

            // RANDOMIZER BUTTON
            randomizeButton = new System.Windows.Forms.Button
            {
                Text = "Randomize",
                Top = 250,
                Left = 20,
                Width = 100
            };

            randomizeButton.Click += (sender, e) =>
            {
                if (propertyBox.Items.Count == 0)
                { outBox("Please provide an event."); return; }

                if (propertyBox.SelectedItem == null)
                { outBox("Please select a property."); return; }

                if (propertyBox.SelectedItem.Equals("no properties found"))
                { outBox("Please provide an event."); return; }

                if (double.Parse(minValueBox.Text.Replace('.', ',')) > double.Parse(maxValueBox.Text.Replace('.', ',')))
                { outBox("Make sure that the minimum value is not greater than the maximum value."); return; }

                eventOutput = "";
                Random random = new Random();

                // remove start and end commas to make the tag syntax work
                string inputEvent = eventInput.Text;
                if (inputEvent[0].Equals(','))
                    inputEvent = inputEvent.Substring(1);
                if (inputEvent[inputEvent.Length - 1].Equals(','))
                    inputEvent = inputEvent.Substring(0, inputEvent.Length - 1);

                double counter = double.Parse(startingBeat.Text.Replace('.', ','));
                double threshold = double.Parse(endingBeat.Text.Replace('.', ','));
                string replaced = "";
                for (; counter <= threshold; counter = counter + double.Parse(stepSize.Text.Replace('.', ',')))
                {
                    try
                    {
                        // Parse min, max, and round values from input
                        double minValue = double.Parse(minValueBox.Text.Replace('.', ','));
                        double maxValue = double.Parse(maxValueBox.Text.Replace('.', ','));
                        double valueRound = double.Parse(roundValueBox.Text.Replace('.', ','));

                        double randomValue = maxValue + 1;
                        while (randomValue > maxValue || randomValue < minValue)
                        {
                            // Generate a random value between minValue and maxValue
                            randomValue = minValue + (random.NextDouble() * (maxValue - minValue));

                            // Round to the nearest multiple of valueRound
                            if (valueRound != 0)
                                randomValue = Math.Round(randomValue / valueRound) * valueRound;

                            if (addMinimumCheck.Checked)
                                randomValue = randomValue + minValue;
                        }

                        try
                        {
                            // Regex pattern to find "property" and its value
                            string pattern = @$"\""{propertyBox.Text}\"":-?[0-9]*\.?[0-9]+"; // Matches "property": followed by a number
                            // Replacement string with the new value
                            string replacement = $"\"{propertyBox.Text}\":{randomValue.ToString().Replace(',', '.')}";
                            // Replace the "property" value in the input string
                            replaced = Regex.Replace(inputEvent, pattern, replacement) + ",\n";

                            // Regex pattern to find "time" and its value
                            pattern = @"\""time\"":-?[0-9]*\.?[0-9]+"; // Matches "time": followed by a number
                            // Replacement string with the new value
                            replacement = $"\"time\":{counter.ToString().Replace(',', '.')}";
                            // Replace the "time" value in the input string
                            eventOutput = eventOutput + Regex.Replace(replaced, pattern, replacement);
                        }
                        catch
                        {
                            outBox("Something went wrong.");
                        }

                        outBox("all done. :)");
                    }
                    catch
                    {
                        outBox("Please make sure min value and max value are set to a number.");
                    }
                }
            };

            // OUTPUT BOX
            outputBox = new System.Windows.Forms.TextBox
            {
                Text = "no output yet",
                Top = 250,
                Left = 140,
                Width = 460,
                ReadOnly = true
            };

            openOutput = new System.Windows.Forms.Button
            {
                Text = "view output events",
                Top = 290,
                Left = 20,
                Width = 130,
                Height = 30
            };

            openOutput.Click += (sender, e) =>
            {
                // Create a new Form
                Form outputForm = new Form();
                outputForm.Text = "Event Output";
                outputForm.Size = new Size(800, 600); // Set the size of the new window

                // Create a TextBox to display the eventOutput string
                System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
                textBox.Multiline = true; // Enable multiline to display large content
                textBox.ScrollBars = ScrollBars.Both; // Add both horizontal and vertical scrollbars
                textBox.WordWrap = false; // Disable word wrap
                textBox.Dock = DockStyle.Fill; // Fill the entire form with the TextBox
                textBox.ReadOnly = true; // Make the TextBox read-only

                // Set the text to eventOutput, minus commas
                textBox.Text = eventOutput.Replace("\n", "\r\n"); ;

                // Add the TextBox to the new Form
                outputForm.Controls.Add(textBox);

                // Show the new Form
                outputForm.Show();
            };

            copyOutput = new System.Windows.Forms.Button
            {
                Text = "copy to clipboard",
                Top = 290,
                Left = 160,
                Width = 130,
                Height = 30
            };

            copyOutput.Click += (sender, e) =>
            {
                Clipboard.SetText(eventOutput);
            };

            saveAsTag = new System.Windows.Forms.Button
            {
                Text = "save as tag",
                Top = 290,
                Left = 300,
                Width = 130,
                Height = 30
            };

            saveAsTag.Click += (sender, e) =>
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"; // Filter for .json files
                    saveFileDialog.Title = "Save As Tag";
                    saveFileDialog.DefaultExt = "json"; // Default file extension
                    saveFileDialog.FileName = "myTag.json"; // Default file name

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        File.WriteAllText(filePath, "[\n" + eventOutput + "]");
                        outBox($"File saved successfully to: {filePath}");
                    }
                }
            };

            // Add controls to the form
            Controls.Add(eventLabel);
            Controls.Add(eventInput);

            Controls.Add(propertyLabel);
            Controls.Add(propertyBox);

            Controls.Add(minValueLabel);
            Controls.Add(minValueBox);
            Controls.Add(maxValueLabel);
            Controls.Add(maxValueBox);
            Controls.Add(roundValueLabel);
            Controls.Add(roundValueBox);
            Controls.Add(addMinimumLabel);
            Controls.Add(addMinimumCheck);

            Controls.Add(acrossTime);
            Controls.Add(acrossTimeLabel);
            Controls.Add(stepSizeLabel);
            Controls.Add(stepSize);
            Controls.Add(stepSizeLabelToo);
            Controls.Add(startingBeatLabel);
            Controls.Add(startingBeat);
            Controls.Add(endingBeatLabel);
            Controls.Add(endingBeat);

            Controls.Add(randomizeButton);
            Controls.Add(outputBox);
            Controls.Add(openOutput);
            Controls.Add(copyOutput);
            Controls.Add(saveAsTag);

            // Set form properties
            Text = "Event Randomizer";
            Width = 660;
            Height = 380;
        }

        private void RandomizeButton_Click(object sender, EventArgs e)
        {
            Randomizer(eventInput.Text);
        }

        private void Randomizer(string input)
        {
            // TODO: Implement randomization logic here
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
