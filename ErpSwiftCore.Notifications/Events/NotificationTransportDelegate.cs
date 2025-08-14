using ErpSwiftCore.Domain.Entities.EntityNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Notifications.Events
{
    /// <summary>
    /// يعلم كيف يأخذ Notification ويُرسِله عبر Email/SMS/Push… يرجع true إذا نجح.
    /// </summary>
    public delegate Task<bool> NotificationTransportDelegate(
        Notification notification);
}