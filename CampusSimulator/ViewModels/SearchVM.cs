using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
//using System.Data.SqlClient;
using CampusSimulator.Models;

namespace CampusSimulator.ViewModels
{
    class SearchVM
    {
        Campuses campuses = new Campuses();

        public SearchVM()
        {
            
        }

        public Campus getCampus(string campusName)
        {
            return campuses.getCampus(campusName);
        }
    }
}
