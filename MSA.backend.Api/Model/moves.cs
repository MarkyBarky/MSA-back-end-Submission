using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSA.backend.Api.Model
{
    public class Move
    {
        [Key]
        public string move { get; set; }

        public string name { get; set; }

        
    }
}
