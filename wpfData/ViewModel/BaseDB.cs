﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfData_Step_4.Model;

namespace wpfData_Step_4.ViewModel
{
    public abstract class BaseDB
    {
        protected OleDbConnection connection;
        protected OleDbCommand command;
        protected OleDbDataReader reader;

        protected abstract BaseEntity NewEntity();
        protected abstract BaseEntity CreateModel(BaseEntity entity);

        protected static string connectionString;
        public BaseDB()
        {
            if (connectionString == null)
            {
                connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                    Path() + @"\SchoolDB.accdb;Persist Security Info=True";
            }
            connection = new OleDbConnection(connectionString);
            command = new OleDbCommand();
            command.Connection = connection;
        }

        public List<BaseEntity> ExecuteCommand() //עבודה וניהול התקשורת מול המסד
        {
            List<BaseEntity> list = new List<BaseEntity>();
            try
            {
                connection.Open(); //פתיחת תקשורת עם המסד
                reader = command.ExecuteReader(); //ביצוע השאילתה
                while (reader.Read()) //מעבר על כל התוצאות
                {
                    BaseEntity entity = NewEntity(); //יצירת עצם חדש מותאם לצורך הנוכחי
                    list.Add(CreateModel(entity)); //מילוי העצם בתכונות מותאמות
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return list;
        }

        private static string Path()
        {
            string s = Environment.CurrentDirectory; //המיקום שבו רץ הפרויקט
            string[] sub = s.Split('\\'); //פירוק מחרוזת הכתובת למערך לפי תיקיות
            
            int index = sub.Length - 2; //חזרה אחורה 2 תיקיות
            sub[index] = "ViewModel";     //שינוי התיקיה לתיקיה המתאימה
            Array.Resize(ref sub, index + 1); //תיקון של אורך המערך, לאורך המתאים לתיקייה

            s = String.Join("\\", sub);  //חיבור מחדש של המערך עם / מפריד אישי 
            return s;
        }

        public int SaveChanges() //ביצוע שינויים במסד
        {
            int records=0;
            try
            {
                connection.Open(); //פתיחת תקשורת עם המסד
                records = command.ExecuteNonQuery(); //ביצוע שאילתת פעולה
               
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message +"\n"+command.CommandText);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return records;
        }
    }
}
