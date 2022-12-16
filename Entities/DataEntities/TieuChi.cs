using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataEntities
{
    [Table(nameof(TieuChi))]
    public class TieuChi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Column(nameof(Name))]
        public string Name { get; set; }

        public double GiaTriMax { get; set; }

        public int Type { get; set; }

        public bool IsMax { get; set; }
    }
}
