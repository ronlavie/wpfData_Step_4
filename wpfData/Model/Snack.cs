using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfData_Step_4.Model;
using wpfData_Step_4.ViewModel;

namespace wpfData_Step_4.Model
{
    public class Snack: BaseEntity
    {
        private string snack;

        public string SnackName
        {
            get { return snack; }
            set { snack = value; }
        }

    }
    public class  SnackList : List<Snack>
    {
        public SnackList() { } //בנאי ברירת מחדל
        public SnackList(IEnumerable<Snack> list) :
            base(list)
        { } //המרה של רשימת קורסים לאוסף של קורסים
        public SnackList(IEnumerable<BaseEntity> list) :
            base(list.Cast<Snack>().ToList())
        { } //המרה כלפי מטה מישות בסיס לרשימת קורסים

    }

}
