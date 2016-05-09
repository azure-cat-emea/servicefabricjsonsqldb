#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on the community
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
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceFabric.Services.Communication.Runtime;

#endregion

namespace Microsoft.AzureCat.Samples.EventProcessorHostService
{
    public class EventProcessorHostListener : ICommunicationListener
    {
        #region Private Constants
        //************************************
        // Parameters
        //************************************
        private const string SqlDatabaseConnectionStringParameter = "SqlDatabaseConnectionString";
        private const string InsertStoredProcedureParameter = "InsertStoredProcedure";
        private const string ConfigurationPackage = "Config";
        private const string ConfigurationSection = "EventProcessorHostConfig";
        private const string StorageAccountConnectionStringParameter = "StorageAccountConnectionString";
        private const string ServiceBusConnectionStringParameter = "ServiceBusConnectionString";
        private const string EventHubNameParameter = "EventHubName";
        private const string ConsumerGroupNameParameter = "ConsumerGroupName";
       
        private const string CheckpointCountParameter = "CheckpointCount";

        //************************************
        // Formats
        //************************************
        private const string ParameterCannotBeNullFormat = "The parameter [{0}] is not defined in the Setting.xml configuration file.";
        private const string RegisteringEventProcessor = "Registering Event Processor [EventProcessor]... ";
        private const string EventProcessorRegistered = "Event Processor [EventProcessor] successfully registered. ";
        #endregion

        #region Private Fields
        private string sqlDatabaseConnectionString;
        private string insertStoredProcedure;
        private string storageAccountConnectionString;
        private string serviceBusConnectionString;
        private string eventHubName;
        private string consumerGroupName;
        private int checkpointCount = 100;
        private EventProcessorHost eventProcessorHost;
        private readonly StatelessServiceContext context;
        #endregion

        #region Public Constructor
        public EventProcessorHostListener(StatelessServiceContext context)
        {
            this.context = context;
        }
        #endregion

        #region ICommunicationListener Methods
        public async Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            try
            {
                // Get the EventProcessorHostConfig section from the Settings.xml file
                var codePackageActivationContext = context.CodePackageActivationContext;
                var config = codePackageActivationContext.GetConfigurationPackageObject(ConfigurationPackage);
                var section = config.Settings.Sections[ConfigurationSection];

                // Check if a parameter called ServiceBusConnectionString exists in the EventProcessorHostConfig config section
                if (section.Parameters.Any(p => string.Compare(p.Name,
                                                               StorageAccountConnectionStringParameter,
                                                               StringComparison.InvariantCultureIgnoreCase) == 0))
                {
                    // Read the StorageAccountConnectionString setting from the Settings.xml file
                    var parameter = section.Parameters[StorageAccountConnectionStringParameter];
                    if (!string.IsNullOrWhiteSpace(parameter?.Value))
                    {
                        storageAccountConnectionString = parameter.Value;
                    }
                    else
                    {
                        throw new ArgumentException(string.Format(ParameterCannotBeNullFormat,
                                                              StorageAccountConnectionStringParameter),
                                                              StorageAccountConnectionStringParameter);
                    }
                }
                else
                {
                    throw new ArgumentException(string.Format(ParameterCannotBeNullFormat, 
                                                              StorageAccountConnectionStringParameter),
                                                              StorageAccountConnectionStringParameter);
                }

                // Check if a parameter called ServiceBusConnectionString exists in the EventProcessorHostConfig config section
                if (section.Parameters.Any(p => string.Compare(p.Name,
                                                               ServiceBusConnectionStringParameter,
                                                               StringComparison.InvariantCultureIgnoreCase) == 0))
                {
                    // Read the ServiceBusConnectionString setting from the Settings.xml file
                    var parameter = section.Parameters[ServiceBusConnectionStringParameter];
                    if (!string.IsNullOrWhiteSpace(parameter?.Value))
                    {
                        serviceBusConnectionString = parameter.Value;
                    }
                    else
                    {
                        throw new ArgumentException(string.Format(ParameterCannotBeNullFormat, 
                                                                  ServiceBusConnectionStringParameter),
                                                                  ServiceBusConnectionStringParameter);
                    }
                }
                else
                {
                    throw new ArgumentException(string.Format(ParameterCannotBeNullFormat,
                                                              ServiceBusConnectionStringParameter),
                                                              ServiceBusConnectionStringParameter);
                }

                // Check if a parameter called ConsumerGroupName exists in the EventProcessorHostConfig config section
                if (section.Parameters.Any(p => string.Compare(p.Name,
                                                               ConsumerGroupNameParameter,
                                                               StringComparison.InvariantCultureIgnoreCase) == 0))
                {
                    // Read the ConsumerGroupName setting from the Settings.xml file
                    var parameter = section.Parameters[ConsumerGroupNameParameter];
                    if (!string.IsNullOrWhiteSpace(parameter?.Value))
                    {
                        consumerGroupName = parameter.Value;
                    }
                    else
                    {
                        throw new ArgumentException(string.Format(ParameterCannotBeNullFormat, 
                                                                  ConsumerGroupNameParameter),
                                                                  ConsumerGroupNameParameter);
                    }
                }
                else
                {
                    throw new ArgumentException(string.Format(ParameterCannotBeNullFormat,
                                                              ConsumerGroupNameParameter),
                                                              ConsumerGroupNameParameter);
                }

                // Check if a parameter called SqlDatabaseConnectionString exists in the EventProcessorHostConfig config section
                if (section.Parameters.Any(p => string.Compare(p.Name,
                                                               SqlDatabaseConnectionStringParameter,
                                                               StringComparison.InvariantCultureIgnoreCase) == 0))
                {
                    // Read the SqlDatabaseConnectionString setting from the Settings.xml file
                    var parameter = section.Parameters[SqlDatabaseConnectionStringParameter];
                    if (!string.IsNullOrWhiteSpace(parameter?.Value))
                    {
                        sqlDatabaseConnectionString = parameter.Value;
                    }
                    else
                    {
                        throw new ArgumentException(string.Format(ParameterCannotBeNullFormat,
                                                                  SqlDatabaseConnectionStringParameter),
                                                                  SqlDatabaseConnectionStringParameter);
                    }
                }
                else
                {
                    throw new ArgumentException(string.Format(ParameterCannotBeNullFormat,
                                                              SqlDatabaseConnectionStringParameter),
                                                              SqlDatabaseConnectionStringParameter);
                }

                // Check if a parameter called InsertStoredProcedure exists in the EventProcessorHostConfig config section
                if (section.Parameters.Any(p => string.Compare(p.Name,
                                                               InsertStoredProcedureParameter,
                                                               StringComparison.InvariantCultureIgnoreCase) == 0))
                {
                    // Read the InsertStoredProcedure setting from the Settings.xml file
                    var parameter = section.Parameters[InsertStoredProcedureParameter];
                    if (!string.IsNullOrWhiteSpace(parameter?.Value))
                    {
                        insertStoredProcedure = parameter.Value;
                    }
                    else
                    {
                        throw new ArgumentException(string.Format(ParameterCannotBeNullFormat,
                                                                  InsertStoredProcedureParameter),
                                                                  InsertStoredProcedureParameter);
                    }
                }
                else
                {
                    throw new ArgumentException(string.Format(ParameterCannotBeNullFormat,
                                                              InsertStoredProcedureParameter),
                                                              InsertStoredProcedureParameter);
                }

                // Check if a parameter called EventHubName exists in the EventProcessorHostConfig config section
                if (section.Parameters.Any(p => string.Compare(p.Name,
                                                               EventHubNameParameter,
                                                               StringComparison.InvariantCultureIgnoreCase) == 0))
                {
                    // Read the EventHubName setting from the Settings.xml file
                    var parameter = section.Parameters[EventHubNameParameter];
                    if (!string.IsNullOrWhiteSpace(parameter?.Value))
                    {
                        eventHubName = parameter.Value;
                    }
                    else
                    {
                        throw new ArgumentException(string.Format(ParameterCannotBeNullFormat, 
                                                                  EventHubNameParameter),
                                                                  EventHubNameParameter);
                    }
                }
                else
                {
                    throw new ArgumentException(string.Format(ParameterCannotBeNullFormat,
                                                              EventHubNameParameter),
                                                              EventHubNameParameter);
                }

                // Check if a parameter called CheckpointCount exists in the EventProcessorHostConfig config section
                if (section.Parameters.Any(p => string.Compare(p.Name,
                                                               CheckpointCountParameter,
                                                               StringComparison.InvariantCultureIgnoreCase) == 0))
                {
                    // Read the CheckpointCount setting from the Settings.xml file
                    var parameter = section.Parameters[CheckpointCountParameter];
                    int value;
                    if (int.TryParse(parameter.Value, out value))
                    {
                        checkpointCount = value;
                    }
                }

                // Start EventProcessorHost
                await StartEventProcessorAsync();

                // Return Event Hub name
                return eventHubName;
            }
            catch (Exception ex)
            {
                // Trace Error
                ServiceEventSource.Current.Message(ex.Message);
                throw;
            }
        } 
       
        public Task CloseAsync(CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                // Trace Error
                ServiceEventSource.Current.Message(ex.Message);
                throw;
            }
        }

        public void Abort()
        {
            try
            {
            }
            catch (Exception ex)
            {
                // Trace Error
                ServiceEventSource.Current.Message(ex.Message);
                throw;
            }
        }
        #endregion

        #region Private Methods
        private async Task StartEventProcessorAsync()
        {
            try
            {
                // Create EventHubClient instance
                var eventHubClient = EventHubClient.CreateFromConnectionString(serviceBusConnectionString, eventHubName);

                // Get the default Consumer Group
                eventProcessorHost = new EventProcessorHost(context.InstanceId.ToString(),
                                                            eventHubClient.Path.ToLower(),
                                                            consumerGroupName.ToLower(),
                                                            serviceBusConnectionString,
                                                            storageAccountConnectionString)
                {
                    PartitionManagerOptions = new PartitionManagerOptions
                    {
                        AcquireInterval = TimeSpan.FromSeconds(10), // Default is 10 seconds
                        RenewInterval = TimeSpan.FromSeconds(10), // Default is 10 seconds
                        LeaseInterval = TimeSpan.FromSeconds(30) // Default value is 30 seconds
                    }
                };
                ServiceEventSource.Current.Message(RegisteringEventProcessor);
                var eventProcessorOptions = new EventProcessorOptions
                {
                    InvokeProcessorAfterReceiveTimeout = true,
                    MaxBatchSize = 100,
                    PrefetchCount = 100,
                    ReceiveTimeOut = TimeSpan.FromSeconds(30),
                };
                eventProcessorOptions.ExceptionReceived += EventProcessorOptions_ExceptionReceived;
                await eventProcessorHost.RegisterEventProcessorFactoryAsync(new EventProcessorFactory<EventProcessor>(sqlDatabaseConnectionString,
                                                                                                                      insertStoredProcedure,
                                                                                                                      checkpointCount),
                                                                            eventProcessorOptions);
                ServiceEventSource.Current.Message(EventProcessorRegistered);
            }
            catch (Exception ex)
            {
                // Trace Error
                ServiceEventSource.Current.Message(ex.Message);
                throw;
            }
        }

        private static void EventProcessorOptions_ExceptionReceived(object sender, ExceptionReceivedEventArgs e)
        {
            if (e?.Exception == null)
            {
                return;
            }

            // Trace Exception
            ServiceEventSource.Current.Message(e.Exception.Message);
        }
        #endregion
    }
}
