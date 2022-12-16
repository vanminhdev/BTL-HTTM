namespace Entities.Dto.LuaChonUngVien
{
    public class UngVienTieuChiDto
    {
        public int UngVienId { get; set; }
        public int TieuChiId { get; set; }
        public double GiaTri { get; set; }

        public UngVienTieuChiDto()
        {
        }

        public UngVienTieuChiDto(int ungVienId, int tieuChiId, double giaTri)
        {
            UngVienId = ungVienId;
            TieuChiId = tieuChiId;
            GiaTri = giaTri;
        }
    }
}
