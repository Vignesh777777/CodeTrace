using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected readonly IRegisterRepository _registerRepository;
        protected readonly IProfileRepository _profileRepository;
        protected readonly ICoursesRepository _coursesRepository;
        protected readonly IOpportunitiesRepository _opportunitiesRepository;
        protected readonly ICodingStatsRepository _codingStatsRepository;

        public BaseApiController(CodingStatsRepository codingStatsRepository)
        {
            _codingStatsRepository = codingStatsRepository;
        }

        public BaseApiController(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public BaseApiController(OpportunitiesRepository opportunitiesRepository)
        {
            _opportunitiesRepository = opportunitiesRepository;
        }

        public BaseApiController(ProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public BaseApiController(IRegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }

        public BaseApiController(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public BaseApiController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public BaseApiController(IOpportunitiesRepository opportunitiesRepository)
        {
            _opportunitiesRepository = opportunitiesRepository;
        }

        public BaseApiController(
            IRegisterRepository registerRepository,
            IProfileRepository profileRepository,
            ICoursesRepository coursesRepository,
            IOpportunitiesRepository opportunitiesRepository,
            ICodingStatsRepository codingStatsRepository)
        {
            _registerRepository = registerRepository;
            _profileRepository = profileRepository;
            _coursesRepository = coursesRepository;
            _opportunitiesRepository = opportunitiesRepository;
            _codingStatsRepository = codingStatsRepository;
        }
    }
}
