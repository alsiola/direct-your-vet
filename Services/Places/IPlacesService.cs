using DYV.Models;
using DYV.Models.PlacesViewModels;
using DYV.Models.PlaceViewModels;
using System.Threading.Tasks;
using DYV.Models.Ajax;

namespace DYV.Services.Places
{
    public interface IPlacesService
    {
        Task<PlacesListViewModel<PlaceListItemViewModel>> GetPlacesListViewModelForCurrentUser();
        Task AddNewPlace(AddPlaceViewModel placeModel);
        Task<PlaceDetailsViewModel> GetPlaceById(int placeId, string returnController);
        Task<PlaceDetailsViewModel> GetPlaceById(int value);
        Task<PlaceDetailsViewModel> GetPlaceByIdForAdmin(int value, string returnController);
        Task<PlaceDetailsViewModel> GetPlaceByIdForAdmin(int value);
        Task UpdatePlace(EditPlaceViewModel placeModel);
        Task<EditPlaceViewModel> GetEditPlaceViewModelById(int value);
        Task<SharePlacesViewModel> GetSharePlacesViewModel();
        Task AddSharePlaces(SharePlacesViewModel sharePlacesViewModel);
        Task AddSharePlaceAtRegistration(string userId, int practiceId);
        Task<UnsharePlacesViewModel> GetUnshareViewModel(int value);
        Task<DeletePlaceViewModel> GetDeletePlaceViewModelById(int value);
        Task DeletePlace(int id);
        Task<PlacesListViewModel<PlaceSubscriberListItemViewModel>> GetPlacesListViewModelForCurrentSubscriber();
        Task<PlacesListViewModel<PlaceSubscriberListItemViewModel>> GetPlacesListViewModelForCurrentSubscriber(SubscriberDashboardAjaxSearchViewModel vm);
        Task<PlacesListViewModel<PlaceSubscriberListItemViewModel>> GetPlacesListViewModelForPracticeIfAdmin(int practiceId);
        Task<PlacesListViewModel<PlaceSubscriberListItemViewModel>> GetPlacesListViewModelForPracticeIfAdmin(int practiceId, SubscriberDashboardAjaxSearchViewModel vm);
    }
}
