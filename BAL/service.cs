using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL
{
    public class service
    {
        #region economic
        public int Id { get; set; }
        public string From_Weight { get; set; }
        public string To_Weight { get; set; }
        public string Price { get; set; }
        public int coid { get; set; }
        #endregion

        public byte IsActive { get; set; }
        public byte IsDelete { get; set; }
       
    }
}
