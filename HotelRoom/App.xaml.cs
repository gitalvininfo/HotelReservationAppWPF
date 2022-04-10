using HotelRoom.DbContexts;
using HotelRoom.Exceptions;
using HotelRoom.Models;
using HotelRoom.Services;
using HotelRoom.Services.ReservationConflictValidators;
using HotelRoom.Services.ReservationCreators;
using HotelRoom.Services.ReservationProviders;
using HotelRoom.Stores;
using HotelRoom.ViewModels;
using Microsoft.EntityFrameworkCore;
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
        private const string CONNECTION_STRING = "Data Source=reservroom.db";
        private readonly Hotel _hotel;
        private readonly NavigationStore _navigationStore;
        private ReservoomDbContextFactory _reservoomDbContextFactory;

        public App()
        {
            _reservoomDbContextFactory = new ReservoomDbContextFactory(CONNECTION_STRING);

            IReservationProvider reservationProvider = new DatabaseReservationProvider(_reservoomDbContextFactory);
            IReservationConflictValidator reservationConflictValidator = new DatabaseReservationConflictValidator(_reservoomDbContextFactory); ;
            IReservationCreator reservationCreator = new DatabaseReservationCreator(_reservoomDbContextFactory);
            ReservationBook reservationBook = new ReservationBook(reservationProvider, reservationCreator, reservationConflictValidator);
            _hotel = new Hotel("SingletonSean Suites", reservationBook);
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {


            using(ReservoomDbContext dbContext = _reservoomDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            _navigationStore.CurrentViewModel = CreateReservationViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();


            base.OnStartup(e);
        }

        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotel, new NavigationService(_navigationStore, CreateReservationViewModel));
        }

        private ReservationListingViewModel CreateReservationViewModel()
        {
            return ReservationListingViewModel.LoadViewModel(_hotel, new NavigationService(_navigationStore, CreateMakeReservationViewModel));
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