using System.Collections.Generic;
using Alpha.HowToUse.Handbook.Configurations;
using Alpha.HowToUse.Handbook.Models;

namespace Alpha.HowToUse.Handbook.Handlers
{
    public class CategoryHandler
    {
        public APIHandler apiHandler;
        private string contentAPIUrl = AppSettingsHelpers.ContentAPIUrl;

        public CategoryHandler()
        {
            apiHandler = new APIHandler();
        }

        public IEnumerable<CategoryModel> GetAll()
        {
            var url = contentAPIUrl + "/api/categories/latest/all";
            var categories = apiHandler.Get<IEnumerable<CategoryModel>>(url);
            var result = new List<CategoryModel> {
                new CategoryModel
                {
                    ShortName="Reports",
                    Slug="reports"
                },
                new CategoryModel
                {
                    ShortName="Transfer",
                    Slug="transfer",
                    Categories = new List<CategoryModel>
                    {
                         new CategoryModel
                         {
                            ShortName="Transfer Condition",
                            Slug="transfer-condition"
                         },
                         new CategoryModel
                         {
                            ShortName="Auto Transfer",
                            Slug="auto-transfer",
                            Categories = new List<CategoryModel>
                            {
                                 new CategoryModel
                                 {
                                    ShortName="Auto Transfer Guide",
                                    Slug="auto-transfer-guide"
                                 },
                                 new CategoryModel
                                 {
                                    ShortName="Auto Transfer Schedule",
                                    Slug="auto-transfer-schedule"
                                 }
                            }
                         }
                    }
                }
            };

            return result;
        }
    }
}