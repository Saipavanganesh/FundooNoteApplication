using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Models
{
    public class CreateNotesModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
