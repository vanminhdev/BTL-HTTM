using ConsoleApp.DbContexts;

namespace Domain
{
    public class TieuChiService
    {
        private readonly ApplicationDbContext _dbContext;

        public TieuChiService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void GetAll()
        {
            Console.WriteLine("---Danh sách tiêu chí---");
            _dbContext.TieuChis.ToList().ForEach(t =>
            {
                Console.WriteLine($"{t.Id,10}|{t.Name,40}|{t.Type,10}|{t.GiaTriMax,20}");
            });
        }
    }
}