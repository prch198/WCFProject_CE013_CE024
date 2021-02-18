using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TenantFinder
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class HouseService : IHouseService
    {
        public bool AddHouse(House h)
        {
            SqlConnection cnn = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Tenant; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = "insert into Houses(no,name,area,city,rent,category,reqtenant) values (@no,@name,@area,@city,@rent,@category,@reqtenant)";
            SqlParameter h1 = new SqlParameter("@no", h.No);
            SqlParameter h2 = new SqlParameter("@name", h.Name);
            SqlParameter h3 = new SqlParameter("@area", h.Area);
            SqlParameter h4 = new SqlParameter("@city", h.City);
            SqlParameter h5 = new SqlParameter("@rent", h.Rent);
            SqlParameter h6 = new SqlParameter("@category", h.Category);
            SqlParameter h7 = new SqlParameter("@reqtenant", h.ReqTenant);

            cmd.Parameters.Add(h1);
            cmd.Parameters.Add(h2);
            cmd.Parameters.Add(h3);
            cmd.Parameters.Add(h4);
            cmd.Parameters.Add(h5);
            cmd.Parameters.Add(h6);
            cmd.Parameters.Add(h7);
            cnn.Open();
            int reader = cmd.ExecuteNonQuery();
            cnn.Close();
            if (reader == 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteHouse(int id)
        {
            SqlConnection cnn = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Tenant; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = "delete from Houses where id=@id";
            SqlParameter h1 = new SqlParameter("@id", id);

            cmd.Parameters.Add(h1);
            cnn.Open();
            int reader = cmd.ExecuteNonQuery();
            cnn.Close();
            if (reader == 0)
            {
                NotFoundHouse nfh = new NotFoundHouse();
                nfh.Exception = "No house record found with given ID";
                throw new FaultException<NotFoundHouse>(nfh);
            }
            return true;
        }

        public DataSet GetAllHouses()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * from Houses",@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Tenant; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            DataSet ds = new DataSet();
            da.Fill(ds, "Houses");
            return ds;
        }

        /*public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }*/

        /*public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }*/

        public House GetHouse(int id)
        {
            SqlConnection cnn = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Tenant; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = "select * from Houses where id=@id";
            SqlParameter h1 = new SqlParameter("@id", id);
            cmd.Parameters.Add(h1);
            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            House house = new House();
            if (reader.Read())
            {
                house.ID = reader.GetInt32(0);
                house.No = reader.GetInt32(1);
                house.Name = reader.GetString(2);
                house.Area = reader.GetString(3);
                house.City = reader.GetString(4);
                house.Category = reader.GetString(5);
                house.Rent = reader.GetFloat(6);
                house.ReqTenant = reader.GetString(7);

            }
            else
            {
                NotFoundHouse nfh = new NotFoundHouse();
                nfh.Exception = "No house record found with given ID";
                throw new FaultException<NotFoundHouse>(nfh);
            }
            reader.Close();
            cnn.Close();
            return house;
        }

        public House UpdateHouse(House h)
        {
            SqlConnection cnn = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Tenant; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = "update Houses set no=@no,name=@name,arae=@area,city=@city,category=@category,rent=@rent,reqtenant=@ReqTenant where id=@id";
            SqlParameter h1 = new SqlParameter("@id", h.ID);
            SqlParameter h2 = new SqlParameter("@no", h.No);
            SqlParameter h3 = new SqlParameter("@name", h.Name);
            SqlParameter h4 = new SqlParameter("@area", h.Area);
            SqlParameter h5 = new SqlParameter("@city", h.City);
            SqlParameter h6 = new SqlParameter("@category", h.Category);
            SqlParameter h7 = new SqlParameter("@rent", h.Rent);
            SqlParameter h8 = new SqlParameter("@reqtenant", h.ReqTenant);


            cmd.Parameters.Add(h1);
            cmd.Parameters.Add(h2);
            cmd.Parameters.Add(h3);
            cmd.Parameters.Add(h4);
            cmd.Parameters.Add(h5);
            cmd.Parameters.Add(h6);
            cmd.Parameters.Add(h7);
            cmd.Parameters.Add(h8);
            cnn.Open();
            int reader = cmd.ExecuteNonQuery();
            cnn.Close();
            if (reader == 0)
            {
                NotFoundHouse nfh = new NotFoundHouse();
                nfh.Exception = "Can't update the house deatils please try after some time!!";
                throw new FaultException<NotFoundHouse>(nfh);
            }
            return h;
        }
    }
}
