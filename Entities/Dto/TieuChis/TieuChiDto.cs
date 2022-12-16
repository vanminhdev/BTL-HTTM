using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.TieuChis
{
    public class TieuChiDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double GiaTriMax { get; set; }
        public double TrongSo { get; set; }
        /// <summary>
        /// Có lấy max
        /// </summary>
        public bool IsMax { get; set; }
    }
}
