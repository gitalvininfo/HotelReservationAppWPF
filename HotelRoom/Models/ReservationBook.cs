using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoom.Models
{
    public class ReservationBook
    {

        private readonly Dictionary<RoomID, List<Reservation>> _roomToReservations;


        public ReservationBook()
        {
            _roomToReservations = new Dictionary<RoomID, List<Reservation>>();
        }

    }
}
