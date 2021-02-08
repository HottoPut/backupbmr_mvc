using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace BMR_MVC.Models
{
    public class BcaSignalR : Hub
    {
        public void Send(String type, String type2, Int64 jobSysId, Int64 step, Int64 runNo, String userName, String date,Int64 index)
        {
            Clients.All.broadcastMessage(type, type2, jobSysId, step, runNo, userName, date,index);
        }
    }
}