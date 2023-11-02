using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfData_Step_4.Model;

namespace wpfData_Step_4.ViewModel
{
    public class SnackDB : BaseDB
    {
        protected override BaseEntity NewEntity()
        {
            return new Snack() as BaseEntity; //יצירת עצם חדש מסוג
        }

        //Reader-מילוי העצם בערכים מה
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Snack Snack = (Snack)entity;
            Snack.Id = (int)this.reader["ID"];
            Snack.SnackName = (string)this.reader["SnackName"];
            return Snack;
        }

        public SnackList SelectAll()
        {
            command.CommandText = "SELECT * FROM TblSnack";
            SnackList SnackList = new SnackList(base.ExecuteCommand());
            return SnackList;
        }

        public Snack SelectById(int id)
        {
            command.CommandText = string.Format("SELECT * FROM TblSnack WHERE ID={0}", id);
            SnackList SnackList = new SnackList(base.ExecuteCommand());
            if (SnackList.Count == 0)
                return null;
            return SnackList[0];
        }

        public SnackList SelectByUser(User user)
        {
            this.command.CommandText = $"SELECT tblSnacks.Id, tblSnacks.name FROM (TblUserSnack INNER JOIN tblSnacks ON TblUserSnack.SnackId = tblSnacks.Id) WHERE (TblUserSnack.UserId = {user.Id})";
            SnackList snacklist = new SnackList(base.ExecuteCommand());
            return snacklist;   
        }
    } 
}