using Entities.DataEntities;
using Entities.Dto.LuaChonUngVien;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using ConsoleApp.DbContexts;
using ConsoleApp.Services;

namespace ConsoleApp
{
    public class Program
    {
        static int ShowMenu()
        {
            Console.WriteLine("--------------Menu--------------");
            Console.WriteLine("(1). Quản lý tiêu chí");
            Console.WriteLine("(2). Quản lý các trường");
            Console.WriteLine("(3). ");
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
            Console.Write("Nhập vào số lượng ứng viên: ");
            if (int.TryParse(Console.ReadLine(), out int n))
            {
                Console.Write("Giá trị không hợp lệ");
                return;
            }

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Nhập vào thông tin ứng viên thứ {i + 1}");
                Console.Write("Tên: ");
                string ten = Console.ReadLine();
                Console.Write("Địa chỉ: ");
                string diaChi = Console.ReadLine();
                ApplicationDbContext.UngViens.Add(new UngVienDto
                {
                    Id = ApplicationDbContext.UngVienId++,
                    Ten = ten,
                    DiaChi = diaChi
                });
            }
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

            foreach (var ungVien in ApplicationDbContext.UngViens)
            {
                Console.WriteLine($"Nhập thông tin cho ứng viên: {ungVien.Ten}");
                foreach (var tieuChi in tieuChis)
                {
                    Console.Write($"{tieuChi.Name}: ");
                    if (double.TryParse(Console.ReadLine(), out double giaTri))
                    {
                        Console.Write("Giá trị không hợp lệ");
                        return;
                    }
                    ApplicationDbContext.UngVienTieuChis.Add(new UngVienTieuChiDto
                    {
                        TieuChiId = tieuChi.Id,
                        UngVienId = ungVien.Id,
                        GiaTri = giaTri
                    });
                }
            }
        }

        static void TinhToanTheoTopsis(int danhGiaId = 0)
        {
            ApplicationDbContext dbContext = new();
            var tieuChiQuery = from tieuChiDanhGia in dbContext.TieuChiDanhGias
                               join tieuChi in dbContext.TieuChis on tieuChiDanhGia.TieuChiId equals tieuChi.Id
                               //where tieuChiDanhGia.DanhGiaId == danhGiaId
                               select new
                               {
                                   tieuChi.Id,
                                   tieuChi.Name,
                                   tieuChi.GiaTriMax,
                                   tieuChiDanhGia.TrongSo
                               };
            var tieuChis = tieuChiQuery.ToList();

            //chuẩn hoá
            foreach (var giaTri in ApplicationDbContext.UngVienTieuChis)
            {
                var tieuChi = tieuChis.FirstOrDefault(t => t.Id == giaTri.TieuChiId);
                ApplicationDbContext.MaTranChuanHoa.Add(new UngVienTieuChiDto
                {
                    TieuChiId = giaTri.TieuChiId,
                    UngVienId = giaTri.UngVienId,
                    GiaTri = giaTri.GiaTri/giaTri.GiaTri
                });
            }

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
        }

        static void ShowMaTran(List<UngVienTieuChiDto> input)
        {
            foreach (var ungVien in input.GroupBy(o => o.UngVienId))
            {
                var ungVienFind = ApplicationDbContext.UngViens.FirstOrDefault(u => u.Id == ungVien.Key);
                Console.Write($"{ungVienFind.Ten,30}| ");
                foreach (var tieuChi in ungVien)
                {
                    Console.Write($"{tieuChi.GiaTri,-10}");
                }
                Console.WriteLine();
            }
        }

        static void ShowMaTran(List<double> input)
        {
            Console.Write("(");
            foreach (var giaTri in input)
            {
                Console.Write($"{giaTri,-10:N2},");
            }
            Console.Write(")");
            Console.WriteLine();
        }

        static void ChonUngVien()
        {
            NhapThongTinUngVien();
            NhapThongTinDanhGia();
            TinhToanTheoTopsis();
        }

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            TieuChiService tieuChiService = new(new ApplicationDbContext());
            while (true)
            {
                int choose = ShowMenu();
                if (choose == 1)
                {
                    tieuChiService.XuLy();
                }
                else if (choose == 2)
                {

                }
                else if (choose == 3)
                {

                }
                else if (choose == 4)
                {
                    ChonUngVien();
                }
                Console.WriteLine("Tiếp tục, nhấn enter.");
                Console.ReadLine();
            }
        }
    }
}

