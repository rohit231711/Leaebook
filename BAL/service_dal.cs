using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BAL
{
    public class service_dal
    {
        service sc = new service();
        dbconnection d = new dbconnection();

        public void insert_economic_service(service sc)
        {
            d.executedml("insert_economic_service '"+sc.From_Weight+"','"+sc.To_Weight+"','"+sc.Price+"' ");
        }

        public void update_economic_service(service sc)
        {
            d.executedml("edit_economic_service '" + sc.From_Weight + "','" + sc.To_Weight + "','" + sc.Price + "','"+sc.Id+"' ");
        }

        public void insert_inter_service(service sc)
        {
            d.executedml("insert_inter_service '" + sc.From_Weight + "','" + sc.To_Weight + "','" + sc.Price + "' ");
        }

        public void update_inter_service(service sc)
        {
            d.executedml("edit_inter_service '" + sc.From_Weight + "','" + sc.To_Weight + "','" + sc.Price + "','" + sc.Id + "' ");
        }

        public void insert_special_service(service sc)
        {
            d.executedml("insert_special_service '" + sc.From_Weight + "','" + sc.To_Weight + "','" + sc.Price + "' ");
        }

        public void update_special_service(service sc)
        {
            d.executedml("edit_special_service '" + sc.From_Weight + "','" + sc.To_Weight + "','" + sc.Price + "','" + sc.Id + "' ");
        }

        public void insert_International_currier_service(service sc)
        {
            d.executedml("insert_International_currier_service '" + sc.From_Weight + "','" + sc.Price + "','"+sc.coid+"' ");
        }

        public void update_International_currier_service(service sc)
        {
            d.executedml("edit_International_currier_service '" + sc.From_Weight + "','" + sc.Price + "','" + sc.coid + "','" + sc.Id + "' ");
        }
    }
}
