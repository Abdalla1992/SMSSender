using Microsoft.AspNetCore.Mvc;
using Moq;
using smsSender.api.Controllers;
using smsSender.Common.Enum;
using smsSender.Core.AppService;
using smsSender.Core.IAppService;
using smsSender.Data.Entity.Entity;
using smsSender.Data.Infrastructure.Dto;
using smsSender.Data.Infrastructure.UnitOfWork;

namespace smsSender.UnitTest
{
    public class ProviderUnitTest
    {
        private readonly  Mock< IProviderAppService> ProviderAppService;
        private readonly ProviderController ProviderController;
        public ProviderUnitTest()
        {
         ProviderAppService = new Mock< IProviderAppService>();
            ProviderController = new ProviderController(ProviderAppService.Object);
        }

        [Fact]
        public async Task  GetAll()
        {
            string searchByProviderName = "";

            List<ProviderDto> providers = new List<ProviderDto>();
            ProviderAppService.Setup(x => x.GetAll(searchByProviderName))
             .ReturnsAsync(providers);
            //var ProviderController = new ProviderController (ProviderAppService.Object);
            var result = ProviderController.GetAll(searchByProviderName);
            Assert.NotNull(result);

        }

        [Fact]
        public void Add()
        {
            ResultBaseDto<bool> response = new ResultBaseDto<bool>(false, "Error");
            AddProviderDto Object = new AddProviderDto()
            {
                Name = "Twilio2",
                ApiUrl = "",
                Cost = (decimal)0.20,
                Status = 1,
             

            };

            ProviderAppService.Setup(x => x.Create(Object))
           .ReturnsAsync(response);

            //var ProviderController = new ProviderController(ProviderAppService.Object);
            var result = ProviderController.Create(Object);


            Assert.NotNull(result);

        }
        //private List<Provider> GetProviders()
        //{
        //    List<Provider> providers = new List<Provider>();
        //    providers.Add(new Provider { 
            
        //    ApiUrl="",
        //    Cost= 0.2,
        //    CreationDate= DateTime.Now,
        //    DeletedStatus=DeleteStatusEnum.NotDeleted,
        //    Name=
        //    }

        //}

    }
}