using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hunger_Guru
{
    public class SearchCriteria
    {
        private String city_name;
        private int city_id;

        private String state_name;

        private int[] cuisine_list;

        public SearchCriteria(String name, int id, String state_name, int[] cuisine_list)
        {
            this.city_name = name;
            this.city_id = id;
            this.state_name = state_name;
            this.cuisine_list = cuisine_list;
        }

        public SearchCriteria()
        {
            this.city_name = "";
            this.city_id = 0;
            this.state_name = "";
            this.cuisine_list = new int [20];
        }

        public void setCity_id(int id)
        {
            this.city_id = id;
        }
        public int getcity_id()
        {
            return this.city_id;
        }
    }
}
