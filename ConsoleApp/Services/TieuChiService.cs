using ConsoleApp.DbContexts;
using ConsoleTables;
using Entities.DataEntities;
using Entities.Dto.TieuChis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ConsoleApp.Services
{
    public class TieuChiService
    {
        private readonly ApplicationDbContext _dbContext;

        public TieuChiService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int ShowMenu()
        {
            Console.WriteLine("--------------Quản lý tiêu chí--------------");
            Console.WriteLine("(1). Danh sách tiêu chí");
            Console.WriteLine("(2). Thêm");
            Console.WriteLine("(3). Trở lại");
            Console.WriteLine("--------------------------------------------");
            while (true)
            {
                Console.Write("Nhập vào lựa chọn: ");
                bool passChoose = int.TryParse(Console.ReadLine(), out int choose);
                if (passChoose && (new int[] { 1, 2, 3 }).Contains(choose))
                {
                    return choose;
                }
            }
        }

        public void XuLy()
        {
            while (true)
            {
                int choose = ShowMenu();
                if (choose == 1)
                {
                    ShowAll();
                }
                else if (choose == 2)
                {
                    Create();
                }
                else if (choose == 3)
                {
                    break;
                }
            }   
        }

        public void Create()
        {
            Console.WriteLine("Nhập vào thông tin tiêu chí:");
            Console.Write("Tên tiêu chí: ");
            TieuChi input = new();
            input.Name = Console.ReadLine();
            Console.Write("Kiểu tiêu chí: (1) Giá trị; (2) Ngoại ngữ: ");
            _=int.TryParse(Console.ReadLine(), out int type);
            input.Type = type;
            Console.Write("Giá trị tối đa: ");
            _=double.TryParse(Console.ReadLine(), out double maxValue);
            input.GiaTriMax = maxValue;
            _dbContext.TieuChis.Add(input);
            _dbContext.SaveChanges();
        }

        public void ShowAll()
        {
            Console.WriteLine("Tiêu chí đánh giá: ");
            var table = new ConsoleTable("Id", "Tên tiêu chí", "Giá trị tối đa", "Trọng số", "Có lấy max");
            var list = GetAll();
            foreach (var item in list)
            {
                table.AddRow(item.Id, item.Name, item.GiaTriMax.ToString("#,0.####"), item.TrongSo.ToString("0.####"), item.IsMax);
            }
            table.Write(Format.Alternative);
            Console.WriteLine();
        }

        public List<TieuChiDto> GetAll()
        {
            var tieuChiQuery = from tieuChiDanhGia in _dbContext.TieuChiDanhGias
                               join tieuChi in _dbContext.TieuChis on tieuChiDanhGia.TieuChiId equals tieuChi.Id
                               //where tieuChiDanhGia.DanhGiaId == danhGiaId
                               select new TieuChiDto
                               {
                                   Id = tieuChi.Id,
                                   Name = tieuChi.Name,
                                   GiaTriMax = tieuChi.GiaTriMax,
                                   TrongSo = tieuChiDanhGia.TrongSo,
                                   IsMax = tieuChi.IsMax,
                               };
            var tieuChis = tieuChiQuery.ToList();
            return tieuChis;
        }
    }
}