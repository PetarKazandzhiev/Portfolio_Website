using Portfolio_Petar.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio_Petar.App_Start
{
    public static class DatabaseHelper{
        private static Portfolio_DBEntities _database = new Portfolio_DBEntities();

         
        public static List<Comment> GetComments(){
            return _database.Comments.OrderByDescending(c=>c.Date).Take(20).ToList();
        }
       


    }

}