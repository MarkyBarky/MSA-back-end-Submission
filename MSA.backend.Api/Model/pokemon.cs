using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;

namespace MSA.backend.Model
{
    public class pokemon
    {
        [Key]
        public string name { get; set; }
       
        public int weight { get; set; }
        
        public string ability { get; set; }
        
    }
}