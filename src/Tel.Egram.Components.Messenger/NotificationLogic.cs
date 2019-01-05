using System;
using System.Reactive.Linq;
using ReactiveUI;
using Splat;
using Tel.Egram.Components.Notifications;
using Tel.Egram.Messaging.Notifications;
using Tel.Egram.Utils.Reactive;

namespace Tel.Egram.Components.Messenger
{
    public static class NotificationLogic
    {
        public static IDisposable BindNotifications(
            this MessengerModel model)
        {
            return BindNotifications(
                model,
                Locator.Current.GetService<INotificationSource>(),
                Locator.Current.GetService<INotificationController>());
        }
        
        public static IDisposable BindNotifications(
            this MessengerModel model,
            INotificationSource notificationSource,
            INotificationController notificationController)
        {
            var chats = notificationSource.ChatNotifications();
            var messages = notificationSource.MessagesNotifications();

            return chats.Merge(messages)
                .Buffer(TimeSpan.FromSeconds(2))
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Accept(notifications =>
                {
                    switch (notifications.Count)
                    {
                        case 0:
                            break;
                        
                        case 1:
                            notificationController.Show(
                                NotificationModel.FromNotification(notifications[0]));
                            break;
                        
                        default:
                            notificationController.Show(
                                NotificationModel.FromNotificationList(notifications));
                            break;
                    }
                });
        }
    }
}