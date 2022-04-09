using HotelRoom.Exceptions;
using HotelRoom.Models;
using HotelRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HotelRoom
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private readonly Hotel _hotel;

        public App()
        {
            _hotel = new Hotel("SingletonSean Suites");
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_hotel)
            };

            MainWindow.Show();


            base.OnStartup(e);
        }
    }
}



//Hotel hotel = new Hotel("SingletonSean Suites");


//try
//{

//    hotel.MakeReservation(new Reservation(
//        new RoomID(1, 3),
//        "SingletonSean",
//        new DateTime(2000, 1, 1),
//        new DateTime(2000, 1, 2)));

//    hotel.MakeReservation(new Reservation(
//        new RoomID(1, 3),
//        "SingletonSean",
//        new DateTime(2000, 1, 1),
//        new DateTime(2000, 1, 4)));
//} 
//catch (ReservationConflictException ex)
//{

//}

//IEnumerable<Reservation> reservations = hotel.GetReservationsForUser("SingletonSean");