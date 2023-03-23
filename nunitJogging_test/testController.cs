using Azure;
using jogging_times.Controllers;
using jogging_times.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace nunitJogging_test
{
   
    public class testController
    {
        private ApplicationDbContext context;
        private UserManager<User> userManager;

        [Test]
        public void GetAllJoggingTimesForUser_ShoudRetunSomeNumber()
        {
            
        
            var controller = new joggingTimeController( context, userManager);
            var result = controller.joggingTimes_For_User();
            Assert.IsNotNull(result);
   
        }
        [Test]
        public void GetAllJoggingTimesForAllUser_ShoudRetunSomeNumber()
        {//suppose have 3 joggingTimes
            int numberJogging = 3;
            var controller = new joggingTimeController(context, userManager);

            var result = controller.joggingTimes_For_All_User();
 
            Assert.AreEqual(numberJogging, result.Count);
           
        
        }
   
      
      


    }
}
