using AutoMapper;
using System.Security.Principal;
using System;
using BussinessObjects.Entities;
using Services.DTO.Request;
using Services.DTO.Response;

namespace Kacy_Backend.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CreateCustomerRequest, Customer>().ReverseMap();
            CreateMap<CustomerResponse, Customer>().ReverseMap();
            CreateMap<CustomerRequest, CustomerResponse>().ReverseMap();
            CreateMap<CreateStaffRequest, Staff>();
            CreateMap<Staff, StaffResponse>();
            CreateMap<StaffRequest, StaffResponse>();
            CreateMap<Admin, AdminResponse>();
            CreateMap<CustomerInRoom, CustomerInRoomResponse>();
            CreateMap<PackOfCustomer, PackOfCustomerResponse>();
            CreateMap<Event, EventResponse>();
            CreateMap<CreateEventRequest, Event>();
            CreateMap<EventRequest, EventResponse>();
            CreateMap<Pack, PackResponse>();
            CreateMap<CreatePackRequest, Pack>();
            CreateMap<PackRequest, PackResponse>();
            CreateMap<CreateRoomRequest, Room>(); 
            CreateMap<RoomRequest, RoomResponse>();
            CreateMap<Room, RoomResponse>();
            CreateMap<RoomRequest, Room>();
            CreateMap<CustomerInRoom, CustomerInRoomResponse>();
            CreateMap<CreateCustomerRequest, Room>();
            CreateMap<AttendanceRequest, AttendanceResponse>();
            CreateMap<Attendance, AttendanceResponse>();
            CreateMap<CheckAttendanceRequest, Attendance>();
            CreateMap<CreateCustomerInRoomRequest, CustomerInRoom>();
            CreateMap<PackOfCustomer, PackOfCustomerResponse>();
            CreateMap<BuyPackAndCreateTransactionRequest, BuyPackRequest>();
            CreateMap<BuyPackAndCreateTransactionRequest, CreateTransactionRequest>();
            CreateMap<CreateTransactionRequest, Transaction>();
            CreateMap<Transaction, TransactionResponse>();
            CreateMap<TransactionRequest, TransactionResponse>();
        }
    }
}
