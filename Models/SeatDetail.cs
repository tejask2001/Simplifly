using System.ComponentModel.DataAnnotations;

namespace Simplifly.Models
{
    public class SeatDetail:IEquatable<SeatDetail>
    {
        [Key]
        public string SeatNumber { get; set; }
        public string SeatClass { get; set;} = string.Empty;
        public string status { get; set; } = string.Empty;

        public SeatDetail()
        {
            
        }

        public SeatDetail(string seatNumber, string seatClass, string status)
        {
            SeatNumber = seatNumber;
            SeatClass = seatClass;
            this.status = status;
        }

        public bool Equals(SeatDetail? other)
        {
            var seatDetail = other ?? new SeatDetail();
            return this.SeatNumber.Equals(seatDetail.SeatNumber);
        }
    }
}
