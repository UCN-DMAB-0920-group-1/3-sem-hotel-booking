using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Price :BaseModel
    {
        public int Value { get; set; }
        public DateTime Start_Date { get; set; }
        public int Room_Type_Id { get; set; }

    }
}
