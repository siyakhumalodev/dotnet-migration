using System;
using Microsoft.Extensions.Configuration;
using ContosoUniversity.Models;
using Newtonsoft.Json;

namespace ContosoUniversity.Services
{
    // MSMQ abstraction - System.Messaging is not available in .NET Core
    // This implementation provides a stub for notification functionality
    // In production, replace with Azure Service Bus, RabbitMQ, or similar
    public class NotificationService : IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly string _queuePath;
        private bool _disposed = false;

        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
            // Get queue path from configuration or use default
            _queuePath = _configuration["NotificationQueuePath"] ?? @".\Private$\ContosoUniversityNotifications";
            
            // MSMQ functionality is stubbed out for .NET Core compatibility
            // TODO: Implement with modern message queue (Azure Service Bus, RabbitMQ, etc.)
            System.Diagnostics.Debug.WriteLine($"NotificationService initialized (MSMQ stubbed). Queue path: {_queuePath}");
        }

        public void SendNotification(string entityType, string entityId, EntityOperation operation, string? userName = null)
        {
            SendNotification(entityType, entityId, null, operation, userName);
        }

        public void SendNotification(string entityType, string entityId, string? entityDisplayName, EntityOperation operation, string? userName = null)
        {
            try
            {
                var notification = new Notification
                {
                    EntityType = entityType,
                    EntityId = entityId,
                    Operation = operation.ToString(),
                    Message = GenerateMessage(entityType, entityId, entityDisplayName, operation),
                    CreatedAt = DateTime.Now,
                    CreatedBy = userName ?? "System",
                    IsRead = false
                };

                // STUBBED: MSMQ Send
                // TODO: Replace with actual message queue implementation
                var jsonMessage = JsonConvert.SerializeObject(notification);
                System.Diagnostics.Debug.WriteLine($"[STUBBED] Notification sent: {jsonMessage}");
            }
            catch (Exception ex)
            {
                // Log error but don't break the main operation
                System.Diagnostics.Debug.WriteLine($"Failed to send notification: {ex.Message}");
            }
        }

        public Notification? ReceiveNotification()
        {
            // STUBBED: MSMQ Receive
            // TODO: Replace with actual message queue implementation
            System.Diagnostics.Debug.WriteLine("[STUBBED] ReceiveNotification called");
            return null;
        }

        public void MarkAsRead(int notificationId)
        {
            // STUBBED: Mark as read
            // TODO: Implement with persistence layer (database)
            System.Diagnostics.Debug.WriteLine($"[STUBBED] MarkAsRead called for ID: {notificationId}");
        }

        private string GenerateMessage(string entityType, string entityId, string? entityDisplayName, EntityOperation operation)
        {
            var displayText = !string.IsNullOrWhiteSpace(entityDisplayName) 
                ? $"{entityType} '{entityDisplayName}'" 
                : $"{entityType} (ID: {entityId})";

            return operation switch
            {
                EntityOperation.CREATE => $"New {displayText} has been created",
                EntityOperation.UPDATE => $"{displayText} has been updated",
                EntityOperation.DELETE => $"{displayText} has been deleted",
                _ => $"{displayText} operation: {operation}"
            };
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                // Cleanup resources if needed
                _disposed = true;
            }
        }
    }
}
