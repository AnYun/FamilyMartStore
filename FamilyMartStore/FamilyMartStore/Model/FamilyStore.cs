using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyMartStore.Model
{
    /// <summary>
    /// 全家店鋪資料(接收 API 使用)
    /// </summary>
    public class FamilyStore
    {
        public string NAME { get; set; }
        public string TEL { get; set; }
        public string POSTel { get; set; }
        public double px { get; set; }
        public double py { get; set; }
        public string addr { get; set; }
        public double SERID { get; set; }
        public string pkey { get; set; }
        public string oldpkey { get; set; }
        public string post { get; set; }
        public string all { get; set; }
        public string road { get; set; }
        public object twoice { get; set; }
    }
}
