using AutoMapper;
using DYV.Models;
using DYV.Models.AccountViewModels;
using DYV.Models.ClientRelations;
using DYV.Models.PlacesViewModels;
using DYV.Models.PlaceViewModels;
using DYV.Models.PracticeViewModels;
using DYV.Models.Purchase;
using DYV.Models.Purchase.ViewModels;
using DYV.Models.SubscriberDashboard;
using DYV.Models.User;
using DYV.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //PLACE
            // place => vm
            CreateMap<Place, PlaceListItemViewModel>()
                .ForMember<string>(
                    p => p.DateAdded,
                    p => p.MapFrom(
                        q => q.DateAdded.ToShortDateString()));

            CreateMap<Place, PlaceSubscriberListItemViewModel>()
                .ForMember<string>(
                    p => p.ClientName,
                    p => p.MapFrom(
                        q => q.ClientUser.Name))
                .ForMember<string>(
                    p=> p.DateAdded,
                    p=> p.MapFrom(
                        q => q.DateAdded.ToShortDateString()));

            CreateMap<Place, PlaceDetailsViewModel>();
            CreateMap<Place, EditPlaceViewModel>();
            CreateMap<Place, DeletePlaceViewModel>();
            CreateMap<Place, DayListPlaceViewModel>();

            // vm => place
            CreateMap<AddPlaceViewModel, Place>();

            //PRACTICE
            // practice => vm
            CreateMap<Practice, PracticeListItemViewModel>()
                .ForMember<int>(
                    p => p.NumClients, 
                    p => p.MapFrom(
                        q => q.ClientPractices.Count))
                .ForMember<int>(
                    p => p.NumSubscribers,
                    p => p.MapFrom(
                        q => q.SubscriberPractices.Count));

            CreateMap<Practice, SelectListItem>()
                .ForMember<string>(
                    sli => sli.Text, 
                    p => p.MapFrom(
                        q => q.Name))
                .ForMember<string>(
                    sli => sli.Value, 
                    p => p.MapFrom(
                        q => q.Id.ToString()));


            CreateMap<Practice, EditPracticeViewModel>();

            CreateMap<Practice, UnsharePlacesViewModel>();
            CreateMap<Practice, PracticeDetailsViewModel>();

            CreateMap<Practice, ClientRelationsIndexViewModel>()
                .ForMember<int>(
                    p => p.EmailQuota,
                    m => m.MapFrom(
                        p => p.GetMsgQuota(PurchaseType.Email, DateTime.Now)))
                .ForMember<int>(
                    p => p.SMSQuota,
                    m => m.MapFrom(
                        p => p.GetMsgQuota(PurchaseType.SMS, DateTime.Now)));

            // vm => practice
            CreateMap<AddPracticeViewModel, Practice>();

            //DAYLIST
            CreateMap<DayList, DayListViewModel>()
                .ForMember<List<DayListPlaceViewModel>>(
                    m => m.dayListPlaces,
                    m => m.MapFrom(
                        p => p.PlaceDayLists.Select(
                            q =>
                                new DayListPlaceViewModel(q.Place, q.ZoomLevel, q.DayListId, q.Place.ClientUser)
                            )))
                .ForMember<string>(
                    m => m.dateAdded,
                    m => m.MapFrom(
                        p => p.DateAdded.ToShortDateString()));

            //USER
            CreateMap<SubscriberUser, SubscriberUserViewModel>();

            CreateMap<PurchaseCost, PurchaseCostItemViewModel>()
                .ForMember<string>(
                    m => m.Text,
                    p => p.MapFrom(
                        q => q.PurchaseType.ToString() + " x " + q.Quantity.ToString() + " = £" + q.Price
                        )
                    )
                .ForMember<int>(
                    m => m.Id,
                    p => p.MapFrom(
                        q => q.Id
                        )
                    );

            CreateMap<MessageGroupResult, GroupSendResultViewModel>()
                .ForMember<string>(
                    m => m.SenderName,
                    m => m.MapFrom(
                        p => p.SubscriberUser.Name));

            CreateMap<SMSSendResult, SMSSendResultViewModel>();
            CreateMap<EmailSendResult, EmailSendResultViewModel>();

        }
    }
}
