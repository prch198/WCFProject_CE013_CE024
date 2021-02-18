using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TenantFinder
{
    [DataContract]
    public class House
    {
        private int id;
        private int no;
        private string name;
        private string area;
        private string city;
        private string category;
        private float rent;
        private string reqtenant;

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public int No
        {
            get { return no; }
            set { no = value; }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public string Area
        {
            get { return area; }
            set { area = value; }
        }

        [DataMember]
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        [DataMember]
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        [DataMember]
        public float Rent
        {
            get { return rent; }
            set { rent = value; }
        }

        [DataMember]
        public string ReqTenant
        {
            get { return reqtenant; }
            set { reqtenant = value; }
        }
    }
}
