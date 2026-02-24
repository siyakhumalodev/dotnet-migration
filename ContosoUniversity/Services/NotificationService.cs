using System;
using System.Collections.Concurrent;
using System.Text.Json;
using ContosoUniversity.Models;
using Microsoft.Extensions.Logging;

namespace ContosoUniversity.Services
{
    public class NotificationService : IDisposable
    {
        private static readonly ConcurrentQueue<Notification> _notificationQueue = new();
        private readonly ILogger<NotificationService>? _logger;

        public NotificationService()
        {
        }

        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
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

                _notificationQueue.Enqueue(notification);
                _logger?.LogInformation("Notification sent: {EntityType} {Operation}", entityType, operation);
            }
            catch (Exception ex)
            {
                // Log error but don't break the main operation
                _logger?.LogError(ex, "Failed to send notification");
            }
        }

        public Notification? ReceiveNotification()
        {
            try
            {
                if (_notificationQueue.TryDequeue(out var notification))
                {
                    return notification;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failed to receive notification");
                return null;
            }
        }

        public void MarkAsRead(int notificationId)
        {
            // In a real implementation, you might want to store notifications in database as well
            // for persistence and tracking read status
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
            // Nothing to dispose in memory-based implementation
        }
    }
}
