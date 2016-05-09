namespace Microsoft.AzureCat.Samples.DeviceSimulator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.mainHeaderPanel = new Microsoft.AzureCat.Samples.DeviceSimulator.HeaderPanel();
            this.radioButtonHttps = new System.Windows.Forms.RadioButton();
            this.radioButtonAmqp = new System.Windows.Forms.RadioButton();
            this.lblEventIntervalInSeconds = new System.Windows.Forms.Label();
            this.txtEventIntervalInSeconds = new Microsoft.AzureCat.Samples.DeviceSimulator.NumericTextBox();
            this.lblMessageRetentionInDays = new System.Windows.Forms.Label();
            this.txtMessageRetentionInDays = new Microsoft.AzureCat.Samples.DeviceSimulator.NumericTextBox();
            this.lblPartitionCount = new System.Windows.Forms.Label();
            this.txtPartitionCount = new Microsoft.AzureCat.Samples.DeviceSimulator.NumericTextBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblMaxValue = new System.Windows.Forms.Label();
            this.txtMaxValue = new Microsoft.AzureCat.Samples.DeviceSimulator.NumericTextBox();
            this.lblMinValue = new System.Windows.Forms.Label();
            this.txtMinValue = new Microsoft.AzureCat.Samples.DeviceSimulator.NumericTextBox();
            this.lblDeviceCount = new System.Windows.Forms.Label();
            this.txtDeviceCount = new Microsoft.AzureCat.Samples.DeviceSimulator.NumericTextBox();
            this.lblKeyName = new System.Windows.Forms.Label();
            this.txtKeyName = new System.Windows.Forms.TextBox();
            this.lblNamespace = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.lblEventHub = new System.Windows.Forms.Label();
            this.txtEventHub = new System.Windows.Forms.TextBox();
            this.lblKeyValue = new System.Windows.Forms.Label();
            this.txtKeyValue = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.logHeaderPanel = new Microsoft.AzureCat.Samples.DeviceSimulator.HeaderPanel();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnClear = new System.Windows.Forms.Button();
            this.mainMenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.mainHeaderPanel.SuspendLayout();
            this.logHeaderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(848, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogToolStripMenuItem,
            this.saveLogToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // clearLogToolStripMenuItem
            // 
            this.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            this.clearLogToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.clearLogToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.clearLogToolStripMenuItem.Text = "Clear Log";
            this.clearLogToolStripMenuItem.Click += new System.EventHandler(this.clearLogToolStripMenuItem_Click);
            // 
            // saveLogToolStripMenuItem
            // 
            this.saveLogToolStripMenuItem.Name = "saveLogToolStripMenuItem";
            this.saveLogToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveLogToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveLogToolStripMenuItem.Text = "Save Log As...";
            this.saveLogToolStripMenuItem.Click += new System.EventHandler(this.saveLogToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logWindowToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // logWindowToolStripMenuItem
            // 
            this.logWindowToolStripMenuItem.Checked = true;
            this.logWindowToolStripMenuItem.CheckOnClick = true;
            this.logWindowToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.logWindowToolStripMenuItem.Name = "logWindowToolStripMenuItem";
            this.logWindowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.logWindowToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.logWindowToolStripMenuItem.Text = "&Log Window";
            this.logWindowToolStripMenuItem.Click += new System.EventHandler(this.logWindowToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 539);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(848, 22);
            this.statusStrip.TabIndex = 20;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(16, 40);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.mainHeaderPanel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.logHeaderPanel);
            this.splitContainer.Size = new System.Drawing.Size(816, 495);
            this.splitContainer.SplitterDistance = 272;
            this.splitContainer.SplitterWidth = 8;
            this.splitContainer.TabIndex = 21;
            // 
            // mainHeaderPanel
            // 
            this.mainHeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.mainHeaderPanel.Controls.Add(this.btnClear);
            this.mainHeaderPanel.Controls.Add(this.radioButtonHttps);
            this.mainHeaderPanel.Controls.Add(this.radioButtonAmqp);
            this.mainHeaderPanel.Controls.Add(this.lblEventIntervalInSeconds);
            this.mainHeaderPanel.Controls.Add(this.txtEventIntervalInSeconds);
            this.mainHeaderPanel.Controls.Add(this.lblMessageRetentionInDays);
            this.mainHeaderPanel.Controls.Add(this.txtMessageRetentionInDays);
            this.mainHeaderPanel.Controls.Add(this.lblPartitionCount);
            this.mainHeaderPanel.Controls.Add(this.txtPartitionCount);
            this.mainHeaderPanel.Controls.Add(this.lblLocation);
            this.mainHeaderPanel.Controls.Add(this.txtLocation);
            this.mainHeaderPanel.Controls.Add(this.lblMaxValue);
            this.mainHeaderPanel.Controls.Add(this.txtMaxValue);
            this.mainHeaderPanel.Controls.Add(this.lblMinValue);
            this.mainHeaderPanel.Controls.Add(this.txtMinValue);
            this.mainHeaderPanel.Controls.Add(this.lblDeviceCount);
            this.mainHeaderPanel.Controls.Add(this.txtDeviceCount);
            this.mainHeaderPanel.Controls.Add(this.lblKeyName);
            this.mainHeaderPanel.Controls.Add(this.txtKeyName);
            this.mainHeaderPanel.Controls.Add(this.lblNamespace);
            this.mainHeaderPanel.Controls.Add(this.txtNamespace);
            this.mainHeaderPanel.Controls.Add(this.lblEventHub);
            this.mainHeaderPanel.Controls.Add(this.txtEventHub);
            this.mainHeaderPanel.Controls.Add(this.lblKeyValue);
            this.mainHeaderPanel.Controls.Add(this.txtKeyValue);
            this.mainHeaderPanel.Controls.Add(this.btnStart);
            this.mainHeaderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainHeaderPanel.ForeColor = System.Drawing.Color.White;
            this.mainHeaderPanel.HeaderColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(205)))), ((int)(((byte)(219)))));
            this.mainHeaderPanel.HeaderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.mainHeaderPanel.HeaderFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.mainHeaderPanel.HeaderHeight = 24;
            this.mainHeaderPanel.HeaderText = "Event Hub and Device Configuration";
            this.mainHeaderPanel.Icon = global::Microsoft.AzureCat.Samples.DeviceSimulator.Properties.Resources.SmallWorld;
            this.mainHeaderPanel.IconTransparentColor = System.Drawing.Color.White;
            this.mainHeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.mainHeaderPanel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mainHeaderPanel.Name = "mainHeaderPanel";
            this.mainHeaderPanel.Padding = new System.Windows.Forms.Padding(5, 28, 5, 4);
            this.mainHeaderPanel.Size = new System.Drawing.Size(816, 272);
            this.mainHeaderPanel.TabIndex = 0;
            // 
            // radioButtonHttps
            // 
            this.radioButtonHttps.AutoSize = true;
            this.radioButtonHttps.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButtonHttps.Location = new System.Drawing.Point(216, 236);
            this.radioButtonHttps.Name = "radioButtonHttps";
            this.radioButtonHttps.Size = new System.Drawing.Size(61, 17);
            this.radioButtonHttps.TabIndex = 55;
            this.radioButtonHttps.Text = "HTTPS";
            this.radioButtonHttps.UseVisualStyleBackColor = true;
            // 
            // radioButtonAmqp
            // 
            this.radioButtonAmqp.AutoSize = true;
            this.radioButtonAmqp.Checked = true;
            this.radioButtonAmqp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButtonAmqp.Location = new System.Drawing.Point(15, 236);
            this.radioButtonAmqp.Name = "radioButtonAmqp";
            this.radioButtonAmqp.Size = new System.Drawing.Size(56, 17);
            this.radioButtonAmqp.TabIndex = 54;
            this.radioButtonAmqp.TabStop = true;
            this.radioButtonAmqp.Text = "AMQP";
            this.radioButtonAmqp.UseVisualStyleBackColor = true;
            // 
            // lblEventIntervalInSeconds
            // 
            this.lblEventIntervalInSeconds.AutoSize = true;
            this.lblEventIntervalInSeconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEventIntervalInSeconds.Location = new System.Drawing.Point(216, 184);
            this.lblEventIntervalInSeconds.Name = "lblEventIntervalInSeconds";
            this.lblEventIntervalInSeconds.Size = new System.Drawing.Size(133, 13);
            this.lblEventIntervalInSeconds.TabIndex = 53;
            this.lblEventIntervalInSeconds.Text = "Event Interval In Seconds:";
            // 
            // txtEventIntervalInSeconds
            // 
            this.txtEventIntervalInSeconds.AllowSpace = false;
            this.txtEventIntervalInSeconds.Location = new System.Drawing.Point(216, 200);
            this.txtEventIntervalInSeconds.Name = "txtEventIntervalInSeconds";
            this.txtEventIntervalInSeconds.Size = new System.Drawing.Size(184, 20);
            this.txtEventIntervalInSeconds.TabIndex = 8;
            // 
            // lblMessageRetentionInDays
            // 
            this.lblMessageRetentionInDays.AutoSize = true;
            this.lblMessageRetentionInDays.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMessageRetentionInDays.Location = new System.Drawing.Point(216, 136);
            this.lblMessageRetentionInDays.Name = "lblMessageRetentionInDays";
            this.lblMessageRetentionInDays.Size = new System.Drawing.Size(141, 13);
            this.lblMessageRetentionInDays.TabIndex = 51;
            this.lblMessageRetentionInDays.Text = "Message Retention In Days:";
            // 
            // txtMessageRetentionInDays
            // 
            this.txtMessageRetentionInDays.AllowSpace = false;
            this.txtMessageRetentionInDays.Location = new System.Drawing.Point(216, 152);
            this.txtMessageRetentionInDays.Name = "txtMessageRetentionInDays";
            this.txtMessageRetentionInDays.Size = new System.Drawing.Size(184, 20);
            this.txtMessageRetentionInDays.TabIndex = 5;
            // 
            // lblPartitionCount
            // 
            this.lblPartitionCount.AutoSize = true;
            this.lblPartitionCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPartitionCount.Location = new System.Drawing.Point(16, 136);
            this.lblPartitionCount.Name = "lblPartitionCount";
            this.lblPartitionCount.Size = new System.Drawing.Size(77, 13);
            this.lblPartitionCount.TabIndex = 49;
            this.lblPartitionCount.Text = "Partiton Count:";
            // 
            // txtPartitionCount
            // 
            this.txtPartitionCount.AllowSpace = false;
            this.txtPartitionCount.Location = new System.Drawing.Point(16, 152);
            this.txtPartitionCount.Name = "txtPartitionCount";
            this.txtPartitionCount.Size = new System.Drawing.Size(184, 20);
            this.txtPartitionCount.TabIndex = 4;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLocation.Location = new System.Drawing.Point(416, 136);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(88, 13);
            this.lblLocation.TabIndex = 47;
            this.lblLocation.Text = "Device Location:";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(416, 152);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(384, 20);
            this.txtLocation.TabIndex = 6;
            // 
            // lblMaxValue
            // 
            this.lblMaxValue.AutoSize = true;
            this.lblMaxValue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMaxValue.Location = new System.Drawing.Point(616, 184);
            this.lblMaxValue.Name = "lblMaxValue";
            this.lblMaxValue.Size = new System.Drawing.Size(60, 13);
            this.lblMaxValue.TabIndex = 45;
            this.lblMaxValue.Text = "Max Value:";
            // 
            // txtMaxValue
            // 
            this.txtMaxValue.AllowSpace = false;
            this.txtMaxValue.Location = new System.Drawing.Point(616, 200);
            this.txtMaxValue.Name = "txtMaxValue";
            this.txtMaxValue.Size = new System.Drawing.Size(184, 20);
            this.txtMaxValue.TabIndex = 10;
            // 
            // lblMinValue
            // 
            this.lblMinValue.AutoSize = true;
            this.lblMinValue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMinValue.Location = new System.Drawing.Point(416, 184);
            this.lblMinValue.Name = "lblMinValue";
            this.lblMinValue.Size = new System.Drawing.Size(57, 13);
            this.lblMinValue.TabIndex = 43;
            this.lblMinValue.Text = "Min Value:";
            // 
            // txtMinValue
            // 
            this.txtMinValue.AllowSpace = false;
            this.txtMinValue.Location = new System.Drawing.Point(416, 200);
            this.txtMinValue.Name = "txtMinValue";
            this.txtMinValue.Size = new System.Drawing.Size(184, 20);
            this.txtMinValue.TabIndex = 9;
            // 
            // lblDeviceCount
            // 
            this.lblDeviceCount.AutoSize = true;
            this.lblDeviceCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDeviceCount.Location = new System.Drawing.Point(16, 184);
            this.lblDeviceCount.Name = "lblDeviceCount";
            this.lblDeviceCount.Size = new System.Drawing.Size(75, 13);
            this.lblDeviceCount.TabIndex = 41;
            this.lblDeviceCount.Text = "Device Count:";
            // 
            // txtDeviceCount
            // 
            this.txtDeviceCount.AllowSpace = false;
            this.txtDeviceCount.Location = new System.Drawing.Point(16, 200);
            this.txtDeviceCount.Name = "txtDeviceCount";
            this.txtDeviceCount.Size = new System.Drawing.Size(184, 20);
            this.txtDeviceCount.TabIndex = 7;
            // 
            // lblKeyName
            // 
            this.lblKeyName.AutoSize = true;
            this.lblKeyName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblKeyName.Location = new System.Drawing.Point(16, 88);
            this.lblKeyName.Name = "lblKeyName";
            this.lblKeyName.Size = new System.Drawing.Size(59, 13);
            this.lblKeyName.TabIndex = 39;
            this.lblKeyName.Text = "Key Name:";
            // 
            // txtKeyName
            // 
            this.txtKeyName.Location = new System.Drawing.Point(16, 104);
            this.txtKeyName.Name = "txtKeyName";
            this.txtKeyName.Size = new System.Drawing.Size(384, 20);
            this.txtKeyName.TabIndex = 2;
            // 
            // lblNamespace
            // 
            this.lblNamespace.AutoSize = true;
            this.lblNamespace.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNamespace.Location = new System.Drawing.Point(16, 40);
            this.lblNamespace.Name = "lblNamespace";
            this.lblNamespace.Size = new System.Drawing.Size(67, 13);
            this.lblNamespace.TabIndex = 30;
            this.lblNamespace.Text = "Namespace:";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(16, 56);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(384, 20);
            this.txtNamespace.TabIndex = 0;
            // 
            // lblEventHub
            // 
            this.lblEventHub.AutoSize = true;
            this.lblEventHub.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEventHub.Location = new System.Drawing.Point(416, 40);
            this.lblEventHub.Name = "lblEventHub";
            this.lblEventHub.Size = new System.Drawing.Size(61, 13);
            this.lblEventHub.TabIndex = 24;
            this.lblEventHub.Text = "Event Hub:";
            // 
            // txtEventHub
            // 
            this.txtEventHub.Location = new System.Drawing.Point(416, 56);
            this.txtEventHub.Name = "txtEventHub";
            this.txtEventHub.Size = new System.Drawing.Size(384, 20);
            this.txtEventHub.TabIndex = 1;
            // 
            // lblKeyValue
            // 
            this.lblKeyValue.AutoSize = true;
            this.lblKeyValue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblKeyValue.Location = new System.Drawing.Point(416, 88);
            this.lblKeyValue.Name = "lblKeyValue";
            this.lblKeyValue.Size = new System.Drawing.Size(58, 13);
            this.lblKeyValue.TabIndex = 22;
            this.lblKeyValue.Text = "Key Value:";
            // 
            // txtKeyValue
            // 
            this.txtKeyValue.Location = new System.Drawing.Point(416, 104);
            this.txtKeyValue.Name = "txtKeyValue";
            this.txtKeyValue.Size = new System.Drawing.Size(384, 20);
            this.txtKeyValue.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStart.Location = new System.Drawing.Point(712, 232);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(88, 24);
            this.btnStart.TabIndex = 11;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            this.btnStart.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnStart.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // logHeaderPanel
            // 
            this.logHeaderPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.logHeaderPanel.Controls.Add(this.lstLog);
            this.logHeaderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logHeaderPanel.ForeColor = System.Drawing.Color.White;
            this.logHeaderPanel.HeaderColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(205)))), ((int)(((byte)(219)))));
            this.logHeaderPanel.HeaderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.logHeaderPanel.HeaderFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.logHeaderPanel.HeaderHeight = 24;
            this.logHeaderPanel.HeaderText = "Log";
            this.logHeaderPanel.Icon = global::Microsoft.AzureCat.Samples.DeviceSimulator.Properties.Resources.SmallDocument;
            this.logHeaderPanel.IconTransparentColor = System.Drawing.Color.White;
            this.logHeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.logHeaderPanel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.logHeaderPanel.Name = "logHeaderPanel";
            this.logHeaderPanel.Padding = new System.Windows.Forms.Padding(5, 28, 5, 4);
            this.logHeaderPanel.Size = new System.Drawing.Size(816, 215);
            this.logHeaderPanel.TabIndex = 0;
            // 
            // lstLog
            // 
            this.lstLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLog.FormattingEnabled = true;
            this.lstLog.HorizontalScrollbar = true;
            this.lstLog.Location = new System.Drawing.Point(5, 28);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(806, 183);
            this.lstLog.TabIndex = 0;
            this.lstLog.Leave += new System.EventHandler(this.lstLog_Leave);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClear.Location = new System.Drawing.Point(616, 232);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(88, 24);
            this.btnClear.TabIndex = 56;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(848, 561);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Device Simulator";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.mainHeaderPanel.ResumeLayout(false);
            this.mainHeaderPanel.PerformLayout();
            this.logHeaderPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.SplitContainer splitContainer;
        private HeaderPanel logHeaderPanel;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private HeaderPanel mainHeaderPanel;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblEventHub;
        private System.Windows.Forms.TextBox txtEventHub;
        private System.Windows.Forms.Label lblKeyValue;
        private System.Windows.Forms.TextBox txtKeyValue;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label lblNamespace;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label lblKeyName;
        private System.Windows.Forms.TextBox txtKeyName;
        private System.Windows.Forms.Label lblMaxValue;
        private NumericTextBox txtMaxValue;
        private System.Windows.Forms.Label lblMinValue;
        private NumericTextBox txtMinValue;
        private System.Windows.Forms.Label lblDeviceCount;
        private NumericTextBox txtDeviceCount;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblMessageRetentionInDays;
        private NumericTextBox txtMessageRetentionInDays;
        private System.Windows.Forms.Label lblPartitionCount;
        private NumericTextBox txtPartitionCount;
        private System.Windows.Forms.Label lblEventIntervalInSeconds;
        private NumericTextBox txtEventIntervalInSeconds;
        private System.Windows.Forms.RadioButton radioButtonHttps;
        private System.Windows.Forms.RadioButton radioButtonAmqp;
        private System.Windows.Forms.Button btnClear;
    }
}