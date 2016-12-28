using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using DYV.Data;
using DYV.Models.PlacesViewModels;
using DYV.Models;
using DYV.Services.EFUpdate;
using DYV.Models.PracticeViewModels;
using DYV.Models.PlaceViewModels;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using DYV.Models.Ajax;
using System.Linq.Expressions;

namespace DYV.Services.Places
{
    public class PlacesService : IPlacesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateProvider _dateProvider;
        private readonly IUserProvider _userProvider;

        public PlacesService(ApplicationDbContext context, IMapper mapper, IDateProvider dateProvider, IUserProvider userProvider)
        {
            _context = context;
            _mapper = mapper;
            _dateProvider = dateProvider;
            _userProvider = userProvider;
        }

        public async Task AddNewPlace(AddPlaceViewModel placeModel)
        {
            var place = _mapper.Map<AddPlaceViewModel, Place>(placeModel);
            place.DateAdded = _dateProvider.GetCurrentDateTime();
            place.ClientUserId = _userProvider.GetUserId();
            _context.Places.Add(place);
            await _context.SaveChangesAsync();
        }

        public async Task<EditPlaceViewModel> GetEditPlaceViewModelById(int placeId)
        {
            var p = await
                _context.Places
                .Where(place =>
                    place.Id == placeId && place.ClientUserId == _userProvider.GetUserId()
                    )
                    .ProjectTo<EditPlaceViewModel>()
                    .FirstOrDefaultAsync();

            return p;
        }

        public async Task<PlaceDetailsViewModel> GetPlaceById(int placeId, string returnController)
        {
            var pvm = await _context.Places
                .Where(place =>
                    place.Id == placeId && 
                    (place.ClientUserId == _userProvider.GetUserId() || place.ClientUser.PermittedPractices.Any(prac => prac.Practice.SubscriberPractices.Any(s => s.SubscriberUserId == _userProvider.GetUserId())))
                )
                .ProjectTo<PlaceDetailsViewModel>()
                .FirstOrDefaultAsync();

            pvm.ReturnController = returnController;

            return pvm;
        }

        public async Task<PlaceDetailsViewModel> GetPlaceByIdForAdmin(int placeId, string returnController)
        {
            var pvm = await _context.Places
                .Where(place =>
                    place.Id == placeId
                )
                .ProjectTo<PlaceDetailsViewModel>()
                .FirstOrDefaultAsync();

            pvm.ReturnController = returnController;

            return pvm;
        }

        public async Task<PlaceDetailsViewModel> GetPlaceById(int placeId)
        {
            return await GetPlaceById(placeId, null);
        }

        public async Task<PlaceDetailsViewModel> GetPlaceByIdForAdmin(int placeId)
        {
            return await GetPlaceByIdForAdmin(placeId, null);
        }

        public async Task<PlacesListViewModel<PlaceListItemViewModel>> GetPlacesListViewModelForCurrentUser()
        {
            return new PlacesListViewModel<PlaceListItemViewModel>()
            {
                Places =
                    await
                    _context.Places
                    .Where(place =>
                        place.ClientUserId == _userProvider.GetUserId()
                    )
                    .ProjectTo<PlaceListItemViewModel>()
                    .Take(10)
                    .ToListAsync(),                
                Practices =
                await
                    _context.Practices
                    .Where(practice =>
                        practice.ClientPractices.Any(
                            cp => cp.ClientUserId == _userProvider.GetUserId()))
                    .ProjectTo<PracticeListItemViewModel>()
                    .ToListAsync()
            };                
        }

        public async Task UpdatePlace(EditPlaceViewModel placeModel)
        {
            var place = await _context.Places.Where(p => p.Id == placeModel.Id && p.ClientUserId == _userProvider.GetUserId()).FirstOrDefaultAsync();

            if (place == null)
                return;

            place.UpdateProperties(placeModel);

            await _context.SaveChangesAsync();
        }

        public async Task<SharePlacesViewModel> GetSharePlacesViewModel()
        {
            return new SharePlacesViewModel()
            {
                Practices = await _context.Practices.Where(practice => !practice.ClientPractices.Any(cp => cp.ClientUserId == _userProvider.GetUserId())).ProjectTo<SelectListItem>().ToListAsync()
            };
        }

        public async Task AddSharePlaces(SharePlacesViewModel sharePlacesViewModel)
        {
            var clientPractice = new ClientPractices() { ClientUserId = _userProvider.GetUserId(), PracticeId = int.Parse(sharePlacesViewModel.SelectedPracticeId) };
            _context.ClientPractices.Add(clientPractice);

            await _context.SaveChangesAsync();
        }

        public async Task AddSharePlaceAtRegistration(string userId, int practiceId)
        {
            var clientPractice = new ClientPractices() { ClientUserId = userId, PracticeId = practiceId };
            _context.ClientPractices.Add(clientPractice);

            await _context.SaveChangesAsync();
        }

        public async Task<UnsharePlacesViewModel> GetUnshareViewModel(int practiceId)
        {
            return (await _context.Practices.Where(pr => pr.Id == practiceId).ProjectTo<UnsharePlacesViewModel>().FirstOrDefaultAsync());
        }

        public Task<DeletePlaceViewModel> GetDeletePlaceViewModelById(int value)
        {
            return _context.Places.Where(place => place.Id == value && place.ClientUserId == _userProvider.GetUserId()).ProjectTo<DeletePlaceViewModel>().SingleOrDefaultAsync();
        }

        public async Task DeletePlace(int id)
        {
            var place = await _context.Places.Where(p => p.Id == id && p.ClientUserId == _userProvider.GetUserId()).SingleOrDefaultAsync();

            if (place == null)
                return;

            _context.Places.Remove(place);

            await _context.SaveChangesAsync();
        }

        public async Task<PlacesListViewModel<PlaceSubscriberListItemViewModel>> GetPlacesListViewModelForCurrentSubscriber()
        {
            return await GetPlacesListViewModelForCurrentSubscriber(null);
        }

        public async Task<PlacesListViewModel<PlaceSubscriberListItemViewModel>> GetPlacesListViewModelForCurrentSubscriber(SubscriberDashboardAjaxSearchViewModel vm)
        {
            Expression<Func<Place, bool>> predicate;
            int page = vm == null ? 0 : vm.Page;
            int take = vm == null ? 5 : vm.Take;

            if (vm == null)
            {
                predicate = x => true;
            }
            else
            {
                predicate = place => (
                    place.Name.ToLower().Contains(vm.PlaceNameTerm) &&
                    place.ClientUser.Name.ToLower().Contains(vm.ClientNameTerm) &&
                        (
                        place.Address1.ToLower().Contains(vm.AddressTerm) ||
                        place.PostCode.ToLower().Contains(vm.AddressTerm) ||
                        place.City.ToLower().Contains(vm.AddressTerm) ||
                        place.County.ToLower().Contains(vm.AddressTerm) ||
                        place.Country.ToLower().Contains(vm.AddressTerm)
                        )
                    );
            }

            var q = _context.Places
                    .Where(place =>
                        place.ClientUser.PermittedPractices.Any(prac =>
                            prac.Practice.SubscriberPractices.Any(sp =>
                                sp.SubscriberUserId == _userProvider.GetUserId()))
                    )
                    .Where(predicate)
                    .ProjectTo<PlaceSubscriberListItemViewModel>();

            return new PlacesListViewModel<PlaceSubscriberListItemViewModel>()
            {
                Places =
                    await
                    q
                    .Skip(page * take)
                    .Take(take)
                    .ToListAsync(),
                Practices = null,
                TotalResults = q.Count()
            };
        }

        public Task<PlacesListViewModel<PlaceSubscriberListItemViewModel>> GetPlacesListViewModelForPracticeIfAdmin(int practiceId)
        {
            return GetPlacesListViewModelForPracticeIfAdmin(practiceId, null);
        }

        public async Task<PlacesListViewModel<PlaceSubscriberListItemViewModel>> GetPlacesListViewModelForPracticeIfAdmin(int practiceId, SubscriberDashboardAjaxSearchViewModel vm)
        {
            Expression<Func<Place, bool>> predicate;
            int page = vm == null ? 0 : vm.Page;
            int take = vm == null ? 5 : vm.Take;

            if (vm == null)
            {
                predicate = x => true;
            }
            else
            {
                predicate = place => (
                    place.Name.ToLower().Contains(vm.PlaceNameTerm) &&
                    place.ClientUser.Name.ToLower().Contains(vm.ClientNameTerm) &&
                        (
                        place.Address1.ToLower().Contains(vm.AddressTerm) ||
                        place.PostCode.ToLower().Contains(vm.AddressTerm) ||
                        place.City.ToLower().Contains(vm.AddressTerm) ||
                        place.County.ToLower().Contains(vm.AddressTerm) ||
                        place.Country.ToLower().Contains(vm.AddressTerm)
                        )
                    );
            }

            var q = _context.Places
                    .Where(place =>
                        place.ClientUser.PermittedPractices.Any(prac =>
                            prac.Practice.Id == practiceId)
                    )
                    .Where(predicate)
                    .ProjectTo<PlaceSubscriberListItemViewModel>();

            return new PlacesListViewModel<PlaceSubscriberListItemViewModel>()
            {
                Places =
                    await
                    q
                    .Skip(page * take)
                    .Take(take)
                    .ToListAsync(),
                Practices = null,
                TotalResults = q.Count()
            };
        }
    }
}
