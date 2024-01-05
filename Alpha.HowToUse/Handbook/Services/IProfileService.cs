using Alpha.HowToUse.Handbook.Criteria;
using Alpha.HowToUse.Handbook.Models;
using Fanex.Data.Repository;

namespace Alpha.HowToUse.Handbook.Services
{
    public interface IProfileService
    {
        ProfilePictureModel GetProfilePicture(int custId);
    }

    public class ProfileService : IProfileService
    {
        private readonly IDynamicRepository dynamicRepository;

        public ProfileService(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public ProfilePictureModel GetProfilePicture(int custId)
        {
            try
            {
                var model = dynamicRepository.Get<ProfilePictureModel>(new SelectProfilePictureCriteria(custId));
                return model ?? new ProfilePictureModel();
            }
            catch
            {
                return new ProfilePictureModel();
            }
        }
    }
}
