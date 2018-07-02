﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace QLNV.DataLayer
{
    class DataAccessHelper
    {
        static DataAccessHelper instance;

        public static DataAccessHelper Instance{
            get
            {
                if (instance == null)
                    instance = new DataAccessHelper();
                return instance;
            }
        }

        static DataClassesDataContext db;
        public static DataClassesDataContext DB
        {
            get
            {
                if (db == null)
                    db = new DataClassesDataContext();
                return db;
            }
        }

        #region Data access properties
        public SqlConnection con;
        SqlCommand cmd;
        public DataTable dt;

        #endregion

        #region Init properties
        public DataAccessHelper()
        {
            String connectionString = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=QLNV;Data Source=DESKTOP-29U4V2H\SQLEXPRESS";
            con = new SqlConnection(connectionString);
        }
        #endregion

        #region Procceed with database
        public void Open()
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
        }

        private void Close()
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
        }

        public DataTable GetDataTable(string select)
        {
            SqlDataAdapter da = new SqlDataAdapter(select, con);
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        #endregion

        public SqlCommand Command(String commandString)
        {
            this.cmd = new SqlCommand(commandString, con);
            return cmd;
        }
    }
}
