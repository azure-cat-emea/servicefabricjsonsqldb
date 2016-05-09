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
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
#endregion

namespace Microsoft.AzureCat.Samples.EventProcessorHostService
{
    public class EventProcessor : IEventProcessor
    {
        #region Private Fields
        private readonly string sqlDatabaseConnectionString;
        private readonly string insertStoredProcedure;
        private readonly int checkpointCount;
        private int messageCount;
        #endregion

        #region Public Constructors
        public EventProcessor(string sqlDatabaseConnectionString, 
                              string insertStoredProcedure,
                              int checkpointCount)
        {
            if (string.IsNullOrWhiteSpace(sqlDatabaseConnectionString))
            {
                throw new ArgumentNullException($"{nameof(sqlDatabaseConnectionString)} parameter cannot be null");
            }
            if (string.IsNullOrWhiteSpace(insertStoredProcedure))
            {
                throw new ArgumentNullException($"{nameof(insertStoredProcedure)} parameter cannot be null");
            }
            this.sqlDatabaseConnectionString = sqlDatabaseConnectionString;
            this.insertStoredProcedure = insertStoredProcedure;
            this.checkpointCount = checkpointCount > 0 ? checkpointCount : 1;
        }
        #endregion

        #region IEventProcessor Methods
        public Task OpenAsync(PartitionContext context)
        {
            try
            {
                // Trace Open Partition
                ServiceEventSource.Current.OpenPartition(context.EventHubPath,
                                                         context.ConsumerGroupName,
                                                         context.Lease.PartitionId);
            }
            catch (Exception ex)
            {
                // Trace Exception
                ServiceEventSource.Current.Message(ex.Message);
            }
            return Task.FromResult<object>(null);
        }

        public async Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> events)
        {
            try
            {
                if (events == null)
                {
                    return;
                }

                var eventList = events.Select(e => Encoding.UTF8.GetString(e.GetBytes())).ToList();

                if (!eventList.Any())
                {
                    return;
                }

                // Trace Process Events
                ServiceEventSource.Current.ProcessEvents(context.EventHubPath,
                                                         context.ConsumerGroupName,
                                                         context.Lease.PartitionId,
                                                         eventList.Count);

                using (var sqlConnection = new SqlConnection(sqlDatabaseConnectionString))
                {
                    await sqlConnection.OpenAsync();

                    // Create command
                    var sqlCommand = new SqlCommand(insertStoredProcedure, sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Add table-valued parameter
                    sqlCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@Events",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = -1,
                        Value = GetJsonArray(eventList)
                    });

                    // Execute the query
                    await sqlCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
                }

                // Increase messageCount
                messageCount += eventList.Count;

                // Invoke CheckpointAsync when messageCount => checkpointCount
                if (messageCount < checkpointCount)
                {
                    return;
                }

                await context.CheckpointAsync();
                messageCount = 0;
            }
            catch (LeaseLostException ex)
            {
                // Trace Exception as message
                ServiceEventSource.Current.Message(ex.Message);
            }
            catch (AggregateException ex)
            {
                // Trace Exception
                foreach (var exception in ex.InnerExceptions)
                {
                    ServiceEventSource.Current.Message(exception.Message);
                }
            }
            catch (Exception ex)
            {
                // Trace Exception
                ServiceEventSource.Current.Message(ex.Message);
            }
        }

        public async Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            try
            {
                // Trace Open Partition
                ServiceEventSource.Current.ClosePartition(context.EventHubPath,
                                                          context.ConsumerGroupName,
                                                          context.Lease.PartitionId,
                                                          reason.ToString());

                if (reason == CloseReason.Shutdown)
                {
                    await context.CheckpointAsync();
                }
            }
            catch (Exception ex)
            {
                // Trace Exception
                ServiceEventSource.Current.Message(ex.Message);
            }
        }
        #endregion

        #region Private Methods
        private string GetJsonArray(IReadOnlyList<string> list)
        {
            if (list == null || list.Count == 0)
            {
                return null;
            }
            var builder = new StringBuilder("[");
            for (var i = 0; i < list.Count; i++)
            {
                builder.Append(i == 0 ? list[0] : $",{list[i]}");
            }
            builder.Append("]");
            return builder.ToString();
        }
        #endregion
    }
}

