#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright © 2016 Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. YOU BEAR THE RISK OF USING IT.
//=======================================================================================
#endregion

#region Using Directives
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AzureCat.Samples.Entities;
#endregion

namespace Microsoft.AzureCat.Samples.DeviceSimulator
{
    public partial class MainForm : Form
    {
        #region Private Constants
        //***************************
        // Formats
        //***************************
        private const string DateFormat = "<{0,2:00}:{1,2:00}:{2,2:00}> {3}";
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";
        private const string LogFileNameFormat = "WADTablesCleaner {0}.txt";

        //***************************
        // Constants
        //***************************
        private const string SaveAsTitle = "Save Log As";
        private const string SaveAsExtension = "txt";
        private const string SaveAsFilter = "Text Documents (*.txt)|*.txt";
        private const string Start = "Start";
        private const string Stop = "Stop";
        private const string SenderSharedAccessKey = "SenderSharedAccessKey";
        private const string DeviceId = "id";
        private const string DeviceName = "name";
        private const string DeviceLocation = "location";
        private const string Value = "value";

        //***************************
        // Configuration Parameters
        //***************************
        private const string NamespaceParameter = "namespace";
        private const string KeyNameParameter = "keyName";
        private const string KeyValueParameter = "keyValue";
        private const string EventHubParameter = "eventHub";
        private const string LocationParameter = "location";
        private const string PartitionCountParameter = "partitionCount";
        private const string RetentionDaysParameter = "retentionDays";
        private const string DeviceCountParameter = "deviceCount";
        private const string EventIntervalParameter = "eventInterval";
        private const string MinValueParameter = "minValue";
        private const string MaxValueParameter = "maxValue";
        private const string ApiVersion = "&api-version=2014-05";

        //***************************
        // Configuration Parameters
        //***************************
        private const string DefaultEventHubName = "SampleEventHub";
        private const int DefaultDeviceNumber = 10;
        private const int DefaultMinValue = 20;
        private const int DefaultMaxValue = 50;
        private const int DefaultEventIntervalInSeconds = 1;


        //***************************
        // Messages
        //***************************
        private const string NamespaceCannonBeNull = "The Service Bus namespace cannot be null.";
        private const string EventHubNameCannonBeNull = "The event hub name cannot be null.";
        private const string KeyNameCannonBeNull = "The senderKey name cannot be null.";
        private const string KeyValueCannonBeNull = "The senderKey value cannot be null.";
        private const string EventHubCreatedOrRetrieved = "Event hub [{0}] created or retrieved.";
        private const string MessagingFactoryCreated = "Device[{0,3:000}]. MessagingFactory created.";
        private const string SasToken = "Device[{0,3:000}]. SAS Token created.";
        private const string EventHubClientCreated = "Device[{0,3:000}]. EventHubClient created: Path=[{1}].";
        private const string HttpClientCreated = "Device[{0,3:000}]. HttpClient created: BaseAddress=[{1}].";
        private const string EventSent = "Device[{0,3:000}]. Message sent. PartitionKey=[{1}] Value=[{2}]";
        private const string SendFailed = "Device[{0,3:000}]. Message send failed: [{1}]";
        #endregion

        #region Private Fields
        private CancellationTokenSource cancellationTokenSource;
        private int eventId;
        #endregion

        #region Public Constructor
        /// <summary>
        /// Initializes a new instance of the MainForm class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            ConfigureComponent();
            ReadConfiguration();
        }
        #endregion

        #region Public Methods

        public void ConfigureComponent()
        {
            txtNamespace.AutoSize = false;
            txtNamespace.Size = new Size(txtNamespace.Size.Width, 24);
            txtKeyName.AutoSize = false;
            txtKeyName.Size = new Size(txtKeyName.Size.Width, 24);
            txtKeyValue.AutoSize = false;
            txtKeyValue.Size = new Size(txtKeyValue.Size.Width, 24);
            txtEventHub.AutoSize = false;
            txtEventHub.Size = new Size(txtEventHub.Size.Width, 24);
        }

        public void HandleException(Exception ex)
        {
            if (ex == null || string.IsNullOrEmpty(ex.Message))
            {
                return;
            }
            WriteToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex.Message));
            if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
            {
                WriteToLog(string.Format(CultureInfo.CurrentCulture, InnerExceptionFormat, ex.InnerException.Message));
            }
        }
        #endregion

        #region Private Methods
        public static bool IsJson(string item)
        {
            if (item == null)
            {
                throw new ArgumentException("The item argument cannot be null.");
            }
            try
            {
                var obj = JToken.Parse(item);
                return obj != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string IndentJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }

        private void ReadConfiguration()
        {
            try
            {
                txtNamespace.Text = ConfigurationManager.AppSettings[NamespaceParameter];
                txtKeyName.Text = ConfigurationManager.AppSettings[KeyNameParameter];
                txtKeyValue.Text = ConfigurationManager.AppSettings[KeyValueParameter];
                txtEventHub.Text = ConfigurationManager.AppSettings[EventHubParameter] ?? DefaultEventHubName;
                var eventHubDescription = new EventHubDescription(txtEventHub.Text);
                int value;
                var setting = ConfigurationManager.AppSettings[PartitionCountParameter];
                txtPartitionCount.Text = int.TryParse(setting, out value) ?
                                       value.ToString(CultureInfo.InvariantCulture) :
                                       eventHubDescription.PartitionCount.ToString(CultureInfo.InvariantCulture);
                setting = ConfigurationManager.AppSettings[RetentionDaysParameter];
                txtMessageRetentionInDays.Text = int.TryParse(setting, out value) ?
                                       value.ToString(CultureInfo.InvariantCulture) :
                                       eventHubDescription.MessageRetentionInDays.ToString(CultureInfo.InvariantCulture);
                txtLocation.Text = ConfigurationManager.AppSettings[LocationParameter];
                setting = ConfigurationManager.AppSettings[DeviceCountParameter];
                txtDeviceCount.Text = int.TryParse(setting, out value) ? 
                                       value.ToString(CultureInfo.InvariantCulture) : 
                                       DefaultDeviceNumber.ToString(CultureInfo.InvariantCulture);
                setting = ConfigurationManager.AppSettings[EventIntervalParameter];
                txtEventIntervalInSeconds.Text = int.TryParse(setting, out value) ?
                                       value.ToString(CultureInfo.InvariantCulture) :
                                       DefaultEventIntervalInSeconds.ToString(CultureInfo.InvariantCulture);
                setting = ConfigurationManager.AppSettings[MinValueParameter];
                txtMinValue.Text = int.TryParse(setting, out value) ?
                                       value.ToString(CultureInfo.InvariantCulture) :
                                       DefaultMinValue.ToString(CultureInfo.InvariantCulture);
                setting = ConfigurationManager.AppSettings[MaxValueParameter];
                txtMaxValue.Text = int.TryParse(setting, out value) ?
                                       value.ToString(CultureInfo.InvariantCulture) :
                                       DefaultMaxValue.ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void WriteToLog(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(InternalWriteToLog), new object[] { message });
            }
            else
            {
                InternalWriteToLog(message);
            }
        }

        private void InternalWriteToLog(string message)
        {
            lock (this)
            {
                if (string.IsNullOrEmpty(message))
                {
                    return;
                }
                var lines = message.Split('\n');
                var now = DateTime.Now;
                var space = new string(' ', 19);

                for (var i = 0; i < lines.Length; i++)
                {
                    if (i == 0)
                    {
                        var line = string.Format(DateFormat,
                                                 now.Hour,
                                                 now.Minute,
                                                 now.Second,
                                                 lines[i]);
                        lstLog.Items.Add(line);
                    }
                    else
                    {
                        lstLog.Items.Add(space + lines[i]);
                    }
                }
                lstLog.SelectedIndex = lstLog.Items.Count - 1;
                lstLog.SelectedIndex = -1;
            }
        }

        #endregion

        #region Event Handlers

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
        }

        /// <summary>
        /// Saves the log to a text file
        /// </summary>
        /// <param name="sender">MainForm object</param>
        /// <param name="e">System.EventArgs parameter</param>
        private void saveLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstLog.Items.Count <= 0)
                {
                    return;
                }
                saveFileDialog.Title = SaveAsTitle;
                saveFileDialog.DefaultExt = SaveAsExtension;
                saveFileDialog.Filter = SaveAsFilter;
                saveFileDialog.FileName = string.Format(LogFileNameFormat, DateTime.Now.ToString(CultureInfo.CurrentUICulture).Replace('/', '-').Replace(':', '-'));
                if (saveFileDialog.ShowDialog() != DialogResult.OK || 
                    string.IsNullOrEmpty(saveFileDialog.FileName))
                {
                    return;
                }
                using (var writer = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (var t in lstLog.Items)
                    {
                        writer.WriteLine(t as string);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void logWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer.Panel2Collapsed = !((ToolStripMenuItem)sender).Checked;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AboutForm();
            form.ShowDialog();
        }

        private void lstLog_Leave(object sender, EventArgs e)
        {
            lstLog.SelectedIndex = -1;
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null)
            {
                control.ForeColor = Color.White;
            }
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null)
            {
                control.ForeColor = SystemColors.ControlText;
            }
        }
        
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            var width = (mainHeaderPanel.Size.Width - 48)/2;
            var halfWidth = (width - 16)/2;

            txtNamespace.Size = new Size(width, txtNamespace.Size.Height);
            txtKeyName.Size = new Size(width, txtKeyName.Size.Height);
            txtKeyValue.Size = new Size(width, txtKeyValue.Size.Height);
            txtEventHub.Size = new Size(width, txtEventHub.Size.Height);
            txtLocation.Size = new Size(width, txtLocation.Size.Height);
            txtPartitionCount.Size = new Size(halfWidth, txtPartitionCount.Size.Height);
            txtMessageRetentionInDays.Size = new Size(halfWidth, txtMessageRetentionInDays.Size.Height);
            txtDeviceCount.Size = new Size(halfWidth, txtDeviceCount.Size.Height);
            txtEventIntervalInSeconds.Size = new Size(halfWidth, txtEventIntervalInSeconds.Size.Height);
            txtMinValue.Size = new Size(halfWidth, txtMinValue.Size.Height);
            txtMinValue.Size = new Size(halfWidth, txtMinValue.Size.Height);

            txtEventHub.Location = new Point(32 + width, txtEventHub.Location.Y);
            txtKeyValue.Location = new Point(32 + width, txtKeyValue.Location.Y);
            txtLocation.Location = new Point(32 + width, txtLocation.Location.Y);
            txtMessageRetentionInDays.Location = new Point(32 + halfWidth, txtMessageRetentionInDays.Location.Y);
            txtEventIntervalInSeconds.Location = new Point(32 + halfWidth, txtEventIntervalInSeconds.Location.Y);
            txtMinValue.Location = new Point(32 + width, txtMinValue.Location.Y);
            txtMaxValue.Location = new Point(48 + width + halfWidth, txtMaxValue.Location.Y);

            lblEventHub.Location = new Point(32 + width, lblEventHub.Location.Y);
            lblKeyValue.Location = new Point(32 + width, lblKeyValue.Location.Y);
            lblLocation.Location = new Point(32 + width, lblLocation.Location.Y);
            lblMessageRetentionInDays.Location = new Point(32 + halfWidth, lblMessageRetentionInDays.Location.Y);
            lblEventIntervalInSeconds.Location = new Point(32 + halfWidth, lblEventIntervalInSeconds.Location.Y);
            lblMinValue.Location = new Point(32 + width, lblMinValue.Location.Y);
            lblMaxValue.Location = new Point(48 + width + halfWidth, lblMaxValue.Location.Y);
            radioButtonHttps.Location = new Point(32 + halfWidth, radioButtonAmqp.Location.Y);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            txtNamespace.SelectionLength = 0;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.Compare(btnStart.Text, Start, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // Change button text
                    btnStart.Text = Stop;

                    // Validate parameters
                    if (!ValidateParameters())
                    {
                        return;
                    }

                    // Create namespace manager
                    var namespaceUri = ServiceBusEnvironment.CreateServiceUri("sb", txtNamespace.Text, string.Empty);
                    var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(txtKeyName.Text, txtKeyValue.Text);
                    var namespaceManager = new NamespaceManager(namespaceUri, tokenProvider);

                    // Check if the event hub already exists, if not, create the event hub.
                    var eventHubDescription = await namespaceManager.EventHubExistsAsync(txtEventHub.Text) ?
                                              await namespaceManager.GetEventHubAsync(txtEventHub.Text) :
                                              await namespaceManager.CreateEventHubAsync(new EventHubDescription(txtEventHub.Text)
                                              {
                                                  PartitionCount = txtPartitionCount.IntegerValue,
                                                  MessageRetentionInDays = txtMessageRetentionInDays.IntegerValue
                                              });
                    WriteToLog(string.Format(EventHubCreatedOrRetrieved, txtEventHub.Text));

                    // Check if the SAS authorization rule used by devices to send events to the event hub already exists, if not, create the rule.
                    var authorizationRule = eventHubDescription.
                                            Authorization.
                                            FirstOrDefault(r => string.Compare(r.KeyName, 
                                                                                SenderSharedAccessKey, 
                                                                                StringComparison.InvariantCultureIgnoreCase) 
                                                                                == 0) as SharedAccessAuthorizationRule;
                    if (authorizationRule == null)
                    {
                        authorizationRule = new SharedAccessAuthorizationRule(SenderSharedAccessKey, 
                                                                                 SharedAccessAuthorizationRule.GenerateRandomKey(), 
                                                                                 new[]
                                                                                 {
                                                                                     AccessRights.Send
                                                                                 });
                        eventHubDescription.Authorization.Add(authorizationRule);
                        await namespaceManager.UpdateEventHubAsync(eventHubDescription);
                    }
                    
                    cancellationTokenSource = new CancellationTokenSource();
                    var serviceBusNamespace = txtNamespace.Text;
                    var eventHubName = txtEventHub.Text;
                    var senderKey = authorizationRule.PrimaryKey;
                    var location = txtLocation.Text;
                    var eventInterval = txtEventIntervalInSeconds.IntegerValue * 1000;
                    var minValue = txtMinValue.IntegerValue;
                    var maxValue = txtMaxValue.IntegerValue;
                    var cancellationToken = cancellationTokenSource.Token;

                    // Create one task for each device
                    for (var i = 1; i <= txtDeviceCount.IntegerValue; i++)
                    {
                        var deviceId = i;
                        #pragma warning disable 4014
                        #pragma warning disable 4014
                        Task.Run(async () =>
                        #pragma warning restore 4014
                        {
                            var deviceName = $"device{deviceId:000}";
                            var random = new Random((int)DateTime.Now.Ticks);

                            if (radioButtonAmqp.Checked)
                            {
                                // The token has the following format: 
                                // SharedAccessSignature sr={URI}&sig={HMAC_SHA256_SIGNATURE}&se={EXPIRATION_TIME}&skn={KEY_NAME}
                                var token = CreateSasTokenForAmqpSender(SenderSharedAccessKey,
                                                                        senderKey,
                                                                        serviceBusNamespace,
                                                                        eventHubName,
                                                                        deviceName,
                                                                        TimeSpan.FromDays(1));
                                WriteToLog(string.Format(SasToken, deviceId));

                                var messagingFactory = MessagingFactory.Create(ServiceBusEnvironment.CreateServiceUri("sb", serviceBusNamespace, ""), new MessagingFactorySettings
                                {
                                    TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(token),
                                    TransportType = TransportType.Amqp
                                });
                                WriteToLog(string.Format(MessagingFactoryCreated, deviceId));

                                // Each device uses a different publisher endpoint: [EventHub]/publishers/[PublisherName]
                                var eventHubClient = messagingFactory.CreateEventHubClient($"{eventHubName}/publishers/{deviceName}");
                                WriteToLog(string.Format(EventHubClientCreated, deviceId, eventHubClient.Path));

                                while (!cancellationToken.IsCancellationRequested)
                                {
                                    // Create random value
                                    var value = random.Next(minValue, maxValue + 1);

                                    // Create EventData object with the payload serialized in JSON format 
                                    using (var eventData = new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Payload
                                    {
                                        EventId = eventId++,
                                        DeviceId = deviceId,
                                        Value = value,
                                        Timestamp = DateTime.UtcNow
                                    })))
                                    {
                                        PartitionKey = deviceName
                                    })
                                    {
                                        // Create custom properties
                                        eventData.Properties.Add(DeviceId, deviceId);
                                        eventData.Properties.Add(DeviceName, deviceName);
                                        eventData.Properties.Add(DeviceLocation, location);
                                        eventData.Properties.Add(Value, value);

                                        // Send the event to the event hub
                                        await eventHubClient.SendAsync(eventData);
                                        WriteToLog(string.Format(EventSent, deviceId, deviceName, value));
                                    }

                                    // Wait for the event time interval
                                    Thread.Sleep(eventInterval);
                                }
                            }
                            else
                            {
                                // The token has the following format: 
                                // SharedAccessSignature sr={URI}&sig={HMAC_SHA256_SIGNATURE}&se={EXPIRATION_TIME}&skn={KEY_NAME}
                                var token = CreateSasTokenForHttpsSender(SenderSharedAccessKey,
                                                                         senderKey,
                                                                         serviceBusNamespace,
                                                                         eventHubName,
                                                                         deviceName,
                                                                         TimeSpan.FromDays(1));
                                WriteToLog(string.Format(SasToken, deviceId));

                                // Create HttpClient object used to send events to the event hub.
                                var httpClient = new HttpClient
                                {
                                    BaseAddress = new Uri($"https://{serviceBusNamespace}.servicebus.windows.net/{eventHubName}/publishers/{deviceName}".ToLower())
                                };
                                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
                                httpClient.DefaultRequestHeaders.Add("ContentType", "application/json;type=entry;charset=utf-8");
                                WriteToLog(string.Format(HttpClientCreated, deviceId, httpClient.BaseAddress));

                                while (!cancellationToken.IsCancellationRequested)
                                {
                                    // Create random value
                                    var value = random.Next(minValue, maxValue + 1);

                                    // Create HttpContent
                                    var postContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Payload
                                    {
                                        EventId = eventId++,
                                        DeviceId = deviceId,
                                        Value = value,
                                        Timestamp = DateTime.UtcNow
                                    })));

                                    // Create custom properties
                                    
                                    postContent.Headers.Add(DeviceId, deviceId.ToString(CultureInfo.InvariantCulture));
                                    postContent.Headers.Add(DeviceName, deviceName);
                                    //postContent.Headers.Add(DeviceLocation, location);
                                    postContent.Headers.Add(Value, value.ToString(CultureInfo.InvariantCulture));

                                    try
                                    {
                                        var response = await httpClient.PostAsync(httpClient.BaseAddress + "/messages" + "?timeout=60" + ApiVersion, postContent, cancellationToken);
                                        response.EnsureSuccessStatusCode();
                                        WriteToLog(string.Format(EventSent, deviceId, deviceName, value));
                                    }
                                    catch (HttpRequestException ex)
                                    {
                                        WriteToLog(string.Format(SendFailed, deviceId, ex.Message));
                                    }
                                }
                            }
                        },
                        cancellationToken).ContinueWith(t =>
#pragma warning restore 4014
                        #pragma warning restore 4014
                        {
                            if (t.IsFaulted && t.Exception != null)
                            {
                                HandleException(t.Exception);
                            }
                        }, cancellationToken);
                    }

                }
                else
                {
                    // Change button text
                    btnStart.Text = Start;
                    cancellationTokenSource?.Cancel();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private bool ValidateParameters()
        {
            if (string.IsNullOrWhiteSpace(txtNamespace.Text))
            {
                WriteToLog(NamespaceCannonBeNull);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEventHub.Text))
            {
                WriteToLog(EventHubNameCannonBeNull);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtKeyName.Text))
            {
                WriteToLog(KeyNameCannonBeNull);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtKeyValue.Text))
            {
                WriteToLog(KeyValueCannonBeNull);
                return false;
            }
            return true;
        }

        public static string CreateSasTokenForAmqpSender(string senderKeyName, 
                                                         string senderKey, 
                                                         string serviceNamespace, 
                                                         string hubName, 
                                                         string publisherName, 
                                                         TimeSpan tokenTimeToLive)
        {
            // This is the format of the publisher endpoint. Each device uses a different publisher endpoint.
            // sb://<NAMESPACE>.servicebus.windows.net/<EVENT_HUB_NAME>/publishers/<PUBLISHER_NAME>. 
            var serviceUri = ServiceBusEnvironment.CreateServiceUri("sb", 
                                                                    serviceNamespace,
                                                                    $"{hubName}/publishers/{publisherName}")
                                                                    .ToString()
                                                                    .Trim('/');
            // SharedAccessSignature sr=<URL-encoded-resourceURI>&sig=<URL-encoded-signature-string>&se=<expiry-time-in-ISO-8061-format. >&skn=<senderKeyName>
            return SharedAccessSignatureTokenProvider.GetSharedAccessSignature(senderKeyName, senderKey, serviceUri, tokenTimeToLive);
        }

        // Create a SAS token for a specified scope. SAS tokens are described in http://msdn.microsoft.com/en-us/library/windowsazure/dn170477.aspx.
        private static string CreateSasTokenForHttpsSender(string senderKeyName, 
                                                           string senderKey, 
                                                           string serviceNamespace, 
                                                           string hubName, 
                                                           string publisherName, 
                                                           TimeSpan tokenTimeToLive)
        {
            // Set token lifetime. When supplying a device with a token, you might want to use a longer expiration time.
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var difference = DateTime.Now.ToUniversalTime() - origin;
            var tokenExpirationTime = Convert.ToUInt32(difference.TotalSeconds) + tokenTimeToLive.Seconds;

            // https://<NAMESPACE>.servicebus.windows.net/<EVENT_HUB_NAME>/publishers/<PUBLISHER_NAME>. 
            var uri = ServiceBusEnvironment.CreateServiceUri("https", serviceNamespace,
                $"{hubName}/publishers/{publisherName}")
                .ToString()
                .Trim('/');
            var stringToSign = HttpUtility.UrlEncode(uri) + "\n" + tokenExpirationTime;
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(senderKey));

            var signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));

            // SharedAccessSignature sr=<URL-encoded-resourceURI>&sig=<URL-encoded-signature-string>&se=<expiry-time-in-ISO-8061-format. >&skn=<senderKeyName>
            var token = String.Format(CultureInfo.InvariantCulture, "SharedAccessSignature sr={0}&sig={1}&se={2}&skn={3}",
            HttpUtility.UrlEncode(uri), HttpUtility.UrlEncode(signature), tokenExpirationTime, senderKeyName);
            return token;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
        }
        #endregion
    }
}
