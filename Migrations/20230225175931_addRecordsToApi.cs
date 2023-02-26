using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleAppWritesonic.Migrations
{
    /// <inheritdoc />
    public partial class addRecordsToApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string[] stringColumns = { "Key", "Account" };
            var records = new List<string[]> {
                new string[] { "3ead8cbb-6326-4441-88f0-e57a90a62f15", "mimlehirto@gufum.com" },
                new string[] { "5b587e59-303f-4e7e-97cb-439a6d33c05a", "fagnihotro@gufum.com" },
                new string[] { "eddb7544-a24e-43fd-baf0-220a441b3e39", "horzojikke@gufum.com" },
                new string[] { "ef72a7c5-79b5-408f-a479-e6999fad4b4a", "hognegutra@gufum.com" },
                new string[] { "ead9a968-37d0-4dc8-b192-89df771547c4", "bestoyitri@gufum.com" },
                new string[] { "48f7f566-47e1-4f98-aeb8-a3f040871d96", "" },
                new string[] { "5a4210c2-97f0-438a-a1a4-6fea749c2305", "" },
            };

            foreach (var record in records)
            {
                migrationBuilder.InsertData("ApiKeys", stringColumns, record);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
