using ConsoleApp.DbContexts;
using Entities.DataEntities;

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
                    GetAll();
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
            int.TryParse(Console.ReadLine(), out int type);
            input.Type = type;
            Console.Write("Giá trị tối đa: ");
            double.TryParse(Console.ReadLine(), out double maxValue);
            input.GiaTriMax = maxValue;
            _dbContext.TieuChis.Add(input);
            _dbContext.SaveChanges();
        }

        public void GetAll()
        {
            Console.WriteLine($"{"ID",10}|{"Tên tiêu chí",40}|{"Loại",10}|{"Giá trị tối đa",20}");
            var list = _dbContext.TieuChis.ToList();
            foreach (var item in list)
            {
                Console.WriteLine($"{item.Id,10}|{item.Name,40}|{item.Type,10}|{item.GiaTriMax,20}");
            }
        }
    }
}