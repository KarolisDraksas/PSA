using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSA.Models
{
    public class following
    {
        /*[Column(TypeName = "nvarchar(100)")]*/
        public string UserID { get; set; }

        /*[Column(TypeName = "nvarchar(100)")]*/
        public string followedUserID { get; set; }
    }
}
