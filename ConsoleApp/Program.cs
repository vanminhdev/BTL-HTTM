using Entities.DataEntities;
using Entities.Dto.LuaChonUngVien;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using ConsoleApp.DbContexts;
using ConsoleApp.Services;
using ConsoleTables;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ConsoleApp
{
    public class Program
    {
        static TieuChiService _tieuChiService = new(new ApplicationDbContext());

        static int ShowMenu()
        {
            Console.WriteLine("--------------Menu--------------");
            Console.WriteLine("(1). Quản lý tiêu chí");
            Console.WriteLine("(2). Quản lý lựa chọn học vấn");
            Console.WriteLine("(3). Quản lý chứng chỉ");
            Console.WriteLine("(4). Lựa chọn ứng viên");
            Console.WriteLine("--------------------------------");
            while (true)
            {
                Console.Write("Nhập vào lựa chọn: ");
                bool passChoose = int.TryParse(Console.ReadLine(), out int choose);
                if (passChoose && (new int[] { 1, 2, 3, 4 }).Contains(choose))
                {
                    return choose;
                }
            }
        }

        static void NhapThongTinUngVien()
        {
            ApplicationDbContext.UngViens.Add(new UngVienDto
            {
                Id = ApplicationDbContext.UngVienId++,
                Ten = "Nguyễn Văn A",
                DiaChi = "Cầu Giấy"
            });

            ApplicationDbContext.UngViens.Add(new UngVienDto
            {
                Id = ApplicationDbContext.UngVienId++,
                Ten = "Nguyễn Văn B",
                DiaChi = "Hà Đông"
            });

            ApplicationDbContext.UngViens.Add(new UngVienDto
            {
                Id = ApplicationDbContext.UngVienId++,
                Ten = "Nguyễn Văn C",
                DiaChi = "Hoàng Mai"
            });

            ApplicationDbContext.UngViens.Add(new UngVienDto
            {
                Id = ApplicationDbContext.UngVienId++,
                Ten = "Nguyễn Văn D",
                DiaChi = "Hai Bà Trưng"
            });

            //Console.Write("Nhập vào số lượng ứng viên: ");
            //if (int.TryParse(Console.ReadLine(), out int n))
            //{
            //    Console.Write("Giá trị không hợp lệ");
            //    return;
            //}

            //for (int i = 0; i < n; i++)
            //{
            //    Console.WriteLine($"Nhập vào thông tin ứng viên thứ {i + 1}");
            //    Console.Write("Tên: ");
            //    string ten = Console.ReadLine();
            //    Console.Write("Địa chỉ: ");
            //    string diaChi = Console.ReadLine();
            //    ApplicationDbContext.UngViens.Add(new UngVienDto
            //    {
            //        Id = ApplicationDbContext.UngVienId++,
            //        Ten = ten,
            //        DiaChi = diaChi
            //    });
            //}
            Console.WriteLine("Thông tin các ứng viên:");
            ShowMaTran(ApplicationDbContext.UngViens);
        }

        static void NhapThongTinDanhGia(int danhGiaId = 0)
        {
            ApplicationDbContext dbContext = new();
            //var danhGia = dbContext.TieuChiDanhGias.FirstOrDefault(t => t.Id == danhGiaId);

            var tieuChiQuery = from tieuChiDanhGia in dbContext.TieuChiDanhGias
                           join tieuChi in dbContext.TieuChis on tieuChiDanhGia.TieuChiId equals tieuChi.Id
                           //where tieuChiDanhGia.DanhGiaId == danhGia.Id
                           select new
                           {
                               tieuChi.Id,
                               tieuChi.Name,
                               tieuChi.GiaTriMax,
                               tieuChiDanhGia.TrongSo
                           };
            var tieuChis = tieuChiQuery.ToList();

            //1
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(1, 1, 10000000));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(1, 2, 4));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(1, 3, 3));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(1, 4, 3));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(1, 5, 8));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(1, 6, 1));
            //2
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(2, 1, 12000000));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(2, 2, 5));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(2, 3, 2));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(2, 4, 3));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(2, 5, 9));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(2, 6, 0));
            //3
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(3, 1, 8000000));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(3, 2, 2));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(3, 3, 6));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(3, 4, 2));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(3, 5, 8));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(3, 6, 0));
            //4
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(4, 1, 8500000));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(4, 2, 3));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(4, 3, 6));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(4, 4, 3));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(4, 5, 7));
            ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto(4, 6, 1));

            //foreach (var ungVien in ApplicationDbContext.UngViens)
            //{
            //    Console.WriteLine($"Nhập thông tin cho ứng viên: {ungVien.Ten}");
            //    foreach (var tieuChi in tieuChis)
            //    {
            //        Console.Write($"{tieuChi.Name}: ");
            //        double giaTri = double.Parse(Console.ReadLine());
            //        ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto
            //        {
            //            TieuChiId = tieuChi.Id,
            //            UngVienId = ungVien.Id,
            //            GiaTri = giaTri
            //        });
            //    }
            //}

            Console.WriteLine("Thông tin lựa chon:");
            ShowMaTran(ApplicationDbContext.UngVienTieuChis);
        }

        static void TinhToanTheoTopsis(int danhGiaId = 0)
        {
            ApplicationDbContext dbContext = new();
            var tieuChis = _tieuChiService.GetAll();

            //chuẩn hoá
            foreach (var giaTri in ApplicationDbContext.UngVienTieuChis)
            {
                var tieuChi = tieuChis.FirstOrDefault(t => t.Id == giaTri.TieuChiId);
                ApplicationDbContext.MaTranChuanHoa.Add(new UngVienTieuChiDto
                {
                    TieuChiId = giaTri.TieuChiId,
                    UngVienId = giaTri.UngVienId,
                    GiaTri = tieuChi.IsMax ? giaTri.GiaTri/tieuChi.GiaTriMax : 1 - (giaTri.GiaTri/tieuChi.GiaTriMax)
                });
            }
            Console.WriteLine("Chuẩn hoá");
            ShowMaTran(ApplicationDbContext.MaTranChuanHoa);
            //tính giá trị theo trọng số
            foreach (var giaTriChuan in ApplicationDbContext.MaTranChuanHoa)
            {
                var tieuChi = tieuChis.FirstOrDefault(t => t.Id == giaTriChuan.TieuChiId);
                ApplicationDbContext.GiaTriTheoTrongSo.Add(new UngVienTieuChiDto
                {
                    TieuChiId = giaTriChuan.TieuChiId,
                    UngVienId = giaTriChuan.UngVienId,
                    GiaTri = giaTriChuan.GiaTri * tieuChi.TrongSo
                });
            }
            Console.WriteLine("Trọng số");
            ShowMaTran(ApplicationDbContext.GiaTriTheoTrongSo);
            //tính A sao
            foreach (var tieuChi in tieuChis)
            {
                double max = ApplicationDbContext.MaTranChuanHoa.Where(m => m.TieuChiId == tieuChi.Id).Max(m => m.GiaTri);
                ApplicationDbContext.ASao.Add(max);
            }
            Console.WriteLine("A*");
            ShowMaTran(ApplicationDbContext.ASao);
            //tính A trừ
            foreach (var tieuChi in tieuChis)
            {
                double min = ApplicationDbContext.MaTranChuanHoa.Where(m => m.TieuChiId == tieuChi.Id).Min(m => m.GiaTri);
                ApplicationDbContext.ATru.Add(min);
            }
            Console.WriteLine("A-");
            ShowMaTran(ApplicationDbContext.ATru);
            //tính S sao, S trừ
            foreach (var ungVien in ApplicationDbContext.UngViens)
            {
                //giá trị của ứng viên với các tiêu chí
                var giaTris = ApplicationDbContext.MaTranChuanHoa.Where(m => m.UngVienId == ungVien.Id).ToList();
                double sumSao = 0;
                double sumTru = 0;
                for (int i = 0; i < giaTris.Count; i++)
                {
                    sumSao += Math.Pow((giaTris[i].GiaTri - ApplicationDbContext.ASao[i]), 2);
                    sumTru += Math.Pow((giaTris[i].GiaTri - ApplicationDbContext.ATru[i]), 2);
                }
                ApplicationDbContext.SSao.Add(Math.Sqrt(sumSao));
                ApplicationDbContext.Stru.Add(Math.Sqrt(sumTru));
            }
            Console.WriteLine("S*");
            ShowMaTran(ApplicationDbContext.SSao);
            Console.WriteLine("S-");
            ShowMaTran(ApplicationDbContext.Stru);
            //tính C
            for (int i = 0; i < ApplicationDbContext.SSao.Count; i++)
            {
                ApplicationDbContext.CSao.Add(ApplicationDbContext.Stru[i]/(ApplicationDbContext.SSao[i] + ApplicationDbContext.Stru[i]));
            }
            Console.WriteLine("C*");
            ShowMaTran(ApplicationDbContext.CSao);

            double maxC = ApplicationDbContext.CSao.Max();
            int index = ApplicationDbContext.CSao.FindIndex(c => c == maxC);
            var ungVienLuaChon = ApplicationDbContext.UngViens[index];
            Console.WriteLine($"Lựa chọn ứng viên: {ungVienLuaChon.Ten}");
        }

        static void ShowMaTran(List<UngVienDto> input)
        {
            List<string> column = new() { "Id", "Tên", "Địa chỉ" };
            var table = new ConsoleTable(column.ToArray());
            foreach (var ungVien in input)
            {
                table.AddRow(ungVien.Id, ungVien.Ten, ungVien.DiaChi);
            }
            table.Write(Format.Alternative);
            Console.WriteLine();
        }

        static void ShowMaTran(List<UngVienTieuChiDto> input)
        {
            var tieuChis = _tieuChiService.GetAll();

            List<string> column = new() { "Tên" };
            column.AddRange(tieuChis.Select(t => t.Name));
            var table = new ConsoleTable(column.ToArray());
            var list = input.GroupBy(o => o.UngVienId).ToList();
            foreach (var ungVien in list)
            {
                var ungVienFind = ApplicationDbContext.UngViens.FirstOrDefault(u => u.Id == ungVien.Key);
                List<string> row = new();
                row.Add(ungVienFind.Ten);
                var test = ungVien.ToList();
                foreach (var tieuChi in ungVien.ToList())
                {
                    row.Add(tieuChi.GiaTri.ToString("#,0.####"));
                }
                table.AddRow(row.ToArray());
            }
            table.Write(Format.Alternative);
            Console.WriteLine();
        }

        static void ShowMaTran(List<double> input)
        {
            var table = new ConsoleTable(input.Select(o => o.ToString("0.####")).ToArray());
            table.Write(Format.Alternative);
            Console.WriteLine();
        }

        static void ChonUngVien()
        {
            _tieuChiService.ShowAll();
            NhapThongTinUngVien();
            NhapThongTinDanhGia();
            TinhToanTheoTopsis();
        }

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            (new ApplicationDbContext()).TieuChis.ToList();
            ChonUngVien();
            //while (true)
            //{
            //    int choose = ShowMenu();
            //    if (choose == 1)
            //    {
            //        _tieuChiService.XuLy();
            //    }
            //    else if (choose == 2)
            //    {

            //    }
            //    else if (choose == 3)
            //    {

            //    }
            //    else if (choose == 4)
            //    {
            //        ChonUngVien();
            //    }
            //    Console.WriteLine("Tiếp tục, nhấn enter.");
            //    Console.ReadLine();
            //}
        }
    }
}

